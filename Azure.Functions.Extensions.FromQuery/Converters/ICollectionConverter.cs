using System.Collections;

namespace Azure.Functions.Extensions.FromQuery
{
    /// <summary>
    /// Used to convert non-generic collection into a generic collection.
    /// </summary>
    public interface ICollectionConverter
    {
        /// <summary>
        /// Converts the <paramref name="collection"/> into a generic collection.
        /// </summary>
        /// <param name="collection"></param>
        /// <returns></returns>
        ICollection Convert(ICollection collection);
    }
}
