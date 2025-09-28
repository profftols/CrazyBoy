using System.Collections;
using _ProjectBoy.Scripts.Service;
using UnityEngine;

namespace _ProjectBoy.Scripts.Core.RunnerGameplay
{
    public class Mover : MonoBehaviour
    {
        private const float DefaultSpeedModifier = 1f;
        private const float IdleSpeed = 0.01f;
        private static readonly int MovementSpeed = Animator.StringToHash("movementSpeed");
        private Animator _animator;

        private CharacterController _characterController;
        private Vector3 _inputDirection = Vector3.zero;

        private float _speedModifier = 1f;

        [SerializeField] protected float moveSpeed = 5f;
        [SerializeField] protected float rotationSpeed = 9999f;

        public Vector3 Direction
        {
            get => _inputDirection;
            set
            {
                _inputDirection = value;
                _inputDirection.y = 0;
            }
        }

        public void Init(CharacterController controller, Animator animator)
        {
            _characterController = controller;
            _animator = animator;
        }

        public void SetSpeedModifier(float timeBonusEffect, float spdbonus)
        {
            StartCoroutine(ActivationBonus(timeBonusEffect, spdbonus));
        }

        protected void Move()
        {
            if (_inputDirection.sqrMagnitude > Constants.Epsilon)
            {
                var move = _inputDirection.normalized * moveSpeed * _speedModifier;
                _characterController.Move(move * Time.deltaTime);
                var targetRotation = Quaternion.LookRotation(move);
                _characterController.transform.rotation = Quaternion.Slerp(_characterController.transform.rotation,
                    targetRotation, rotationSpeed * Time.deltaTime);
                AnimationService.SetSpeedAnim(_animator, _speedModifier);
            }
            else
            {
                AnimationService.SetSpeedAnim(_animator, IdleSpeed);
            }
        }

        private IEnumerator ActivationBonus(float time, float spdbonus)
        {
            _speedModifier = spdbonus;
            yield return new WaitForSeconds(time);
            _speedModifier = DefaultSpeedModifier;
        }
    }
}