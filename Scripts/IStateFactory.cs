using System;

/// <summary>
/// Interface for producing states for a StateMachineHandler.
/// 
/// 06.01.2022 - Bl4ck?
/// </summary>
public interface IStateFactory<T, U> where T : struct, IConvertible where U : struct, IConvertible
{
    /// <summary>
    /// Creates states based on the state specified.
    /// </summary>
    /// <param name="_stateType">Type of the state</param>
    /// <param name="_blackBoard">The blackboard to use for initialization</param>
    /// <returns>The desired state</returns>
    public State<T, U> CreateState(T _stateType, BlackBoard<T,U> _blackBoard);
}
