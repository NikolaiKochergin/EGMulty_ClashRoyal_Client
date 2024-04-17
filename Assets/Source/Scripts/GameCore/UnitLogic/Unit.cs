using Source.Scripts.GameCore.States;
using Source.Scripts.GameCore.UnitLogic.AI;
using Source.Scripts.GameCore.UnitLogic.States;
using Source.Scripts.GameCore.UnitLogic.UI;
using Source.Scripts.StaticData;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

namespace Source.Scripts.GameCore.UnitLogic
{
    public class Unit : MonoBehaviour, IDamageable
    {
        [SerializeField] private NavMeshAgent _agent;
        [SerializeField] private HealthBar _healthBar;
        [SerializeField] private UnitAnimator _unitAnimator;
        [SerializeField] private AttackBase _attack;
        [SerializeField] private UnitStats _stats;
        
        private FSM _fsm;
        private Health _health;
        private IMover _mover;
        private TargetContainer _target;
        private MeleeUnitBrain _brain;

        public UnitStats Stats => _stats;

        public Transform Transform => transform;

        public float Radius => _stats.ModelRadius;

        public IHealth Health => _health;

        public void Construct(Team enemyTeam)
        {
            _health = new Health(_stats.HealthMaxValue);
            _mover = new NavMeshMover(_agent, _stats);
            _target = new TargetContainer();
            _fsm = new FSMBuilder()
                .Add(new SearchTargetState())
                .Add(new MoveToTargetState(_unitAnimator, _mover, _target))
                .Add(new AttackState(_unitAnimator, _attack, _target))
                .Add(new ChaseState(_unitAnimator, _mover, _target))
                .Add(new VictoryState(_unitAnimator))
                .Add(new DieState(_unitAnimator))
                .Build();
            _brain = new MeleeUnitBrain(this, _fsm, enemyTeam, _target);
        }

        private void Start()
        {
            _healthBar.Initialize(_health);
            _attack.Initialize(_stats);
            _health.Died += OnDied;
            
        }

        private void Update()
        {
            if(_health.CurrentValue != 0)
                _brain.Update();
        }

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
            Handles.color = Color.black;
            Handles.DrawWireDisc(transform.position, Vector3.up, _stats.StopChaseDistance);
            
            Handles.color = Color.red;
            Handles.DrawWireDisc(transform.position, Vector3.up, _stats.StartAttackDistance);
            Handles.color = Color.magenta;
            Handles.DrawWireDisc(transform.position, Vector3.up, _stats.StopAttackDistance);
        }
#endif
    }
}
