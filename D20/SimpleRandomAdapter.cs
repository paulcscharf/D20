using System;

namespace D20
{
    public sealed class SimpleRandomAdapter : IRandom
    {
        private readonly Random random;

        public SimpleRandomAdapter()
            : this(new Random())
        { }

        public SimpleRandomAdapter(Random random)
        {
            if (random == null)
                throw new ArgumentNullException(nameof(random));

            this.random = random;
        }

        public int Next(int lowerInclusiveBound, int upperExclusiveBound)
            => this.random.Next(lowerInclusiveBound, upperExclusiveBound);
    }
}