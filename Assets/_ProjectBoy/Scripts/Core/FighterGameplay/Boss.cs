using _ProjectBoy.Scripts.Core.CharacterSc;
using _ProjectBoy.Scripts.Core.FighterGameplay.Punch;
using Random = UnityEngine.Random;

namespace _ProjectBoy.Scripts.Core.FighterGameplay
{
    public class Boss : Fighters
    {
        private readonly int _maxRandom = 10;
        private readonly int _minRandom = 0;
        private readonly float _multiplyAttack = 1.2f;
        private readonly float _multiplyHealth = 28.4f;
        private readonly int _probabilityDefence = 3;
        private readonly int _stepsCount = 3;

        private float _power;

        public Boss(Character character, float power) : base(character)
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

        public void ActEnemy()
        {
            for (var i = 0; i < _stepsCount; i++)
                if (Random.Range(_minRandom, _maxRandom) <= _probabilityDefence)
                    DefenceAction();
                else
                    AttackAction();
        }
    }
}