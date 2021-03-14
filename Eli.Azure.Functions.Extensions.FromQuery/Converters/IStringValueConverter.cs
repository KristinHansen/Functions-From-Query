using System;

namespace Eli.Azure.Functions.Extensions
{
    public interface IStringValueConverter
    {
        Type? Type { get; }
        object Convert(string value);
    }
}
