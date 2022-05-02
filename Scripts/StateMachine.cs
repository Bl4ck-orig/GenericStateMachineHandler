using System;

/// <summary>
/// Generic statemachine concept class for the StateMachineHandler.
/// 
/// 19.10.2021 - Bl4ck?
/// </summary>
/// <typeparam name="V">Enum that determines the blackboard entry</typeparam>
/// <typeparam name="U">Enum that determines the state</typeparam>
/// <typeparam name="T">The type of state</typeparam>
public class StateMachine<V, U, T> where V : struct, IConvertible where U : struct, IConvertible where T : State<U, V>
{
    /// <summary>
    /// The state that is running.
    /// </summary>
    public T CurrentState { get; protected set; }

    /// <summary>
    /// The state that was active before the current state
    /// </summary>
    public T PreviousState { get; protected set; }

    /// <summary>
    /// Initializes a state and enters it
    /// </summary>
    /// <param name="_startingState">The desired state</param>
    public void Initialize(T _startingState)
    {
        CurrentState = _startingState;
        CurrentState.Enter();
    }

    /// <summary>
    /// Changes the statemachine to a state
    /// </summary>
    /// <param name="_newState">The desired state</param>
    public void ChangeState(T _newState)
    {
        PreviousState = CurrentState;
        CurrentState.Exit();
        CurrentState = _newState;
        CurrentState.Enter();
    }
}
