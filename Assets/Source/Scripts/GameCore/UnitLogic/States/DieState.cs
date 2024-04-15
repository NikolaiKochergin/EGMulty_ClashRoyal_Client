using Source.Scripts.GameCore.States;
using UnityEngine;

namespace Source.Scripts.GameCore.UnitLogic.States
{
    public class DieState : FSMState
    {
        private readonly Unit _unit;

        public DieState(Unit unit) => 
            _unit = unit;

        public override void Enter()
        {
            Debug.Log($"{_unit.gameObject.name} DIED");
            Object.Destroy(_unit.gameObject);
        }
    }
}