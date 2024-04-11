using Source.Scripts.UI;
using UnityEditor;
using UnityEngine;

namespace Source.Scripts.GameCore.UnitLogic
{
    public class Tower : MonoBehaviour , IDamageable
    {
        [SerializeField] private SpriteSlider _healthSlider;
        [SerializeField, Min(0)] private float _healthMaxValue = 20f;
        [SerializeField, Min(0)] private float _radius = 2f;

        private Health _health;
        
        public Transform Transform => transform;
        public float Radius => _radius;
        public IHealth Health => _health;

        private void Start() => Initialize(); 

        private void Initialize()
        {
            _health = new Health(_healthMaxValue);
            _healthSlider.SetFill(_health.CurrentValue/_health.MaxValue);
        }

        public void ApplyDamage(float value)
        {
            _health.ApplyDamage(value);
            _healthSlider.SetFill(_health.CurrentValue/_health.MaxValue);
        }

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            Handles.color = Color.green;
            Handles.DrawWireDisc(transform.position, Vector3.up, _radius);
        }
#endif
    }
}