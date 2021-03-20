using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace NikolayT2DGame
{
    public class SimplePatrolAI
    {
        #region Fields

        private readonly LevelObjectView _view;
        private readonly SimplePatrolAIModel _model;

        #endregion


        #region Class life cycles

        public SimplePatrolAI(LevelObjectView view, SimplePatrolAIModel model)
        {
            _view = view != null ? view : throw new ArgumentNullException(nameof(view));
            _model = model != null ? model : throw new ArgumentNullException(nameof(model));
        }

        public void FixedUpdate()
        {
            var newVelocity = _model.CalculateVelocity(_view.Transform.position) * Time.fixedDeltaTime;
            _view.Rigidbody2D.velocity = newVelocity;
        }

        #endregion
    }
}