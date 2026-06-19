using UnityEngine;

public class StateMachine
{ 
    public State CurrentState { get; private set; }

    public void Initialize(State startingState) //start state
    {
        CurrentState = startingState;
        CurrentState.Enter();
    }
    public void ChangeState(State newState) //end previous state and start next
    {
        CurrentState.Exit();
        CurrentState = newState;
        CurrentState.Enter();
    }
}
