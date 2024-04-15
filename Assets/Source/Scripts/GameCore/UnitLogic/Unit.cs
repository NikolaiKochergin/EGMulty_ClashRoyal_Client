using System;
using System.Collections.Generic;
using Source.Scripts.GameCore.States;
using Source.Scripts.GameCore.UnitLogic.States;
using Source.Scripts.StaticData;
using Source.Scripts.UI;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

namespace Source.Scripts.GameCore.UnitLogic
{
    public class Unit : MonoBehaviour , IDamageable
    {
        [SerializeField] private NavMeshAgent _agent;
        [SerializeField] private SpriteSlider _healthSlider;
        [SerializeField] private UnitStats _stats;
        
        private FSM _fsm;
        private Health _health;
        private Team _selfTeam;
        private Team _enemyTeam;

        public UnitStats Stats => _stats;

        public Transform Transform => transform;

        public float Radius => _stats.ModelRadius;

        public IHealth Health => _health;

        public void Construct(Team selfTeam, Team enemyTeam)
        {
            _selfTeam = selfTeam;
            _enemyTeam = enemyTeam;
            _health = new Health(_stats.HealthMaxValue);
            _fsm = new FSMBuilder()
                .Add(new MoveState(this, _agent, _enemyTeam))
                .Add(new AttackState(this, _enemyTeam))
                .Add(new ChaseState(this, _agent, _enemyTeam))
                .Add(new VictoryState())
                .Add(new DieState(this))
                .Build();
        }

        private void Start()
        {
            _agent.stoppingDistance = _stats.StartAttackDistance;
            _agent.speed = _stats.Speed;
            _agent.radius = _stats.ModelRadius;
            
            _healthSlider.SetFill(_health.CurrentValue/_health.MaxValue);
            _health.Died += OnDied;
            
            _fsm.Set<MoveState>();
        }

        private void Update() => 
            _fsm.Update();

        public void ApplyDamage(float value)
        {
            _health.ApplyDamage(value);
            _healthSlider.SetFill(_health.CurrentValue/_health.MaxValue);
        }

        private void OnDied()
        {
            _health.Died -= OnDied;
            _selfTeam.Remove(this);
            _fsm.Set<DieState>();
            Destroy(gameObject);
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
