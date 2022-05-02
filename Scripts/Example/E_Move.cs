/// <summary>
/// Example state for the example StateMachine implementation.
/// 
/// 02.05.2022 - Bl4ck?
/// </summary>
public class E_Move : ExampleState
{
    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="_blackBoard">Blackboard of the group fsm</param>
    /// <param name="_stateName">Statetype of the state</param>
    public E_Move(ExampleBlackBoard _blackBoard, EExampleState _stateName) : base(_blackBoard, _stateName)
    {
    }

    /// <summary>
    /// Gets called in the fixedupdate.
    /// Moves the transform of the StateMachine.
    /// </summary>
    public override void PhysicsUpdate() => BlackBoard.Transform.position += BlackBoard.Transform.forward;
}

