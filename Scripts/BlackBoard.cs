using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handles variables for the StateMachineHandler.
/// 
/// Timer values are incremented in the blackboard. These can be set from the inspector 
/// with a corresponding evaluation method which can invoke actions like changing state.
/// 
/// 23.03.2022 - Bl4ck?
/// </summary>
/// <typeparam name="T">Enum that determines the state</typeparam>
/// <typeparam name="U">Enum that determines the blackboard entry</typeparam>
public abstract class BlackBoard<T, U> where T : struct, IConvertible where U : struct, IConvertible
{
    public static int amountOfBlackBoards = 0;

    public int Id { get => id; set => id = value; }
    public T CurrentState { get => currentState; set => currentState = value; }
    public T StandardState { get => standardState; set => standardState = value; }
    public T StartState { get => startState; set => startState = value; }

    private int id;
    private T currentState;
    private T standardState;
    private T startState;

    private Dictionary<U, BlackBoardEntry<U>> entryDict;
    private Dictionary<U, float> timerValues;

    private float timer = 0f;
    private int secondsPassed = 0;
    
    /// <summary>
    /// Initializes the blackboard. Gets called in the StateMachineHandler.
    /// </summary>
    /// <param name="_stateMachineHandler">StateMachineHandler</param>
    /// <param name="_startState">The state that the statemachine should be started in</param>
    /// <param name="_standardState">The standard state for the state machine to be in</param>
    /// <param name="_blackBoardEntries">List of entries for the BlackBoard</param>
    /// <returns>The initilized BlackBoard based on the parameters</returns>
    public BlackBoard<T,U> Init(StateMachineHandler<T,U> _stateMachineHandler, T _startState, T _standardState, List<BlackBoardEntry<U>> _blackBoardEntries)
    {
        Id = amountOfBlackBoards++;
        StartState = _startState;
        StandardState = _standardState;
        CurrentState = StartState;

        entryDict = new Dictionary<U, BlackBoardEntry<U>>();
        timerValues = new Dictionary<U, float>();

        // Initialize the BlackBoardEntries by setting up evaluations for each entry and setting up timers for values if needed
        foreach (BlackBoardEntry<U> entry in _blackBoardEntries)
        {
            if (entryDict.ContainsKey(entry.EntryType))
                Debug.LogError("Duplicate blackboard entry in list!");
            else
            {
                entry.Init(Id);
                entryDict.Add(entry.EntryType, entry);
                if (entry.IsTimerValue)
                    timerValues.Add(entry.EntryType, entry.IncrementAfterSeconds);
            }
        }

        // Initializes all other values for deriving classes
        InitNonFloats(_stateMachineHandler);

        return this;
    }

    /// <summary>
    /// Initializes all other values for deriving classes
    /// </summary>
    /// <param name="_stateMachineHandler">StateMachineHandler</param>
    public abstract void InitNonFloats(StateMachineHandler<T,U> _stateMachineHandler);

    /// <summary>
    /// Increments the timer values
    /// </summary>
    public void UpdateTimers()
    {
        secondsPassed = (int)Mathf.Floor(timer);

        // Only increments on each second in order to safe resources
        if (secondsPassed < Mathf.Floor(timer + Time.deltaTime))
        {
            foreach (KeyValuePair<U, float> entry in timerValues)
            {
                if (secondsPassed % entry.Value == 0)
                    IncrementValue(entry.Key);
            }
        }
        timer += Time.deltaTime;
    }


    #region Get/Set Values ------------------------------------------------------------------
    /// <summary>
    /// Sets the value of the the blackboard entry to a specified amount.
    /// </summary>
    /// <param name="_entry">The entry to set the value of</param>
    /// <param name="_value">The desired value</param>
    public void SetValue(U _entry, float _value) => entryDict[_entry].Value = _value;

    /// <summary>
    /// Increments the desired value.
    /// </summary>
    /// <param name="_entry">The entry to be incremented</param>
    public void IncrementValue(U _entry) => entryDict[_entry].Value++;

    /// <summary>
    /// Increases the desired value by a specified amount.
    /// </summary>
    /// <param name="_entry">The entry to be increased</param>
    /// <param name="_amount">The amount of increasement</param>
    public void IncreaseValue(U _entry, float _amount) => entryDict[_entry].Value += _amount;

    /// <summary>
    /// Decrements the desired value.
    /// </summary>
    /// <param name="_entry">The entry to be decremented</param>
    public void DecrementValue(U _entry) => entryDict[_entry].Value--;

    /// <summary>
    /// Reduces the desired value by a specified amount.
    /// </summary>
    /// <param name="_entry">The entry to be reduced</param>
    /// <param name="_amount">The amount of reducement</param>
    public void ReduceValue(U _entry, float _amount) => entryDict[_entry].Value -= _amount;


    /// <summary>
    /// Gets a value from the blackboard.
    /// </summary>
    /// <param name="_entry">The desired entry</param>
    /// <returns>The value of the entry</returns>
    public float GetValue(U _entry) => entryDict[_entry].Value;


    /// <summary>
    /// Gets a value from the blackboard as bool.
    /// </summary>
    /// <param name="_entry">The desired entry</param>
    /// <returns>The value of the entry as bool</returns>
    public bool GetValueAsBool(U _entry) => entryDict[_entry].Value > 0;
    #endregion ------------------------------------------------------------------------------------
}

