using UnityEngine;

namespace Source.Scripts.GameCore.UnitLogic
{
    public interface ITarget
    {
        Transform Transform { get; }
        float Radius { get; }
    }
}