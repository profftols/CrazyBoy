using System;

public static class EventBus
{
    public static Action<Character, float> OnBonusSpeed;
    public static Action<Character, bool> OnBonusInvulnerability;
    public static Action<Item> OnComebackItem;
    public static Action<float> OnScore;
    public static Action<float> OnDefeatGame;
    public static Action<float> OnVictoryGame;
}
