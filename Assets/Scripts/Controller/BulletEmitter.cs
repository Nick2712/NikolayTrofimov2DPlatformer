﻿using System.Collections.Generic;
using UnityEngine;


namespace NikolayT2DGame
{
    public class BulletsEmitter
    {
        private const float _delay = 1;
        private const float _startSpeed = -2;

        private readonly List<PhysicsBullet> _bullets;
        private readonly Transform _transform;

        private int _currentIndex;
        private float _timeTillNextBullet;

        public BulletsEmitter(List<PhysicsBullet> bulletViews, Transform transform)
        {
            _transform = transform;
            _bullets = bulletViews;
        }

        public void Update()
        {
            if (_timeTillNextBullet > 0)
            {
                _timeTillNextBullet -= Time.deltaTime;
            }
            else
            {
                _timeTillNextBullet = _delay;
                _bullets[_currentIndex].Throw(_transform.position, 
                    _transform.up * _startSpeed);
                _currentIndex++;
                if (_currentIndex >= _bullets.Count) _currentIndex = 0;
            }
        }
    }
}