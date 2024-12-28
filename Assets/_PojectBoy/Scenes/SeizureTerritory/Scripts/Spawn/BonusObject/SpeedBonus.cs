public class SpeedBonus : Item
{
    protected override float Timer => 4.5f;
    
    private float _speed = 1.5f;
    
    public override void OnPickUp(Character character)
    {
        EventBus.OnBonusSpeed?.Invoke(character, _speed);
        gameObject.SetActive(false);
    }
}
