using UnityEngine;

/// <summary>
/// State factory for producing states for the example StateMachine implementation.
/// 
/// 02.05.2022 - Bl4ck?
/// </summary>
public class ExampleStateFactory : IStateFactory<EExampleState, EBlackBoardEntryExample>
{

    /// <summary>
    /// Creates states based on the state specified.
    /// </summary>
    /// <param name="_stateType">Type of the state</param>
    /// <param name="_blackBoard">The blackboard to use for initialization</param>
    /// <returns>The desired state</returns>
    public State<EExampleState, EBlackBoardEntryExample> CreateState(EExampleState _stateType, BlackBoard<EExampleState, EBlackBoardEntryExample> _blackBoard)
    {
        switch (_stateType)
        {
            case EExampleState.Move:
                return new E_Move((ExampleBlackBoard)_blackBoard, _stateType);
            case EExampleState.Idle:
                return new E_Idle((ExampleBlackBoard)_blackBoard, _stateType);
            default:
                Debug.LogError("State unknown!");
                return null;
        }
    }
}
