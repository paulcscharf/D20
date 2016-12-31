
namespace D20
{
    public sealed class Product : BinaryOperator
	{
	    public static Product Of(Rollable left, Rollable right) => new Product(left, right);

	    public Product(Rollable left, Rollable right)
			: base (left, right) { }

		protected override int Apply(int l, int r) => l * r;
		protected override double Apply(double l, double r)	=> l * r;
		protected override string Symbol => "*";
	    protected override Precedence OperatorPrecedence => Precedence.Product;

	    public override Rollable With(Rollable left, Rollable right)
	        => new Product(left, right);
	}
}