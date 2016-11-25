using System.Collections.Generic;
using System.Linq;

namespace D20
{
    internal abstract class BinaryOperator : IRollable
	{
		protected abstract int Apply(int left, int right);
		protected abstract double Apply(double left, double right);
		protected abstract string Symbol { get; }

		protected IRollable Left { get; }
		protected IRollable Right { get; }

		protected BinaryOperator(IRollable left, IRollable right)
		{
			this.Left = left;
			this.Right = right;
		}

		public override int MinValue => this.Apply(this.Left.MinValue, this.Right.MinValue);
		public override int MaxValue => this.Apply(this.Left.MaxValue, this.Right.MaxValue);
		public override double Average => this.Apply(this.Left.Average, this.Right.Average);

		public override IEnumerable<int> PossibleValues =>
			this.Left.PossibleValues
				.Join(this.Right.PossibleValues, (l, r) => this.Apply(l, r))
				.Distinct()
				.OrderBy(x => x);

		public override IEnumerable<CountedValue> CountedValues =>
			this.Left.CountedValues
				.Join(this.Right.CountedValues, (l, r) => new CountedValue(this.Apply(l.Value, r.Value), l.Count * r.Count))
				.GroupBy(x => x.Value)
				.Select(CountedValue.FromGroup)
				.OrderBy(v => v.Value);

		public override int Roll() => this.Apply(this.Left.Roll(), this.Right.Roll());

		public override string ToString() => $"{this.Left}{this.Symbol}{this.Right}";
	}
}