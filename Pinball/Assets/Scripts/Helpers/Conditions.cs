public static class Conditions
{
    public delegate bool Condition();

    public static class LightOnConditions
    {
        private static bool MultipleIsNumber(int number)
    {
        ScoreManager scoreManager = Finder.GetScoreManager();

        if(scoreManager.multiplier >= number)
        { 
            return true;

        }
        else
        {
            return false;
        }
    }

    // TODO(Roger): Change this. Please.
    public static bool MultipleIsOne() => MultipleIsNumber(1);
    public static bool MultipleIsTwo() => MultipleIsNumber(2);
    public static bool MultipleIsThree() => MultipleIsNumber(3);
    public static bool MultipleIsFour() => MultipleIsNumber(4);
    public static bool MultipleIsFive() => MultipleIsNumber(5);
    public static bool MultipleIsSix() => MultipleIsNumber(6);
    public static bool MultipleIsSeven() => MultipleIsNumber(7);
    public static bool MultipleIsEight() => MultipleIsNumber(8);
    public static bool MultipleIsNine() => MultipleIsNumber(9);
    public static bool MultipleIsTen() => MultipleIsNumber(10);
    public static bool MultipleIsOneHundred() => MultipleIsNumber(100);
    }

    public static class LightOffConditions
    {
        public static bool Example()
        {
            return false;
        }

        public static bool Example2()
        {
            return false;
        }
    }
}