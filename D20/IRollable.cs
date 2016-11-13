using System.Collections.Generic;

namespace D20
{
    public interface IRollable
    {
        int MaxValue { get; }
        int MinValue { get; }
        double Average { get; }

        IEnumerable<int> PossibleValues { get; }
        IEnumerable<CountedValue> CountedValues { get; }

		int Roll();
    }
}