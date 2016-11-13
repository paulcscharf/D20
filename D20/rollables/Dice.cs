using System;
using System.Linq;
using System.Collections.Generic;

namespace D20
{
    internal class Dice : IRollable
	{
		private static readonly Random random = new Random();

		public static Dice D2(int count) => new Dice(count, 2);
		public static Dice D4(int count) => new Dice(count, 4);
		public static Dice D6(int count) => new Dice(count, 6);
		public static Dice D8(int count) => new Dice(count, 8);
		public static Dice D10(int count) => new Dice(count, 10);
		public static Dice D12(int count) => new Dice(count, 12);
		public static Dice D20(int count) => new Dice(count, 20);

		private readonly int count;
		private readonly int value;

		public Dice(int count, int value)
		{
			this.count = count;
			this.value = value;
		}

		public int MinValue => this.count;
		public int MaxValue => this.count * this.value;
		public double Average => this.count * (this.value + 1) * 0.5;
		public IEnumerable<int> PossibleValues => Enumerable.Range(this.MinValue, this.MaxValue - this.MinValue + 1);
		public IEnumerable<CountedValue> CountedValues
		{
			get
			{
				var range = Enumerable.Range(1, this.value);
				var all = range;
				for (int i = 1; i < this.count; i++)
				{
					all = all.Join(range, (l, r) => l + r);
				}
				return all.GroupBy(x => x)
					.Select(CountedValue.FromGroup)
					.OrderBy(v => v.Value);
			}
		}

        public int Roll() =>
            this.count + this.count.Times(() => Dice.random.Next(this.value)).Sum();

		public override string ToString() => $"{this.count}d{this.value}";
	}
}