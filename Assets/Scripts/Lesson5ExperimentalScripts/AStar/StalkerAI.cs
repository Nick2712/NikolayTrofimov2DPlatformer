using Pathfinding;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace NikolayT2DGame
{
    public class StalkerAI
    {
        #region Fields
        private readonly LevelObjectView _view;
        private readonly StalkerAIModel _model;
        private readonly Seeker _seeker;
        private readonly Transform _target;
        private readonly Vector2 _nest;
        private readonly float _maxSqrDistanceToStalking;
        private float _distance;
        private Vector2 _currentTarget;
        #endregion

        #region Class life cycles

        public StalkerAI(LevelObjectView view, StalkerAIModel model, Seeker seeker, 
            Transform target, float maxSqrDistanceToStalking)
        {
            _view = view != null ? view : throw new ArgumentNullException(nameof(view));
            _model = model != null ? model : throw new ArgumentNullException(nameof(model));
            _seeker = seeker != null ? seeker : throw new ArgumentNullException(nameof(seeker));
            _target = target != null ? target : throw new ArgumentNullException(nameof(target));
            _nest = view.Transform.position;
            _maxSqrDistanceToStalking = maxSqrDistanceToStalking;
        }

        #endregion

        #region Methods

        public void FixedUpdate()
        {
            _distance = (_target.position - _view.transform.position).sqrMagnitude;
            if(_distance > _maxSqrDistanceToStalking)
            {
                _currentTarget = _nest;
            }
            else
            {
                _currentTarget = _target.position;
            }
            var newVelocity = _model.CalculateVelocity(_view.Transform.position) * 
                Time.fixedDeltaTime;
            _view.Rigidbody2D.velocity = newVelocity;
        }

        public void RecalculatePath()
        {
            if (_seeker.IsDone())
            {
                _seeker.StartPath(_view.Rigidbody2D.position, _currentTarget, OnPathComplete);
            }
        }

        private void OnPathComplete(Path p)
        {
            if (p.error) return;
            _model.UpdatePath(p);
        }

        #endregion
    }
}