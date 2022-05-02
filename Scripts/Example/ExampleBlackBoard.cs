using UnityEngine;

/// <summary>
/// Blackboard for the example StateMachine implementation.
/// 
/// 02.05.2022 - Bl4ck?
/// </summary>
public class ExampleBlackBoard : BlackBoard<EExampleState, EBlackBoardEntryExample>
{
    /// <summary>
    /// Transform of the example StateMachine implementation.
    /// </summary>
    public Transform Transform { get; set; }

    /// <summary>
    /// Intializes all values that are not set in the parent class based on the StateMachineHandler (the group in this case).
    /// </summary>
    /// <param name="_stateMachineHandler">The StateMachineHandler to get the values from</param>
    public override void InitNonFloats(StateMachineHandler<EExampleState, EBlackBoardEntryExample> _stateMachineHandler)
    {
        Transform = _stateMachineHandler.transform;
    }
}


