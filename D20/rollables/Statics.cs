namespace D20
{
    public static class Statics
    {
        public static Die D2 => Die.D2;
        public static Die D4 => Die.D4;
        public static Die D6 => Die.D6;
        public static Die D8 => Die.D8;
        public static Die D10 => Die.D10;
        public static Die D12 => Die.D12;
        public static Die D20 => Die.D20;

        public static Dice D2s(int count) => new Dice(count, 2);
        public static Dice D4s(int count) => new Dice(count, 4);
        public static Dice D6s(int count) => new Dice(count, 6);
        public static Dice D8s(int count) => new Dice(count, 8);
        public static Dice D10s(int count) => new Dice(count, 10);
        public static Dice D12s(int count) => new Dice(count, 12);
        public static Dice D20s(int count) => new Dice(count, 20);

    }
}