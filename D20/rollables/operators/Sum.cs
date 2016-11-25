
namespace D20
{
    public sealed class Sum : BinaryOperator
	{
		public Sum(Rollable left, Rollable right)
			: base (left, right) { }

		protected override int Apply(int l, int r) => l + r;
		protected override double Apply(double l, double r)	=> l + r;
		protected override string Symbol => "+";
	}
}