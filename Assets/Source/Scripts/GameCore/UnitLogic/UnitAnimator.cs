using System;
using UnityEngine;

namespace Source.Scripts.GameCore.UnitLogic
{
    public class UnitAnimator : MonoBehaviour
    {
        private static readonly int Idle = Animator.StringToHash("Idle");
        private static readonly int Attack = Animator.StringToHash("Attack");
        private static readonly int Run = Animator.StringToHash("Run");
        private static readonly int Victory = Animator.StringToHash("Victory");
        private static readonly int Die = Animator.StringToHash("Die");
        
        [SerializeField] private Animator _animator;

        private Action _onAnimationTriggerCallback;

        public void ShowIdle() => 
            _animator.SetTrigger(Idle);

        public void ShowAttack(Action onAttackHappenedCallback = null)
        {
            _onAnimationTriggerCallback = onAttackHappenedCallback;
            _animator.ResetTrigger(Run);
            _animator.SetTrigger(Attack);
        }

        public void ShowRun() => 
            _animator.SetTrigger(Run);

        public void ShowVictory() => 
            _animator.SetTrigger(Victory);

        public void ShowDie(Action onDieAnimationOverCallback = null)
        {
            _onAnimationTriggerCallback = onDieAnimationOverCallback;
            _animator.SetTrigger(Die);
        }

        private void Handle_AttackAnimationTrigger() => 
            _onAnimationTriggerCallback?.Invoke();

        private void Handle_DieAnimationOver() => 
            _onAnimationTriggerCallback?.Invoke();
    }
}
