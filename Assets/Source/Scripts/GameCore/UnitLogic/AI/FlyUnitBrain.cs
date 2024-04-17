﻿using Source.Scripts.GameCore.States;
using Source.Scripts.GameCore.UnitLogic.States;
using UnityEngine;

namespace Source.Scripts.GameCore.UnitLogic.AI
{
    public class FlyUnitBrain
    {
        private readonly UnitBase _unit;
        private readonly FSM _fsm;
        private readonly Team _enemyTeam;
        private readonly TargetContainer _target;
        
        public FlyUnitBrain(UnitBase unit, FSM fsm, Team enemyTeam, TargetContainer target)
        {
            _unit = unit;
            _fsm = fsm;
            _enemyTeam = enemyTeam;
            _target = target;
            _unit.Health.Died += OnDied;
        }

        public void Update()
        {
            switch (_fsm.CurrentState)
            {
                case SearchTargetState:
                    if (_enemyTeam.TryGetNearestTower(_unit.Transform.position, out _target.Damageable) ||
                        _enemyTeam.TryGetNearestUnit(_unit.Transform.position, out _target.Damageable))
                    {
                        _fsm.Set<MoveToTargetState>();
                        break;
                    }

                    _target.Damageable = null;
                    _fsm.Set<VictoryState>();
                    break;
                case MoveToTargetState:
                    if (_target.Damageable.Health.CurrentValue == 0)
                    {
                        _fsm.Set<SearchTargetState>();
                        break;
                    }
                    
                    if (_unit.Stats.StartAttackDistance + _target.Damageable.Radius >= DistanceTo(_target.Damageable.Transform))
                    {
                        _fsm.Set<AttackState>();
                        break;
                    }

                    if (_enemyTeam.TryGetNearestUnit(_unit.transform.position, out IDamageable target) == false)
                        break;

                    if (_unit.Stats.StartChaseDistance + target.Radius >= DistanceTo(target.Transform))
                    {
                        _target.Damageable = target;
                        _fsm.Set<ChaseState>();
                    }
                    break;
                case AttackState:
                    if (_target.Damageable.Health.CurrentValue == 0)
                    {
                        _fsm.Set<SearchTargetState>();
                        break;
                    }
                    
                    if(_unit.Stats.StopAttackDistance + _target.Damageable.Radius < DistanceTo(_target.Damageable.Transform))
                        _fsm.Set<ChaseState>();
                    break;
                case ChaseState:
                    if (_target.Damageable.Health.CurrentValue == 0 
                        || _unit.Stats.StopChaseDistance + _target.Damageable.Radius < DistanceTo(_target.Damageable.Transform))
                    {
                        _fsm.Set<SearchTargetState>();
                        break;
                    }
                    
                    if(_unit.Stats.StartAttackDistance + _target.Damageable.Radius >= DistanceTo(_target.Damageable.Transform))
                        _fsm.Set<AttackState>();
                    break;
                case VictoryState:
                    break;
                case DieState:
                    break;
                default:
                    _fsm.Set<SearchTargetState>();
                    break;
            }
            
            _fsm.Update();
        }

        private void OnDied()
        {
            _unit.Health.Died -= OnDied;
            _fsm.Set<DieState>();
        }

        private float DistanceTo(Transform transform) => 
            Vector3.Distance(_unit.Transform.position, transform.position);
    }
}