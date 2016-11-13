using System.Linq;

namespace D20
{
	public struct CountedValue
	{
	    public int Value { get; }
	    public int Count { get; }

	    public CountedValue(int value, int count)
		{
		    this.Value = value;
		    this.Count = count;
		}

	    public static CountedValue Single(int value)
		{
			return new CountedValue(value, 1);
		}

		public static CountedValue FromGroup(IGrouping<int, int> group)
		{
			return new CountedValue(group.Key, group.Count());
		}
		public static CountedValue FromGroup(IGrouping<int, CountedValue> group)
		{
			return new CountedValue(group.Key, group.Sum(v => v.Count));
		}

		public override string ToString()
		{
			return this.Value + "(x" + this.Count + ")";
		}
	}
}