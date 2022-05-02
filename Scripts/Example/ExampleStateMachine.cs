/// <summary>
/// Example StateMachine implementation.
/// 
/// Most of the StateMachine will be set up from the inspector.
/// Changing states is also possible by using the blackboard entries and evaluations.
/// 
/// This example statemachine will wait 5 seconds and then change to the goto state.
/// 
/// 02.05.2022 - Bl4ck?
/// </summary>
public class ExampleStateMachine : StateMachineHandler<EExampleState, EBlackBoardEntryExample>
{
    /// <summary>
    /// The blackboard of the group in abstract.
    /// </summary>
    public override BlackBoard<EExampleState, EBlackBoardEntryExample> AbstractBlackBoard { get => blackBoard; set => blackBoard = (ExampleBlackBoard)value; }

    /// <summary>
    /// The statefactory for producing the states.
    /// </summary>
    protected override IStateFactory<EExampleState, EBlackBoardEntryExample> StateFactory { get => groupStateFactory; }

    private ExampleBlackBoard blackBoard = new ExampleBlackBoard();
    private ExampleStateFactory groupStateFactory = new ExampleStateFactory();


    /// <summary>
    /// Gets called from the evaluation in the inspector.
    /// </summary>
    public void OnIdleTimeExceededLimit() => ChangeState(EExampleState.Move);
}
