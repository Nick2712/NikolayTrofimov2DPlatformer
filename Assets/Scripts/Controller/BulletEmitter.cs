using System.Collections.Generic;
using UnityEngine;


namespace NikolayT2DGame
{
    public class BulletsEmitter
    {
        private readonly float _delay;
        private readonly float _startSpeed;

        private readonly List<PhysicsBullet> _bullets;
        private readonly Transform _transform;

        private int _currentIndex;
        private float _timeTillNextBullet;

        public BulletsEmitter(List<PhysicsBullet> bulletViews, Transform transform, float delay, float startSpeed)
        {
            _transform = transform;
            _bullets = bulletViews;
            _delay = delay;
            _startSpeed = startSpeed;
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