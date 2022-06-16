using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
/// <summary>
/// Handles information and evaluation for entries of the BlackBoard.
/// 
/// Bl4ck?
/// </summary>
[System.Serializable]
public class BlackBoardEntry<U> where U : struct, IConvertible
{

    /// <summary>
    /// The type of entry
    /// </summary>
    [field:SerializeField] public U EntryType { get; private set; }

    /// <summary>
    /// The value that the entry should have at start.
    /// </summary>
    [field:SerializeField] public float StartValue { get; private set; }

    /// <summary>
    /// Determines if the entry is a value that should be incremented based on time.
    /// </summary>
    [field:SerializeField] public bool IsTimerValue { get; private set; }

    /// <summary>
    /// The interval after which the timer entry should be incremented in seconds. 
    /// </summary>
    [field:SerializeField] public float IncrementAfterSeconds { get; private set; }

    [SerializeField] private List<Evaluation> evaluations;
    [SerializeField] private bool deactivateInvokation = false;
    
    /// <summary>
    /// The current value of the entry.
    /// </summary>
    public float Value
    {
        get
        {
            return value;
        }
        set
        {
            // This behavior is used here to reduce the amount of invokes
            if (value > this.value)
            {
                this.value = value;
                OnIncrement?.Invoke(this.value);
            }
            else if (value < this.value)
            {
                this.value = value;
                OnDecrement?.Invoke(this.value);
            }
        }
    }

    private float value;
    private event Action<float> OnIncrement;
    private event Action<float> OnDecrement;
    
    /// <summary>
    /// Initializes the entry.
    /// </summary>
    /// <param name="_idOfCreature"></param>
    public void Init(int _idOfCreature)
    {
        Value = StartValue;
        if(evaluations != null && evaluations.Count > 0 && !deactivateInvokation)
            foreach(Evaluation evaluation in evaluations)
                evaluation.Init(this);
    }


    #region Evaluation ------------------------------------------------------------------
    [System.Serializable]
    /// <summary>
    /// Evaluation class which invokes events based on the increasement or decreasement 
    /// of the entries the evaluation class and reduces overhead where possible.
    /// 
    /// It is just used in BlackBoardEntries.
    /// </summary>
    public class Evaluation
    {
        [SerializeField] private EBlackBoardEventEvaluation evaluationMethod;
        [SerializeField] private float invokeValue;
        [Tooltip("Set to true if evaluation case needs to be set to false in order to reinvoke the response")]
        [SerializeField] private bool stopInvokingAfterLimit = true;
        [SerializeField] private UnityEvent response;

        /// <summary>
        /// The evaluation method to be used when changing the value of the entry.
        /// </summary>
        public EBlackBoardEventEvaluation EvaluationMethod { get => evaluationMethod; }

        /// <summary>
        /// The value at which the entry should be invoked.
        /// </summary>
        public float InvokeValue { get => invokeValue; }

        /// <summary>
        /// The response to be executed upon the evaluation.
        /// </summary>
        public UnityEvent Response { get => response; }

        private bool stopInvoking = false;

        /// <summary>
        /// Initializes the evaluation based on the evaluation method.
        /// </summary>
        /// <param name="_entry">The entry to set the evaluation up for</param>
        /// <param name="_idOfCreature">The id of the creature</param>
        public void Init(BlackBoardEntry<U> _entry)
        {
            // This behavior is used here to reduce the amount of invokes
            switch (evaluationMethod)
            {
                case EBlackBoardEventEvaluation.lessThan:
                    _entry.OnDecrement += Validate;
                    break;
                case EBlackBoardEventEvaluation.greaterThan:
                    _entry.OnIncrement += Validate;
                    break;
                default:
                    _entry.OnDecrement += Validate;
                    _entry.OnIncrement += Validate;
                    break;
            }
        }

        /// <summary>
        /// Validates if the response should be executed and reduces overhead by only
        /// invoking when needed. Gets called on increment or decrement.
        /// </summary>
        /// <param name="_value">The current value of the entry</param>
        public void Validate(float _value)
        {
            if (!IsInvokingAllowed(_value))
                return;


            switch (evaluationMethod)
            {
                case EBlackBoardEventEvaluation.lessThan:
                   /*
                    * Only invokes when the value has been decreased in the case of 
                    * the evaluation being based on a less than comparison.
                    */
                    if (_value < invokeValue)
                    {
                        response?.Invoke();
                        // The evaluation is told to stop evlauating upon first invoking if specified
                        if (stopInvokingAfterLimit)
                            stopInvoking = true;
                    }
                    break;
                case EBlackBoardEventEvaluation.greaterThan:
                   /*
                    * Only invokes when the value has been increased in the case of 
                    * the evaluation being based on a greater than comparison.
                    */
                    if (_value > invokeValue)
                    {
                        response?.Invoke();
                        // The evaluation is told to stop evlauating upon first invoking if specified
                        if (stopInvokingAfterLimit)
                            stopInvoking = true;
                    }
                    break;
                case EBlackBoardEventEvaluation.equals:
                   /*
                    * Only invokes when the value has been set to the specified value
                    * in the case of the evaluation being based on a equals comparison.
                    */
                    if (_value == invokeValue)
                        response?.Invoke();
                    break;
                default:
                    // Otherwise just invokes
                    response?.Invoke();
                    break;
            }
        }

        /// <summary>
        /// Checks if invoking is allowed based on the evaluation method
        /// and the stopInvoking variable which has possibly been set before.
        /// Possibly changes the value of the stopInvoking variable aswell.
        /// </summary>
        /// <param name="_value">The value of the entry</param>
        /// <returns>True if invoking is allowed</returns>
        private bool IsInvokingAllowed(float _value)
        {
            if (!stopInvoking)
                return true;

            /*
             * Checks if the value has passed the invoke value again 
             * so that passing the threshold is possible again.
             */
            switch (evaluationMethod)
            {
                case EBlackBoardEventEvaluation.lessThan:
                    if (_value >= InvokeValue)
                    {
                        stopInvoking = false;
                        return true;
                    }
                    else
                        return false;
                case EBlackBoardEventEvaluation.greaterThan:
                    if (_value <= InvokeValue)
                    {
                        stopInvoking = false;
                        return true;
                    }
                    else
                        return false;
                default:
                    return true;
            }
        }
    }
    #endregion ------------------------------------------------------------------
}