using Source.Scripts.GameCore.States;
using UnityEngine;

namespace Source.Scripts.GameCore.UnitLogic.States
{
    public class DieState : FSMState
    {
        private readonly Unit _unit;

        public DieState(FSM fsm, Unit unit) : base(fsm)
        {
            _unit = unit;
        }

        public override void Enter()
        {
            Debug.Log($"{_unit.gameObject.name} DIED");
        }
    }
}