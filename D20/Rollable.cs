using System.Collections.Generic;

namespace D20
{
    public abstract class Rollable
    {
        public abstract int MaxValue { get; }
        public abstract int MinValue { get; }
        public abstract double Average { get; }

        public abstract IEnumerable<int> PossibleValues { get; }
        public abstract IEnumerable<CountedValue> CountedValues { get; }

        public abstract int Roll();
    }
}