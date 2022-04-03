using Mapster;

namespace BaseQueryDemo.Common
{
    /// <summary>
    /// AutoMapper
    /// </summary>
    public static class Mapper
    {
        public static T MapTo<F, T>(this F input)
        {
            return input.Adapt<F, T>();
        }

        public static IEnumerable<T> MapTo<F, T>(this IEnumerable<F> input)
        {
            var a = input.Adapt<IEnumerable<T>>();
            return a;
        }
    }
}