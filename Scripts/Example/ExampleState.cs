/// <summary>
/// Abstract state for the example StateMachine implementation.
/// 
/// 02.05.2022 - Bl4ck?
/// </summary>
public abstract class ExampleState : State<EExampleState, EBlackBoardEntryExample>
{
    /// <summary>
    /// Blackboard of the StateMachine.
    /// </summary>
    public override BlackBoard<EExampleState, EBlackBoardEntryExample> AbstractBlackboard { get => BlackBoard; }

    protected ExampleBlackBoard BlackBoard;

    /// <summary>
    /// Constructor for the state.
    /// </summary>
    /// <param name="_blackBoard">The blackboard to use</param>
    /// <param name="_stateName">The desired state</param>
    protected ExampleState(ExampleBlackBoard _blackBoard, EExampleState _stateName)
    {
        BlackBoard = _blackBoard;
        StateName = _stateName;
    }

    /// <summary>
    /// Gets called when entering a a state.
    /// </summary>
    protected override void Entering() { }

    /// <summary>
    /// Gets called when exiting a a state.
    /// </summary>
    protected override void Exiting() { }

    /// <summary>
    /// Gets called in the update.
    /// </summary>
    public override void LogicUpdate() { }

    /// <summary>
    /// Gets called in the fixedupdate.
    /// </summary>
    public override void PhysicsUpdate() { }
}

