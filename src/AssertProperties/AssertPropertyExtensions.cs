namespace AssertProperties
{
    public static class AssertPropertyExtensions
    {
        public static AssertProperties<T> AssertProperties<T>(this T objectValue)
        {
            return new AssertProperties<T>(objectValue);
        }
    }
}