namespace BaseQuery.Common
{
    /// <summary>
    /// AutoMapper
    /// </summary>
    public static class Mapper
    {
        public static T MapTo<F, T>(this F input)
        {
            return default;
        }

        public static IEnumerable<T> MapTo<F, T>(this IEnumerable<F> input)
        {
            return default;
        }
    }
}