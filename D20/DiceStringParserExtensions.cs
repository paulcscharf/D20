
namespace D20
{
	public static class DiceStringParserExtensions
	{
		public static Rollable Rollable(this string dice)
		{
			return DiceStringParser.Parse(dice);
		}
		public static void Roll(this string dice)
		{
			dice.Rollable().Roll();
		}
	}
}