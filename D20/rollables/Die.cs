using System;
using System.Linq;
using System.Collections.Generic;

namespace D20
{
    public sealed class Die : Rollable
	{
		public static readonly Die D2 = new Die(2);
		public static readonly Die D4 = new Die(4);
		public static readonly Die D6 = new Die(6);
		public static readonly Die D8 = new Die(8);
		public static readonly Die D10 = new Die(10);
		public static readonly Die D12 = new Die(12);
		public static readonly Die D20 = new Die(20);

		private readonly int value;
	    private readonly IRandom random;

	    public Die(int value, IRandom random = null)
	    {
	        if (value < 1)
	            throw new ArgumentException("Must be equal or larger than 1.", nameof(value));

	        this.value = value;
	        this.random = random ?? Rollable.DefaultRandom;
	    }

        public override int MinValue => 1;
        public override int MaxValue => this.value;
		public override double Average => (this.value + 1) * 0.5;
		public override IEnumerable<int> PossibleValues => Enumerable.Range(1, this.value);
		public override IEnumerable<CountedValue> CountedValues => this.PossibleValues.Select(CountedValue.Single);

		public override int Roll() => this.random.Next(1, this.value + 1);

	    public override Rollable With(IRandom random) => new Die(this.value, random);

	    public override string ToString() => $"d{this.value}";
	}
}