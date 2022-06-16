using System;
using UnityEngine;

/// <summary>
/// Abstract state for StateMachineHandler
/// 
/// 06.01.2022 - Bl4ck?
/// </summary>
/// <typeparam name="T">Enum that determines the state</typeparam>
/// <typeparam name="U">Enum that determines the blackboard entry</typeparam>
public abstract class State<T,U> where T : struct, IConvertible where U : struct, IConvertible
{
    /// <summary>
    /// The point of time in which the state was started.
    /// </summary>
    public float StartTime { get; set; }

    /// <summary>
    /// The name of the state.
    /// </summary>
    public T StateName { get; set; }

    /// <summary>
    /// The blackboard to use for.
    /// </summary>
    public abstract BlackBoard<T,U> AbstractBlackboard { get; }

    /// <summary>
    /// Determines if the state is leaving.
    /// </summary>
    public bool IsExitingState { get; set; }

    #region Entering / Exiting -----------------------------------------------------------------
    /// <summary>
    /// Gets called when entering the state.
    /// </summary>
    public void Enter()
    {
        AbstractBlackboard.CurrentState = this;
        StartTime = Time.time;
        IsExitingState = false;
        Entering();
    }

    /// <summary>
    /// Abstract method which gets called when enter finished.
    /// </summary>
    protected abstract void Entering();

    /// <summary>
    /// Gets called when exitign the state.
    /// </summary>
    public void Exit()
    {
        IsExitingState = true;
        Exiting();
    }

    /// <summary>
    /// Abstract method which gets called when exit finished.
    /// </summary>
    protected abstract void Exiting();
    #endregion -----------------------------------------------------------------

    #region Updating -----------------------------------------------------------------
    /// <summary>
    /// Gets called in an update function
    /// </summary>
    public abstract void LogicUpdate();

    /// <summary>
    /// Gets called in a fixedupdate function
    /// </summary>
    public abstract void PhysicsUpdate();
    #endregion -----------------------------------------------------------------
}
