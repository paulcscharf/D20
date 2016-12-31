
namespace D20
{
    public sealed class Sum : BinaryOperator
	{
	    public static Sum Of(Rollable left, Rollable right) => new Sum(left, right);

		public Sum(Rollable left, Rollable right)
			: base (left, right) { }

		protected override int Apply(int l, int r) => l + r;
		protected override double Apply(double l, double r)	=> l + r;
		protected override string Symbol => "+";
	    protected override Precedence OperatorPrecedence => Precedence.Sum;

	    public override Rollable With(Rollable left, Rollable right)
	        => new Sum(left, right);
	}
}