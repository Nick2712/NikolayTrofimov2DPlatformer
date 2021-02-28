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
        private SpriteAnimator _bonfireAnimator;
        private MainHeroWalker _mainHeroWalker;
        private PlayerCamera _playerCamera;

        private void Awake()
        {
            SpriteAnimatorConfig config = 
                Resources.Load<SpriteAnimatorConfig>("PlayerAnimationCfg");
            _mainHeroWalker = new MainHeroWalker(_playerView, new SpriteAnimator(
                config));
            config = Resources.Load<SpriteAnimatorConfig>("BonfireAnimationCfg");
            _bonfireAnimator = new SpriteAnimator(config);
            _bonfireAnimator.StartAnimation(_bonfireView.SpriteRenderer, 
                AnimState.Idle, true, _animationSpeed);
            _playerCamera = new PlayerCamera(_camera.transform, _playerView.transform);
        }

        private void Update()
        {
            _mainHeroWalker.Update();
            _bonfireAnimator.Update();
        }

        private void LateUpdate()
        {
            _playerCamera.LateUpdate();
        }
    }
}