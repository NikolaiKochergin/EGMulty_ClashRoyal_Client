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
        private Team _selfTeam;

        public Transform Transform => transform;
        public float Radius => _radius;
        public IHealth Health => _health;

        public void Construct(Team selfTeam)
        {
            _selfTeam = selfTeam;
            _health = new Health(_healthMaxValue);
        }

        private void Start()
        {
            _healthSlider.SetFill(_health.CurrentValue/_health.MaxValue);
            _health.Died += OnDied;
        }

        public void ApplyDamage(float value)
        {
            _health.ApplyDamage(value);
            _healthSlider.SetFill(_health.CurrentValue/_health.MaxValue);
        }

        private void OnDied()
        {
            _health.Died -= OnDied;
            _selfTeam.Remove(this);
            Destroy(gameObject);
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