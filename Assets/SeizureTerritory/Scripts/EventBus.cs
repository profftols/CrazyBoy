using System;

public static class EventBus
{
    public static Action<Character, float> OnBonusSpeed;
    public static Action<Character, bool> OnBonusInvulnerability;
    public static Action<Item> OnComeBackItem;
}
