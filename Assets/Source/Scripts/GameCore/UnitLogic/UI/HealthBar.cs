using UnityEngine;

namespace Source.Scripts.GameCore.UnitLogic.UI
{
    public class HealthBar : MonoBehaviour
    {
        [SerializeField] private SpriteSlider _spriteSlider;
        
        private IHealth _health;

        public void Initialize(IHealth health)
        {
            _health = health;
            DisplayHealth();
            _health.Changed += DisplayHealth;
        }

        private void OnDestroy() => 
            _health.Changed -= DisplayHealth;

        private void DisplayHealth() => 
            _spriteSlider.SetFill(_health.CurrentValue/_health.MaxValue);
    }
}