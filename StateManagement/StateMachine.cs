using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.StateManagement
{
    public class StateMachine
    {
        public State CurrentState { get; private set; }

        public void Init(State firstState)
        {
            CurrentState = firstState;
        }

        public void SetState(State nextState)
        {
            CurrentState?.Exit();
            CurrentState = nextState;
            CurrentState.Enter();
        }
    }
}
