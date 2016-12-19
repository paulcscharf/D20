namespace D20
{
    public static class Extensions
    {
        public static Dice D2(this int count) => Dice.D2(count);
        public static Dice D4(this int count) => Dice.D4(count);
        public static Dice D6(this int count) => Dice.D6(count);
        public static Dice D8(this int count) => Dice.D8(count);
        public static Dice D10(this int count) => Dice.D10(count);
        public static Dice D12(this int count) => Dice.D12(count);
        public static Dice D20(this int count) => Dice.D20(count);

        public static Dice D(this int count, int value) => new Dice(count, value);
    }
}