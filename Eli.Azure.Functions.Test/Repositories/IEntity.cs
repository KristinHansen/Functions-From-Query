using System;

namespace Eli.Azure.Functions.Test
{
    public interface IEntity
    {
        public int Ix { get; }
        public Guid Id { get; }
    }
}
