using System;
using System.Collections.Generic;
using System.Linq;

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
		    OpenParenthesis,
		    CloseParenthesis,
		    EndOfString,
		}

	    private static readonly Dictionary<CharacterType, char> characters = new Dictionary<CharacterType, char>
	    {
	        { CharacterType.DiceSymbol, 'd' },
	        { CharacterType.SumSymbol, '+' },
	        { CharacterType.DifferenceSymbol, '-' },
	        { CharacterType.ProductSymbol, '*' },
	        { CharacterType.OpenParenthesis, '(' },
	        { CharacterType.CloseParenthesis, ')' },
	    };

	    private static readonly Dictionary<char, CharacterType> characterTypes
	        = characters.ToDictionary(p => p.Value, p => p.Key);

	    public static Rollable Parse(string dice, IRandom random = null)
		    => new DiceStringParser(dice, random).parse();
		
		private readonly string input;
	    private readonly IRandom random;

		private int characterIndex = -1;
		private CharacterType currentType = CharacterType.Unknown;

		private char current => this.input[this.characterIndex];
	    private int currentAsDigit => this.current - '0';
	    private bool endOfString => this.characterIndex >= this.input.Length;

	    private DiceStringParser(string input, IRandom random)
		{
			if (string.IsNullOrWhiteSpace(input))
                throw new ArgumentException("Must be non-empty string.", nameof(input));
			this.input = input.Trim().ToLower();
		    this.random = random;
		}

		private Rollable parse()
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

	    private void read(CharacterType characterType)
	    {
	        var character = characters[characterType];
	        if (this.endOfString)
	            throw expected($"'{character}', found end of string");
	        if (this.currentType != characterType)
	            throw expected($"'{character}', found '{this.current}'");
	        this.moveNext();
	    }

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
		    if (c >= '0' && c <= '9')
		        return CharacterType.Digit;
		    CharacterType type;
		    return characterTypes.TryGetValue(c, out type) ? type : CharacterType.Unknown;
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
			    case CharacterType.OpenParenthesis:
			        return this.readParenthesized();
			    case CharacterType.EndOfString:
			        throw expected($"expression, found end of string");
			    default:
                    throw expected($"expression, found '{this.current}'");
			}
		}

	    private Rollable readParenthesized()
	    {
	        this.read(CharacterType.OpenParenthesis);
	        var rollable = this.readRollable();
	        this.read(CharacterType.CloseParenthesis);
	        return rollable;
	    }

	    private Rollable readDiceOrConstant()
		{
			var c = this.readInt();
			if (this.endOfString || this.currentType != CharacterType.DiceSymbol)
				return new Constant(c);

		    this.read(CharacterType.DiceSymbol);
			var d = this.readInt();
			if (c == 0 || d <= 1)
				return new Constant(c);
			if (c == 1)
				return new Die(d, this.random);
			return new Dice(c, d, this.random);
		}

		private Rollable readDie()
		{
			this.read(CharacterType.DiceSymbol);
			return new Die(this.readInt());
		}

		private int readInt()
		{
		    var sum = this.readDigit();
		    int digit;
		    while (this.tryReadDigit(out digit))
		    {
		        sum = sum * 10 + digit;
		    }
		    return sum;
		}

	    private int readDigit()
	    {
	        if (this.endOfString)
	            throw expected($"digit, found end of string");
	        if (this.currentType != CharacterType.Digit)
	            throw expected($"digit, found '{this.current}'");

	        var i = this.currentAsDigit;
	        this.moveNext();
	        return i;
	    }

	    private bool tryReadDigit(out int digit)
	    {
	        digit = 0;
	        if (this.currentType != CharacterType.Digit)
	            return false;

	        digit = this.currentAsDigit;
	        this.moveNext();
	        return true;
	    }

	}
}