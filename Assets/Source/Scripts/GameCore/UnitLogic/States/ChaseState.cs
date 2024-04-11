using Source.Scripts.GameCore.States;
using UnityEngine;
using UnityEngine.AI;

namespace Source.Scripts.GameCore.UnitLogic.States
{
    public class ChaseState : FSMState
    {
        private readonly Unit _unit;
        private readonly NavMeshAgent _agent;
        private readonly Team _enemyTeam;
        private ITarget _targetUnit;

        private float _startAttackDistance;

        public ChaseState(FSM fsm, Unit unit, NavMeshAgent agent, Team enemyTeam) : base(fsm)
        {
            _unit = unit;
            _agent = agent;
            _enemyTeam = enemyTeam;
        }

        public override void Enter()
        {
            _agent.isStopped = false;
            if (_enemyTeam.TryGetNearestUnit(_unit.Transform.position, out Unit targetUnit, out float distance))
            {
                _targetUnit = targetUnit;
                _startAttackDistance = _unit.Stats.StartAttackDistance + _targetUnit.Radius;
            }
        }

        public override void Update()
        {
            if (_targetUnit == null)
            {
                Fsm.Set<MoveState>();
                return;
            }

            float distanceToTarget = Vector3.Distance(_unit.Transform.position, _targetUnit.Transform.position);
            
            if(distanceToTarget > _unit.Stats.StopChaseDistance + _targetUnit.Radius)
                Fsm.Set<MoveState>();
            else if(distanceToTarget <= _startAttackDistance) 
                Fsm.Set<AttackState>();
            else
                _agent.SetDestination(_targetUnit.Transform.position);
        }

        public override void Exit() => 
            _agent.isStopped = true;
    }
}