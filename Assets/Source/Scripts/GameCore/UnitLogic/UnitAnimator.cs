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

        public void ShowIdle() => 
            _animator.SetTrigger(Idle);

        public void ShowAttack() =>
            _animator.SetTrigger(Attack);

        public void ShowRun() => 
            _animator.SetTrigger(Run);

        public void ShowVictory() => 
            _animator.SetTrigger(Victory);

        public void ShowDie()
        {
            _animator.SetTrigger(Die);
        }
    }
}
