
namespace D20
{
	public static class DiceStringParserExtensions
	{
		public static Rollable Rollable(this string dice, IRandom random = null)
		{
			return DiceStringParser.Parse(dice, random);
		}
		public static void Roll(this string dice, IRandom random = null)
		{
			dice.Rollable(random).Roll();
		}
	}
}