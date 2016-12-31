using System.Collections.Generic;

namespace D20
{
    public abstract class Rollable
    {
        public static IRandom DefaultRandom { get; } = new SimpleRandomAdapter();
        public static IRandom ThreadSafeRandom { get; } = new SimpleRandomAdapter();

        public abstract int MaxValue { get; }
        public abstract int MinValue { get; }
        public abstract double Average { get; }

        public abstract IEnumerable<int> PossibleValues { get; }
        public abstract IEnumerable<CountedValue> CountedValues { get; }

        public abstract int Roll();

        public abstract Rollable With(IRandom random);
        public Rollable ThreadSafe => this.With(Rollable.ThreadSafeRandom);

        public Rollable Plus(Rollable right) => this + right;
        public Rollable Minus(Rollable right) => this - right;
        public Rollable Times(Rollable right) => this * right;

        public static Rollable operator +(Rollable left, Rollable right) => Sum.Of(left, right);
        public static Rollable operator -(Rollable left, Rollable right) => Difference.Of(left, right);
        public static Rollable operator *(Rollable left, Rollable right) => Product.Of(left, right);

        public static implicit operator Rollable(int constant) => new Constant(constant);

        public abstract override string ToString();
    }
}