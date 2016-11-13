using System.Collections.Generic;

namespace D20
{
    internal class Constant : IRollable
	{
		private readonly int value;

		public Constant(int value)
		{
		    this.value = value;
		}

		public int MinValue => this.value;
		public int MaxValue => this.value;
		public double Average => this.value;
        public IEnumerable<int> PossibleValues => this.value.Yield();
        public IEnumerable<CountedValue> CountedValues => CountedValue.Single(this.value).Yield();

		public int Roll() => this.value;

		public override string ToString() => this.value.ToString();
	}
}