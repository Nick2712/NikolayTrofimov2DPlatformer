using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NikolayT2DGame
{
    public class Lessons : MonoBehaviour
    {
        [SerializeField] private Camera _camera;
        [SerializeField] private LevelObjectView _playerView;
        [SerializeField] private LevelObjectView _bonfireView;
        [SerializeField] private int _animationSpeed = 10;
        [SerializeField] private Transform _cannonBarrel;
        [SerializeField] private GameObject _bullet;
        [SerializeField] private Transform _emitter;
        [SerializeField] private int _bulletsCount = 5;
        private SpriteAnimator _bonfireAnimator;
        private MainHeroWalker _mainHeroWalker;
        private PlayerCamera _playerCamera;
        private AimingMuzzle _aimingMuzzle;
        private BulletsEmitter _bulletsEmitter;

        private float _groundLevel = -3.0f;

        private void Awake()
        {
            SpriteAnimatorConfig config = 
                Resources.Load<SpriteAnimatorConfig>("PlayerAnimationCfg");
            _mainHeroWalker = new MainHeroWalker(_playerView, new SpriteAnimator(
                config), _groundLevel);

            config = Resources.Load<SpriteAnimatorConfig>("BonfireAnimationCfg");
            _bonfireAnimator = new SpriteAnimator(config);
            _bonfireAnimator.StartAnimation(_bonfireView.SpriteRenderer, 
                AnimState.Idle, true, _animationSpeed);

            _playerCamera = new PlayerCamera(_camera.transform, _playerView.transform);

            _aimingMuzzle = new AimingMuzzle(_cannonBarrel, _playerView.transform);

            List<Bullet> bullets = new List<Bullet>();
            for(int i = 0; i < _bulletsCount; i++)
            {
                Bullet bullet = new Bullet(Instantiate(_bullet), 
                    _groundLevel);
                bullets.Add(bullet);
            }
            _bulletsEmitter = new BulletsEmitter(bullets, _emitter);
        }

        private void Update()
        {
            _mainHeroWalker.Update();
            _bonfireAnimator.Update();
            _aimingMuzzle.Update();
            _bulletsEmitter.Update();
        }

        private void LateUpdate()
        {
            _playerCamera.LateUpdate();
        }
    }
}