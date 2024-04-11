using Source.Scripts.GameCore.States;
using UnityEngine;

namespace Source.Scripts.GameCore.UnitLogic.States
{
    public class VictoryState : FSMState
    {
        public VictoryState(FSM fsm) : base(fsm)
        {
        }

        public override void Enter()
        {
            Debug.Log("Victory");
        }
    }
}