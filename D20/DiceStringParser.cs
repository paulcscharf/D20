using System;

namespace D20
{
    internal class DiceStringParser
	{
		private enum CharacterType
		{
			Unknown,
			DiceSymbol,
			Digit,
		    EndOfString,
		}

		public static IRollable Parse(string dice) => new DiceStringParser(dice).Parse();
		
		private readonly string input;
		private int characterIndex = -1;
		private CharacterType currentType = CharacterType.Unknown;

		private char current => this.input[this.characterIndex];
	    private int currentAsDigit => this.current - '0';
	    private bool endOfString => this.characterIndex >= this.input.Length;

	    private DiceStringParser(string input)
		{
			if (string.IsNullOrWhiteSpace(input))
                throw new ArgumentException("Must be non-empty string.", nameof(input));
			this.input = input.Trim().ToLower();
		}

		public IRollable Parse()
		{
			try
			{
				this.moveNext();
				var rollable = this.readRollable();

				if (this.characterIndex != this.input.Length)
				{
                    throw expected("end of string");
				}

				return rollable;
			}
			catch (Exception e)
			{
				var message =
$@"Syntax error parsing dice string at :{this.characterIndex}
{this.input}
{new string('-', this.characterIndex)}^
{e.Message}";
                
			    throw new Exception(message);
			}
		}

        private static Exception expected(string expectedString) => new Exception($"Expected {expectedString}");

		private bool moveNext()
		{
			this.characterIndex++;
			if (this.endOfString)
			{
			    this.currentType = CharacterType.EndOfString;
				return false;
			}
            this.currentType = typeOfChar(this.current);
			return true;
		}

		private static CharacterType typeOfChar(char c)
		{
			if (c == 'd')
				return CharacterType.DiceSymbol;
			if (c >= '0' && c <= '9')
				return CharacterType.Digit;
			return CharacterType.Unknown;
		}

		private IRollable readRollable()
		{
			switch (this.currentType)
			{
				case CharacterType.DiceSymbol:
					return this.readDie();
				case CharacterType.Digit:
					return this.readDiceOrConstant();
			    default:
                    throw expected($"digit or 'd', found '{this.current}'");
			}
		}

		private IRollable readDiceOrConstant()
		{
			var c = this.readInt();
			if (this.endOfString || this.currentType != CharacterType.DiceSymbol)
				return new Constant(c);

			this.moveNext();
			var d = this.readInt();
			if (c == 0 || d <= 1)
				return new Constant(c);
			if (c == 1)
				return new Die(d);
			return new Dice(c, d);
		}

		private IRollable readDie()
		{
			this.moveNext();
			return new Die(this.readInt());
		}

		private int readInt()
		{
		    var sum = this.currentAsDigit;
			while (this.moveNext() && this.currentType == CharacterType.Digit)
			{
				sum = sum * 10 + this.currentAsDigit;
			}
			return sum;
		}

	}
}