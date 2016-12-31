using System;
using System.Linq;
using System.Collections.Generic;

namespace D20
{
    public sealed class Die : Rollable
	{
		public static Die D2 => new Die(2);
		public static Die D4 => new Die(4);
		public static Die D6 => new Die(6);
		public static Die D8 => new Die(8);
		public static Die D10 => new Die(10);
		public static Die D12 => new Die(12);
		public static Die D20 => new Die(20);

		public int Value { get; }
	    private readonly IRandom random;

	    public Die(int value, IRandom random = null)
	    {
	        if (value < 1)
	            throw new ArgumentException("Must be equal or larger than 1.", nameof(value));

	        this.Value = value;
	        this.random = random ?? Rollable.DefaultRandom;
	    }

        public override int MinValue => 1;
        public override int MaxValue => this.Value;
		public override double Average => (this.Value + 1) * 0.5;
		public override IEnumerable<int> PossibleValues => Enumerable.Range(1, this.Value);
		public override IEnumerable<CountedValue> CountedValues => this.PossibleValues.Select(CountedValue.Single);

		public override int Roll() => this.random.Next(1, this.Value + 1);

	    public override Rollable With(IRandom random) => new Die(this.Value, random);

	    public override string ToString() => $"d{this.Value}";
	}
}