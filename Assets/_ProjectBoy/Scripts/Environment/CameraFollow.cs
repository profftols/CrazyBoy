using UnityEngine;

namespace _ProjectBoy.Scripts.Environment
{
    public class CameraFollow : MonoBehaviour
    {
        private Quaternion _initialRotation;
        [SerializeField] private float distance;
        [SerializeField] private Transform following;
        [SerializeField] private float offsetY;
        [SerializeField] private float rotationAngelX;

        private void LateUpdate()
        {
            if (!following)
                return;

            UpdatePosition();
        }

        public void SetFollowing(Transform follow)
        {
            following = follow;
            InitializeCameraRotation();
        }

        private void InitializeCameraRotation()
        {
            var targetYRotation = following.eulerAngles.y;
            _initialRotation = Quaternion.Euler(rotationAngelX, targetYRotation, 0f);
            transform.rotation = _initialRotation;
        }

        private void UpdatePosition()
        {
            var offset = _initialRotation * new Vector3(0, 0, -distance);
            var position = following.position + Vector3.up * offsetY + offset;
            transform.position = position;
        }
    }
}