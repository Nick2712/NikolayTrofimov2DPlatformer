using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NikolayT2DGame
{
    public class Lessons : MonoBehaviour
    {
        [SerializeField] private UIView _uIView;
        [SerializeField] private Transform _cannonBarrel;
        [SerializeField] private GameObject _bullet;
        [SerializeField] private Transform _emitter;
        [SerializeField] private int _animationSpeed = 10;
        [SerializeField] private int _bulletsCount = 5;
        [SerializeField] private int _playerStartHealth = 3;
        
        private AimingMuzzle _aimingMuzzle;
        private BulletsEmitter _bulletsEmitter;
        private GameInitializer _gameInitializer;


        private void Awake()
        {
            _gameInitializer = new GameInitializer(_animationSpeed, _playerStartHealth, _uIView);

            _aimingMuzzle = new AimingMuzzle(_cannonBarrel, 
                _gameInitializer.PlayerController.PlayerTransform);

            List<PhysicsBullet> bullets = new List<PhysicsBullet>();
            for(int i = 0; i < _bulletsCount; i++)
            {
                LevelObjectView bulletView = Instantiate(_bullet).GetComponent<LevelObjectView>();
                bulletView.name = $"bullet{i}";
                PhysicsBullet bullet =
                    new PhysicsBullet(bulletView, _gameInitializer._playerView,
                    _gameInitializer.PlayerController);
                
                bullets.Add(bullet);
            }
            _bulletsEmitter = new BulletsEmitter(bullets, _emitter);
        }

        private void Update()
        {
            _aimingMuzzle.Update();
            _bulletsEmitter.Update();
            _gameInitializer.PlayerController.Update();
            _gameInitializer.CoinsManager.Update();
            _gameInitializer.BonfireAnimation.Update();
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