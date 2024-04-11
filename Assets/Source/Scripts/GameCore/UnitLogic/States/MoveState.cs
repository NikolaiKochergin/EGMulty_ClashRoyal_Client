﻿using Source.Scripts.GameCore.States;
using UnityEngine;
using UnityEngine.AI;

namespace Source.Scripts.GameCore.UnitLogic.States
{
    public class MoveState : FSMState
    {
        private readonly Unit _unit;
        private readonly NavMeshAgent _agent;
        private readonly Team _enemyTeam;

        private ITarget _nearestTower;

        public MoveState(FSM fsm, Unit unit, NavMeshAgent agent, Team enemyTeam) : base(fsm)
        {
            _unit = unit;
            _agent = agent;
            _enemyTeam = enemyTeam;
        }

        public override void Enter()
        {
            _nearestTower = _enemyTeam.GetNearestTower(_unit.Transform.position);
            _agent.isStopped = false;
            _agent.SetDestination(_nearestTower.Transform.position);
        }

        public override void Update()
        {
            if (!TryAttackTower() && !TryAttackUnit()) 
                return;
            
            Fsm.Set<AttackState>();
        }

        public override void Exit() =>
            _agent.isStopped = true;

        private bool TryAttackTower()
        {
            float distanceToTarget = Vector3.Distance(_unit.Transform.position, _nearestTower.Transform.position);
            return distanceToTarget > _unit.Stats.StartAttackDistance + _nearestTower.Radius == false;
        }

        private bool TryAttackUnit()
        {
            if (_enemyTeam.TryGetNearestUnit(_unit.Transform.position, out Unit nearestUnit, out float distance) == false)
                return false;
            
            if (_unit.Stats.StartAttackDistance + nearestUnit.Radius >= distance)
                return true;

            if (_unit.Stats.StartChaseDistance + nearestUnit.Radius >= distance == false) 
                return false;
            
            Fsm.Set<ChaseState>();
            return false;
        }
    }
}