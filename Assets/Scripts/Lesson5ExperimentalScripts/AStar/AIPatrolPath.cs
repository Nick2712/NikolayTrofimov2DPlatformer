using Pathfinding;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace NikolayT2DGame
{
    public class AIPatrolPath : AIPath
    {
        #region Events

        public new event EventHandler TargetReached;

        #endregion


        #region Inheritance

        public override void OnTargetReached()
        {
            base.OnTargetReached();
            DispatchTargetReached();
        }

        #endregion


        #region Methods

        protected virtual void DispatchTargetReached()
        {
            TargetReached?.Invoke(this, EventArgs.Empty);
        }

        #endregion
    }

}