using Source.Scripts.GameCore.States;
using UnityEngine;

namespace Source.Scripts.GameCore.UnitLogic.States
{
    public class VictoryState : FSMState
    {
        public override void Enter()
        {
            Debug.Log("Victory");
        }
    }
}