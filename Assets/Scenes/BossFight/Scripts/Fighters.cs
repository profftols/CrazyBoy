using System;
using UnityEngine;
using UnityEngine.UI;

namespace Scenes.BossFight.Scripts
{
    public class Fighters : MonoBehaviour
    {
        [SerializeField] private Fighters _enemy;
        [SerializeField] private Steps _steps;

        private Defence _defence;
        private Attack _attack;
        private float _power;
        private float _health;
        private float _defenceCoficent = 0.6f;
        private bool _isDefence;


        private void Start()
        {
            _defence = new Defence(this, _power);
            _attack = new Attack(_enemy, _power);
        }

        public void Defence()
        {
            _steps.Add(_defence);
        }

        public void Attack()
        {
            _steps.Add(_attack);
        }

        public void TakeDamage(float damage)
        {
            if (_isDefence)
            {
                _health -= damage * _defenceCoficent;
                _isDefence = false;
                return;
            }
            else
            {
                _health -= damage;
            }
            
            if (_health <= 0)
            {
                return; //dead;
            }
        }

        public void TakeDefence()
        {
            _isDefence = true;
        }
    }
}