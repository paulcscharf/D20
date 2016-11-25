using System.Collections.Generic;

namespace D20
{
    internal sealed class Constant : IRollable
	{
		private readonly int value;

		public Constant(int value)
		{
		    this.value = value;
		}

		public override int MinValue => this.value;
		public override int MaxValue => this.value;
		public override double Average => this.value;
        public override IEnumerable<int> PossibleValues => this.value.Yield();
        public override IEnumerable<CountedValue> CountedValues => CountedValue.Single(this.value).Yield();

		public override int Roll() => this.value;

		public override string ToString() => this.value.ToString();
	}
}