using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Test.StateMachine
{
    public abstract class FsmState 
    {
        protected readonly Fsm Fsm;

        public FsmState(Fsm fsm)
        {
            Fsm = fsm;
        }

        public abstract void Enter();
        public abstract void Exit();
        public abstract void Update();
    }
}
