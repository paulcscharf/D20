using System;
using System.Threading;

namespace D20
{
    public sealed class ThreadSafeRandom : IRandom
    {
        private readonly ThreadLocal<Random> random;

        public ThreadSafeRandom()
            : this(() => new Random())
        { }

        public ThreadSafeRandom(Func<Random> randomFactory)
        {
            if (randomFactory == null)
                throw new ArgumentNullException(nameof(randomFactory));

            this.random = new ThreadLocal<Random>(randomFactory);
        }

        public int Next(int lowerInclusiveBound, int upperExclusiveBound)
            => this.random.Value.Next(lowerInclusiveBound, upperExclusiveBound);
    }
}