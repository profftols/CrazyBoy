using UnityEngine;

namespace _ProjectBoy.Scripts.Core.RunnerGameplay.Plane
{
    public class Land : MonoBehaviour
    {
        private static readonly int ColorProperty = Shader.PropertyToID("_Color");
        private Color _currentColor;

        private MaterialPropertyBlock _mpb;
        [SerializeField] private Outline _outline;

        [SerializeField] private Renderer _renderer;

        private void Awake()
        {
            _mpb = new MaterialPropertyBlock();
            _renderer.GetPropertyBlock(_mpb);
            _currentColor = _mpb.GetColor(ColorProperty);
        }

        public void ActivationOutline()
        {
            _outline.enabled = true;
        }

        public void DeactivationOutline()
        {
            _outline.enabled = false;
        }

        public void SetMaterial(Material material)
        {
            if (material == null)
                return;

            // Берём только цвет из переданного материала
            var newColor = material.HasProperty(ColorProperty)
                ? material.GetColor(ColorProperty)
                : Color.white;

            // Применяем цвет, если он действительно изменился
            if (newColor != _currentColor)
            {
                _currentColor = newColor;
                _renderer.GetPropertyBlock(_mpb);
                _mpb.SetColor(ColorProperty, _currentColor);
                _renderer.SetPropertyBlock(_mpb);
            }
        }

        public bool IsPainted(Material material)
        {
            if (material == null)
                return true;

            var colorToCheck = material.HasProperty(ColorProperty)
                ? material.GetColor(ColorProperty)
                : Color.white;

            return _currentColor != colorToCheck;
        }
    }
}