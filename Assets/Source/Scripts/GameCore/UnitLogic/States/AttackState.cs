using Source.Scripts.GameCore.States;
using UnityEngine;

namespace Source.Scripts.GameCore.UnitLogic.States
{
    public class AttackState : FSMState
    {
        
        private readonly Unit _unit;
        private readonly UnitAnimator _animator;
        private readonly Team _enemyTeam;
        
        private IDamageable _target;
        private float _stopAttackDistance;
        private float _time;

        public AttackState(Unit unit, UnitAnimator unitAnimator, Team enemyTeam)
        {
            _unit = unit;
            _animator = unitAnimator;
            _enemyTeam = enemyTeam;
        }

        public override void Enter()
        {
            _animator.ShowIdle();
            _time = 0;
            if (TryFindTarget(out _stopAttackDistance)) 
                _unit.transform.LookAt(_target.Transform.position);
            else
                Fsm.Set<MoveState>();
        }

        public override void Update()
        {
            if (_target.Health.CurrentValue == 0)
            {
                Fsm.Set<MoveState>();
                return;
            }
            
            _time += Time.deltaTime;
            if(_time < _unit.Stats.AttackDelay)
                return;

            _time -= _unit.Stats.AttackDelay;
            
            float distanceToTarget = Vector3.Distance(_unit.transform.position, _target.Transform.position);
            if(distanceToTarget > _stopAttackDistance + _target.Radius)
                Fsm.Set<ChaseState>();
            
            _animator.ShowAttack(()=> _target.ApplyDamage(_unit.Stats.Damage));
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