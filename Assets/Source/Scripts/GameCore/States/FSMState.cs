namespace Source.Scripts.GameCore.States
{
    public abstract class FSMState
    {
        protected FSM Fsm { get; private set; }

        public void Bind(FSM fsm) => Fsm = fsm;
        public virtual void Enter() { }
        public virtual void Update() { }
        public virtual void Exit() { }
    }
}