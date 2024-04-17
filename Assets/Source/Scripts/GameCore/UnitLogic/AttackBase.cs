using Source.Scripts.StaticData;
using UnityEngine;

namespace Source.Scripts.GameCore.UnitLogic
{
    public abstract class AttackBase : MonoBehaviour
    {
        public abstract float Delay { get; }

        public abstract void Initialize(UnitStats stats);
        public abstract void ApplyTo(IDamageable target);
    }
}