public static class Conditions
{
    public delegate bool Condition();

    public static bool MyConditionOne()
    {
        return true;
    }

    public static bool MyConditionTwo()
    {
        return true;
    }

    public static bool MyConditionThree()
    {
        return true;
    }
}