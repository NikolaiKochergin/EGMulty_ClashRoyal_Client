using Source.Scripts.GameCore.States;
using UnityEngine;

namespace Source.Scripts.GameCore.UnitLogic.States
{
    public class AttackState : FSMState
    {
        private readonly UnitAnimator _animator;
        private readonly AttackBase _attack;
        private readonly TargetContainer _target;

        private IDamageable _tempTarget;
        private float _time;

        public AttackState(UnitAnimator unitAnimator, AttackBase attack, TargetContainer target)
        {
            _animator = unitAnimator;
            _attack = attack;
            _target = target;
        }

        public override void Enter()
        {
            _animator.transform.LookAt(_target.Damageable.Transform, Vector3.up);
            _animator.ShowIdle();
            _time = 0;
        }

        public override void Update()
        {
            _time += Time.deltaTime;
            if(_time < _attack.Delay)
                return;

            _time -= _attack.Delay;
            _tempTarget = _target.Damageable;
            _animator.transform.LookAt(_target.Damageable.Transform, Vector3.up);
            _animator.ShowAttack(()=> _attack.ApplyTo(_tempTarget));
        }
    }
}