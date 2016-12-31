using System;
using System.Linq;
using System.Collections.Generic;

namespace D20
{
    public sealed class Dice : Rollable
	{
		public static Dice D2(int count) => new Dice(count, 2);
		public static Dice D4(int count) => new Dice(count, 4);
		public static Dice D6(int count) => new Dice(count, 6);
		public static Dice D8(int count) => new Dice(count, 8);
		public static Dice D10(int count) => new Dice(count, 10);
		public static Dice D12(int count) => new Dice(count, 12);
		public static Dice D20(int count) => new Dice(count, 20);

		public int Count { get; }
		public int Value { get; }
	    private readonly IRandom random;

		public Dice(int count, int value, IRandom random = null)
		{
		    if (count < 1)
		        throw new ArgumentException("Must be equal or larger than 1.", nameof(count));
		    if (value < 1)
		        throw new ArgumentException("Must be equal or larger than 1.", nameof(value));

		    this.Count = count;
			this.Value = value;
		    this.random = random ?? Rollable.DefaultRandom;
		}

		public override int MinValue => this.Count;
		public override int MaxValue => this.Count * this.Value;
		public override double Average => this.Count * (this.Value + 1) * 0.5;
		public override IEnumerable<int> PossibleValues => Enumerable.Range(this.MinValue, this.MaxValue - this.MinValue + 1);
		public override IEnumerable<CountedValue> CountedValues
		{
			get
			{
				var range = Enumerable.Range(1, this.Value);
				var all = range;
				for (int i = 1; i < this.Count; i++)
				{
					all = all.Join(range, (l, r) => l + r);
				}
				return all.GroupBy(x => x)
					.Select(CountedValue.FromGroup)
					.OrderBy(v => v.Value);
			}
		}

        public override int Roll() =>
            this.Count + this.Count.Times(() => this.random.Next(1, this.Value + 1)).Sum();

	    public override Rollable With(IRandom random)
	        => new Dice(this.Count, this.Value, random);

	    public override string ToString() => $"{this.Count}d{this.Value}";
	}
}