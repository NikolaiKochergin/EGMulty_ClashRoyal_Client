using Source.Scripts.GameCore.States;
using UnityEngine;

namespace Source.Scripts.GameCore.UnitLogic.States
{
    public class AttackState : FSMState
    {
        private const float DamageValue = 1.5f;
        private const float AttackDelay = 1f;
        
        private readonly Unit _unit;
        private readonly Team _enemyTeam;
        
        private IDamageable _target;
        private float _stopAttackDistance;
        private float _time;
        

        public AttackState(FSM fsm, Unit unit, Team enemyTeam) : base(fsm)
        {
            _unit = unit;
            _enemyTeam = enemyTeam;
        }

        public override void Enter()
        {
            _time = 0;
            if (TryFindTarget(out _stopAttackDistance)) 
                _unit.transform.LookAt(_target.Transform.position);
            else
                Fsm.Set<MoveState>();
        }

        public override void Update()
        {
            _time += Time.deltaTime;
            if(_time < AttackDelay)
                return;

            _time -= AttackDelay;
            
            if (_target == null)
            {
                Fsm.Set<MoveState>();
                return;
            }
            
            float distanceToTarget = Vector3.Distance(_unit.transform.position, _target.Transform.position);
            if(distanceToTarget > _stopAttackDistance + _target.Radius)
                Fsm.Set<ChaseState>();
            
            _target.ApplyDamage(DamageValue);
        }

        private bool TryFindTarget(out float stopAttackDistance)
        {
            stopAttackDistance = 0;
            Vector3 unitPosition = _unit.transform.position;
            bool hasEnemy = _enemyTeam.TryGetNearestUnit(unitPosition, out Unit target, out float distance);
            if (hasEnemy && distance - target.Stats.ModelRadius <= _unit.Stats.StartAttackDistance)
            {
                _target = target;
                stopAttackDistance = _unit.Stats.StopAttackDistance + target.Radius;
                return true;
            }

            Tower targetTower = _enemyTeam.GetNearestTower(unitPosition);
            if (Vector3.Distance(unitPosition, targetTower.Transform.position) - targetTower.Radius <=
                _unit.Stats.StartAttackDistance)
            {
                _target = targetTower;
                stopAttackDistance = _unit.Stats.StopAttackDistance + targetTower.Radius;
                return true;
            }

            return false;
        }
    }
}