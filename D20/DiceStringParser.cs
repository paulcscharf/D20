using System;
using System.Collections.Generic;

namespace D20
{
    internal class DiceStringParser
	{
		private enum CharacterType
		{
			Unknown,
			DiceSymbol,
			Digit,
		    SumSymbol,
		    DifferenceSymbol,
		    ProductSymbol,
		    EndOfString,
		}

		public static Rollable Parse(string dice) => new DiceStringParser(dice).Parse();
		
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

		public Rollable Parse()
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

	    private static readonly Dictionary<char, CharacterType> characters = new Dictionary<char, CharacterType>
	    {
	        { 'd', CharacterType.DiceSymbol },
	        { '+', CharacterType.SumSymbol },
	        { '-', CharacterType.DifferenceSymbol },
	        { '*', CharacterType.ProductSymbol },
	    };

		private static CharacterType typeOfChar(char c)
		{
		    if (c >= '0' && c <= '9')
		        return CharacterType.Digit;
		    CharacterType type;
		    return characters.TryGetValue(c, out type) ? type : CharacterType.Unknown;
		}

	    private Rollable readRollable()
	    {
	        return readSum();
	    }

	    private Rollable readSum()
	    {
	        var sum = readProduct();
	        while (!this.endOfString)
	        {
	            if (this.currentType == CharacterType.SumSymbol)
	            {
	                this.moveNext();
	                sum += readProduct();
	            }
	            else if (this.currentType == CharacterType.DifferenceSymbol)
	            {
	                this.moveNext();
	                sum -= readProduct();
	            }
	            else break;
	        }
	        return sum;
	    }

	    private Rollable readProduct()
	    {
	        var product = readRollableUnit();
	        while (!this.endOfString && this.currentType == CharacterType.ProductSymbol)
	        {
                this.moveNext();
                product *= this.readRollableUnit();
	        }
	        return product;
	    }

		private Rollable readRollableUnit()
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

		private Rollable readDiceOrConstant()
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

		private Rollable readDie()
		{
			this.moveNext();
			return new Die(this.readInt());
		}

		private int readInt()
		{
		    if (this.currentType != CharacterType.Digit)
		        throw expected($"digit, found '{this.current}'");
		    var sum = this.currentAsDigit;
			while (this.moveNext() && this.currentType == CharacterType.Digit)
			{
				sum = sum * 10 + this.currentAsDigit;
			}
			return sum;
		}

	}
}