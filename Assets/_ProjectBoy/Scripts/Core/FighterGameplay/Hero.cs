using _ProjectBoy.Scripts.Core.CharacterSc;
using _ProjectBoy.Scripts.Core.FighterGameplay.Punch;

namespace _ProjectBoy.Scripts.Core.FighterGameplay
{
    public class Hero : Fighters
    {
        private readonly float _multiplyAttack = 1.4f;
        private readonly float _multiplyHealth = 21.4f;
        private float _power;

        public Hero(Character character, float power) : base(character)
        {
            _power = power;
        }

        public string Name => Character.Name;

        public void Init(IDamageable enemy)
        {
            if ((int)_power < 200)
                _power = 200f;

            Health = new Health(_power * _multiplyHealth);
            defence = new Defence(Character.Animator, this, _power);
            attack = new Attack(Character.Animator, enemy, _power * _multiplyAttack);
            Health.OnDead += Dead;
        }
    }
}