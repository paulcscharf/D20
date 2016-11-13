
namespace D20
{
    internal class Sum : BinaryOperator
	{
		public Sum(IRollable left, IRollable right)
			: base (left, right) { }

		protected override int Apply(int l, int r) => l + r;
		protected override double Apply(double l, double r)	=> l + r;
		protected override string Symbol => "+";
	}
}