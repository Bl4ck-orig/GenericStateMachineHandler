/// <summary>
/// Example state for the example StateMachine implementation.
/// 
/// 02.05.2022 - Bl4ck?
/// </summary>
public class E_Idle : ExampleState
{
    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="_blackBoard">Blackboard of the group fsm</param>
    /// <param name="_stateName">Statetype of the state</param>
    public E_Idle(ExampleBlackBoard _blackBoard, EExampleState _stateName) : base(_blackBoard, _stateName)
    {
    }
}