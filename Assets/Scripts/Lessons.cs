using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NikolayT2DGame
{
    public class Lessons : MonoBehaviour
    {
        [SerializeField] private int _animationSpeed = 10;
        [SerializeField] private Transform _cannonBarrel;
        [SerializeField] private GameObject _bullet;
        [SerializeField] private Transform _emitter;
        [SerializeField] private int _bulletsCount = 5;
        
        private AimingMuzzle _aimingMuzzle;
        private BulletsEmitter _bulletsEmitter;
        private GameInitializer _gameInitializer;


        private void Awake()
        {
            _gameInitializer = new GameInitializer(_animationSpeed);

            _aimingMuzzle = new AimingMuzzle(_cannonBarrel, 
                _gameInitializer.PlayerController.PlayerTransform);

            List<PhysicsBullet> bullets = new List<PhysicsBullet>();
            for(int i = 0; i < _bulletsCount; i++)
            {
                PhysicsBullet bullet =
                    new PhysicsBullet(Instantiate(_bullet).GetComponent<LevelObjectView>());
                bullets.Add(bullet);
            }
            _bulletsEmitter = new BulletsEmitter(bullets, _emitter);
        }

        private void Update()
        {
            _aimingMuzzle.Update();
            _bulletsEmitter.Update();
            _gameInitializer.PlayerController.Update();
            if(_gameInitializer.CoinsAnimation.Count > 0)
            {
                foreach(var coin in _gameInitializer.CoinsAnimation)
                {
                    coin.Update();
                }
            }
            if(_gameInitializer.EnvironmentsAnimation.Count > 0)
            {
                foreach(var environment in _gameInitializer.EnvironmentsAnimation)
                {
                    environment.Update();
                }
            }
        }

        private void FixedUpdate()
        {
            _gameInitializer.PlayerController.FixedUpdate();
        }

        private void LateUpdate()
        {
            _gameInitializer.PlayerCamera.LateUpdate();
        }
    }
}