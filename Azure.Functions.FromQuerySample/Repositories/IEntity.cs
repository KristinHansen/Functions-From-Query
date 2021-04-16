using System;

namespace Azure.Functions.FromQuerySample
{
    public interface IEntity
    {
        public int Ix { get; }
        public Guid Id { get; }
    }
}
