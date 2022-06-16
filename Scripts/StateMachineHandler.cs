using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Gneric handler for a Statemachine concept with a blackboard for handling informations.
/// 
/// 23.03.2022 - Bl4ck?
/// </summary>
/// <typeparam name="T">Enum that contains all states</typeparam>
/// <typeparam name="U">Enum that contains the blackboard entries</typeparam>
public abstract class StateMachineHandler<T,U> : MonoBehaviour where T : struct, IConvertible where U : struct, IConvertible
{
    /// <summary>
    /// The abstract blackboard.
    /// </summary>
    public abstract BlackBoard<T,U> AbstractBlackBoard { get; set; } 

    /// <summary>
    /// The abstract statefactory.
    /// </summary>
    protected abstract IStateFactory<T,U> StateFactory { get; }

    [SerializeField] private List<T> states;
    [SerializeField] protected T standardState;
    [SerializeField] private T startState;
    [SerializeField] protected List<BlackBoardEntry<U>> blackBoardEntries;
    
    protected Dictionary<T, State<T,U>> StatesDict;
    protected StateMachine<U, T, State<T,U>> stateMachine; 
    protected Queue<State<T, U>> StatesQueue;
    protected bool initialized = false;

    #region Initilization -----------------------------------------------------------------
    /// <summary>
    /// Starts initilizing the StateMachine if it is not initialized already.
    /// </summary>
    protected virtual void Start()
    {
        if(!initialized)
            InitStateMachineHandler();
    }

    /// <summary>
    /// Initializes all necessary collections and variables.
    /// </summary>
    protected virtual void InitStateMachineHandler()
    {
        // Initialize BlackBoard
        AbstractBlackBoard = AbstractBlackBoard.Init(this, startState, standardState, blackBoardEntries);

        // Initialize States
        StatesQueue = new Queue<State<T,U>>();
        StatesDict = new Dictionary<T, State<T, U>>();
        foreach (T state in states)
        {
            if (!StatesDict.ContainsKey(state))
                StatesDict.Add(state, StateFactory.CreateState(state, AbstractBlackBoard));
        }

        // Initialize StateMachine
        stateMachine = new StateMachine<U, T, State<T, U>>();
        stateMachine.Initialize(StatesDict[startState]);
        initialized = true;
    }
    #endregion -----------------------------------------------------------------

    #region Updating -----------------------------------------------------------------
    /// <summary>
    /// Updates the current state by the Update and timer values from the blackboard.
    /// </summary>
    protected virtual void Update()
    {
        stateMachine.CurrentState.LogicUpdate();
        AbstractBlackBoard.UpdateTimers();
    }

    /// <summary>
    /// Updates the current state by the FixedUpdate.
    /// </summary>
    protected virtual void FixedUpdate()
    {
        stateMachine.CurrentState.PhysicsUpdate();
    }
    #endregion -----------------------------------------------------------------

    #region Handle States -----------------------------------------------------------------
    /// <summary>
    /// Changes to a desired state if possible.
    /// </summary>
    /// <param name="_state">The deired state</param>
    public virtual void ChangeState(T _state)
    {
        if (StatesDict.ContainsKey(_state) && stateMachine.CurrentState != StatesDict[_state])
        {
            stateMachine.ChangeState(StatesDict[_state]);
            AbstractBlackBoard.CurrentState = _state;
        }
    }

    /// <summary>
    /// Changes to a desired state.
    /// </summary>
    /// <param name="_state">The desired state</param>
    public void ChangeState(State<T,U> _state) => ChangeState(_state.StateName);

    /// <summary>
    /// Enqueues a state.
    /// </summary>
    /// <param name="_state">The desired state</param>
    public void EnqueueState(T _state)
    {
        if (StatesDict.ContainsKey(_state))
            StatesQueue.Enqueue(StatesDict[_state]);
    }

    /// <summary>
    /// Changes either to the next state ot to the standard state.
    /// </summary>
    protected void NextState()
    {
        if (StatesQueue.Count > 0)
            ChangeState(StatesQueue.Dequeue());
        else
            ChangeState(standardState);
    }

    /// <summary>
    /// Clears the states queue.
    /// </summary>
    public void ClearStatesQueue() => StatesQueue.Clear();

    /// <summary>
    /// Clears the states queue and resets.
    /// </summary>
    public void ClearStatesQueueAndReset()
    {
        StatesQueue.Clear();
        NextState();
    }
    #endregion -----------------------------------------------------------------


}
