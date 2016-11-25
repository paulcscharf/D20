
namespace D20
{
    public sealed class Difference : BinaryOperator
	{
		public Difference(Rollable left, Rollable right)
			: base (left, right) { }

		protected override int Apply(int l, int r) => l * r;
		protected override double Apply(double l, double r)	=> l * r;
		protected override string Symbol => "-";

		public override int MinValue => this.Left.MinValue - this.Right.MaxValue;
		public override int MaxValue => this.Left.MaxValue - this.Right.MinValue;
	}
}