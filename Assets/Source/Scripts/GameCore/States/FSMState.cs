namespace Source.Scripts.GameCore.States
{
    public abstract class FSMState
    {
        protected FSMState(FSM fsm) => 
            Fsm = fsm;

        protected FSM Fsm { get; }

        public virtual void Enter() { }
        public virtual void Update() { }
        public virtual void Exit() { }
    }
}