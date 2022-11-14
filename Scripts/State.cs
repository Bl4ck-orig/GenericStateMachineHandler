using System;
using UnityEngine;

namespace FSM
{
    public abstract class State<T> where T : struct, IConvertible
    {
        public float StartTime { get; set; }

        public T StateType { get; set; }

        public bool IsExitingState { get; set; }

        private int id;
        private EScriptGroup scriptGroup;
        private IDebugInformation debugInfo;
        private string debugString;

        protected State(T _stateType, EScriptGroup _scriptGroup, int _id, IDebugInformation _debugInfo = null)
        {
            StateType = _stateType;
            id = _id;
            scriptGroup = _scriptGroup;
            debugInfo = _debugInfo;
        }

        #region Entering -----------------------------------------------------------------
        public void Enter()
        {
            StartTime = Time.time;
            IsExitingState = false;
            Entering();
        }

        protected abstract void Entering();

        #endregion -----------------------------------------------------------------

        #region Updating -----------------------------------------------------------------
        public abstract void LogicUpdate();

        public abstract void PhysicsUpdate();
        #endregion -----------------------------------------------------------------

        #region Exiting -----------------------------------------------------------------
        public void Exit()
        {
            IsExitingState = true;
            Exiting();
        }

        protected abstract void Exiting();
        #endregion -----------------------------------------------------------------
    }
}
