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
        private SpriteAnimator _playerAnimator;
        private SpriteAnimator _bonfireAnimator;

        private void Awake()
        {
            SpriteAnimatorConfig config = 
                Resources.Load<SpriteAnimatorConfig>("PlayerAnimationCfg");
            _playerAnimator = new SpriteAnimator(config);
            _playerAnimator.StartAnimation(_playerView._spriteRenderer,
                AnimState.Idle, true, _animationSpeed);
            config = Resources.Load<SpriteAnimatorConfig>("BonfireAnimationCfg");
            _bonfireAnimator = new SpriteAnimator(config);
            _bonfireAnimator.StartAnimation(_bonfireView._spriteRenderer, 
                AnimState.Idle, true, _animationSpeed);
        }

        private void Update()
        {
            _playerAnimator.Update();
            _bonfireAnimator.Update();
        }
    }
}