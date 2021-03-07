using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace NikolayT2DGame
{
    public class CannonController
    {
        private readonly CannonView _cannonView;
        private readonly AimingMuzzle _aimingMuzzle;
        private readonly BulletsEmitter _bulletsEmitter;
        private readonly GameObject _bullet;

        public CannonController(CannonView cannonView, GameInitializer gameInitializer)
        {
            _cannonView = cannonView;
            _aimingMuzzle = new AimingMuzzle(_cannonView.CannonBarrel, 
                gameInitializer.PlayerView.transform);
            _bullet = Resources.Load(LoadPathManager.BULLET) as GameObject;

            List<PhysicsBullet> bullets = new List<PhysicsBullet>();
            for (int i = 0; i < cannonView.BulletsCount; i++)
            {
                LevelObjectView bulletView = 
                    Object.Instantiate(_bullet).GetComponent<LevelObjectView>();
                bulletView.name = $"bullet{i}";
                PhysicsBullet bullet = new PhysicsBullet(bulletView, 
                    gameInitializer.PlayerView, gameInitializer.PlayerController);

                bullets.Add(bullet);
            }
            _bulletsEmitter = new BulletsEmitter(bullets, _cannonView.Emitter, 
                _cannonView.BulletShootDelay, _cannonView.BulletStartSpeed);
        }

        public void Update()
        {
            _aimingMuzzle.Update();
            _bulletsEmitter.Update();
        }
    }
}