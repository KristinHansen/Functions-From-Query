using System;
using System.Linq;

namespace Azure.Functions.FromQuerySample
{
    internal static class RandomSauce
    {
        private static readonly Random _Random = new Random();
        private const string _Characters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

        internal static string RandomString(int length = 10) 
            => new string(Enumerable.Repeat(_Characters, length).Select(s => s[_Random.Next(s.Length)]).ToArray());

        internal static decimal RandomPrice(int min = 1, int max = 1000, bool withDecimals = true) 
            => decimal.Round(_Random.Next(min, max) + (withDecimals ? (decimal)_Random.NextDouble() : 0m), 2);
    }
}
