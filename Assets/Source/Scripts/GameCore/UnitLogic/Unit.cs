using Source.Scripts.GameCore.States;
using Source.Scripts.GameCore.UnitLogic.States;
using Source.Scripts.GameCore.UnitLogic.UI;
using Source.Scripts.StaticData;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

namespace Source.Scripts.GameCore.UnitLogic
{
    public class Unit : MonoBehaviour , IDamageable
    {
        [SerializeField] private NavMeshAgent _agent;
        [SerializeField] private HealthBar _healthBar;
        [SerializeField] private UnitAnimator _unitAnimator;
        [SerializeField] private UnitStats _stats;
        
        private FSM _fsm;
        private Health _health;

        public UnitStats Stats => _stats;

        public Transform Transform => transform;

        public float Radius => _stats.ModelRadius;

        public IHealth Health => _health;

        public void Construct(Team enemyTeam)
        {
            _health = new Health(_stats.HealthMaxValue);
            _fsm = new FSMBuilder()
                .Add(new MoveState(this, _unitAnimator, _agent, enemyTeam))
                .Add(new AttackState(this, _unitAnimator, enemyTeam))
                .Add(new ChaseState(this, _unitAnimator, _agent, enemyTeam))
                .Add(new VictoryState(_unitAnimator))
                .Add(new DieState(_unitAnimator))
                .Build();
        }

        private void Start()
        {
            _agent.stoppingDistance = _stats.StartAttackDistance;
            _agent.speed = _stats.Speed;
            _agent.radius = _stats.ModelRadius;
            
            _healthBar.Initialize(_health);
            _health.Died += OnDied;
            
            _fsm.Set<MoveState>();
        }

        private void Update() => 
            _fsm.Update();

        public void ApplyDamage(float value) => 
            _health.ApplyDamage(value);

        private void OnDied()
        {
            _health.Died -= OnDied;
            _fsm.Set<DieState>();
        }

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            Handles.color = Color.blue;
            Handles.DrawWireDisc(transform.position, Vector3.up, _stats.StartChaseDistance);
            Handles.color = Color.cyan;
            Handles.DrawWireDisc(transform.position, Vector3.up, _stats.StopChaseDistance);
            
            Handles.color = Color.red;
            Handles.DrawWireDisc(transform.position, Vector3.up, _stats.StartAttackDistance);
            Handles.color = Color.yellow;
            Handles.DrawWireDisc(transform.position, Vector3.up, _stats.StopAttackDistance);
        }
#endif
    }
}
