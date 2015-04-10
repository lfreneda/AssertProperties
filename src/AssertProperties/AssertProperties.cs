using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace AssertProperties
{
    public class AssertProperties<T>
    {
        private readonly ICollection<string> _errors = new List<string>();

        public class AssertValue<TResult>
        {
            private readonly object _value;
            private readonly string _propertName;
            private readonly AssertProperties<T> _parent;

            public AssertValue(string propertName, object value, AssertProperties<T> parent)
            {
                _propertName = propertName;
                _value = value;
                _parent = parent;
            }

            public AssertProperties<T> ShouldBeNull()
            {
                if (_value != null)
                {
                    _parent.AddError(string.Format("{0} expected to be NULL but was {1}", _propertName, _value));
                }

                return _parent;
            }

            public AssertProperties<T> ShouldBe(TResult expectedValue)
            {
                if (expectedValue == null && _value == null)
                {
                    return _parent;
                }

                if (expectedValue == null || _value == null || !expectedValue.Equals(_value))
                {
                    _parent.AddError(string.Format("{0} expected to be {1} but was {2}", _propertName, expectedValue, _value));
                }

                return _parent;
            }
        }

        private void AddError(string error)
        {
            _errors.Add(error);
        }

        private readonly T _object;

        public AssertProperties(T @object)
        {
            _object = @object;
        }

        public AssertValue<TResult> EnsureThat<TResult>(Expression<Func<T, TResult>> expression)
        {
            MemberExpression memberExpression = null;

            if (expression.Body.NodeType == ExpressionType.Convert)
                memberExpression = ((UnaryExpression)expression.Body).Operand as MemberExpression;
            else if (expression.Body.NodeType == ExpressionType.MemberAccess)
                memberExpression = expression.Body as MemberExpression;

            if (memberExpression == null || memberExpression.Member == null)
                throw new ArgumentNullException("expression", "Not a member access!");

            var propertyInfo = memberExpression.Member as PropertyInfo;
            if (propertyInfo == null)
            {
                throw new InvalidOperationException("Could not found property name");
            }

            var function = expression.Compile();
            var value = function.Invoke(_object);
            return new AssertValue<TResult>(propertyInfo.Name, value, this);
        }

        public AssertValue<TResult> And<TResult>(Expression<Func<T, TResult>> expression)
        {
            return EnsureThat(expression);
        }

        public void Assert()
        {
            if (!_errors.Any())
            {
                return;
            }

            var sb = new StringBuilder();
            foreach (var error in _errors)
            {
                sb.AppendLine(error);
            }
            throw new AssertExpcetion(sb.ToString());
        }

        public class AssertExpcetion : Exception
        {
            public AssertExpcetion(string message)
                : base(message)
            {
            }
        }
    }
}
