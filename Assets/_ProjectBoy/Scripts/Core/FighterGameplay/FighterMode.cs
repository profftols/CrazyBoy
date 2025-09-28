using System.Collections;
using _ProjectBoy.Scripts.Core.CharacterSc;
using _ProjectBoy.Scripts.Core.FighterGameplay.Punch;
using DG.Tweening;
using UnityEngine;

namespace _ProjectBoy.Scripts.Core.FighterGameplay
{
    public class FighterMode : IFighterMode
    {
        private const string PlayerTag = "Player";
        private const string EnemyTag = "Enemy";
        private const float WaitTime = 1.5f;
        private const float DurationCloser = 1.4f;
        private readonly MonoBehaviour _monoBehaviour;

        private readonly Steps _steps;
        private Coroutine _fightSteps;

        public FighterMode(float power)
        {
            var player = GameObject.FindWithTag(PlayerTag).GetComponent<Player>();
            var enemy = GameObject.FindWithTag(EnemyTag).GetComponent<Enemy>();
            Enemy = new Boss(enemy, power);
            Player = new Hero(player, power);
            _steps = new Steps();
            _monoBehaviour = player;
        }

        private float CloserEnemy => Enemy.Character.transform.position.z < 0
            ? Enemy.Character.transform.position.z + 1f
            : Enemy.Character.transform.position.z - 1f;

        private float CloserPlayer => Player.Character.transform.position.z < 0
            ? Player.Character.transform.position.z + 1f
            : Player.Character.transform.position.z - 1f;

        public Hero Player { get; }
        public Boss Enemy { get; }

        public void InitGame()
        {
            Player.Init(Enemy);
            Enemy.Init(Player);
        }

        public void StopFight()
        {
            if (_fightSteps != null) _monoBehaviour.StopCoroutine(_fightSteps);
        }

        public void StartFight()
        {
            Enemy.ActEnemy();
            _fightSteps = _monoBehaviour.StartCoroutine(LaunchActions());
        }

        public void SubscribeEvent()
        {
            Player.OnActionSteps += _steps.AddAction;
            Enemy.OnActionSteps += _steps.AddAction;
            Player.Health.OnDead += StopFight;
            Enemy.Health.OnDead += StopFight;
        }

        public void UnsubscribeEvent()
        {
            Player.OnActionSteps -= _steps.AddAction;
            Enemy.OnActionSteps -= _steps.AddAction;
            Player.Health.OnDead -= StopFight;
            Enemy.Health.OnDead -= StopFight;
        }

        private IEnumerator LaunchActions()
        {
            var waiter = new WaitForSecondsRealtime(WaitTime);
            var playerTrans = Player.Character.transform;
            var enemyTrans = Enemy.Character.transform;
            var posZPlayer = playerTrans.position.z;
            var posZEnemy = enemyTrans.position.z;

            while (_steps.Count > 0)
            {
                var typeAction = _steps.GetAction();

                if (typeAction is Attack)
                {
                    if (_steps.Count > 2)
                        playerTrans.DOMoveZ(CloserEnemy, DurationCloser);
                    else
                        enemyTrans.DOMoveZ(CloserPlayer, DurationCloser);
                }

                yield return waiter;

                typeAction.Step();

                if (typeAction is Attack)
                    if (_steps.Count <= 3)
                        playerTrans.DOMoveZ(posZPlayer, DurationCloser);
            }

            enemyTrans.DOMoveZ(posZEnemy, DurationCloser);
        }
    }
}