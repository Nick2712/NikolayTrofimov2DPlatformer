using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace NikolayT2DGame
{
    public class MainHeroWalker
    {
        private const float _walkSpeed = 4.0f;
        private const float _walkAnimationsSpeed = 30.0f;
        private const float _idleAnimationsSpeed = 10.0f;
        private const float _jumpStartSpeed = 10.0f;
        private const float _movingThresh = 0.01f;
        private const float _flyThresh = 1.0f;
        private const float _groundLevel = -3.0f;
        private const float _g = -20.0f;

        private readonly Vector3 _leftScale = new Vector3(-1, 1, 1);
        private readonly Vector3 _rightScale = new Vector3(1, 1, 1);

        private float _yVelocity;
        private bool _doJump;
        private float _xAxisInput;

        private readonly LevelObjectView _view;
        private readonly SpriteAnimator _spriteAnimator;

        public MainHeroWalker(LevelObjectView view, SpriteAnimator spriteAnimator)
        {
            _view = view;
            _spriteAnimator = spriteAnimator;
        }

        public void Update()
        {
            if(Input.GetKey(KeyCode.Space)) _doJump = true;
            else _doJump = false;
            _xAxisInput = Input.GetAxis("Horizontal");
            var goSideWay = Mathf.Abs(_xAxisInput) > _movingThresh;

            if (IsGrounded())
            {
                //walking
                if (goSideWay) GoSideWay();
                _spriteAnimator.StartAnimation(_view.SpriteRenderer, 
                    goSideWay ? AnimState.Walk : AnimState.Idle, true, 
                    goSideWay ? _walkAnimationsSpeed : _idleAnimationsSpeed);

                //start jump
                if (_doJump && _yVelocity == 0)
                {
                    _yVelocity = _jumpStartSpeed;
                }
                //stop jump
                else if (_yVelocity < 0)
                {
                    _yVelocity = 0;
                    _view.Transform.position = 
                        _view.Transform.position.Change(y: _groundLevel);
                }
            }
            else
            {
                //flying
                if (goSideWay) GoSideWay();
                if (Mathf.Abs(_yVelocity) > _flyThresh)
                {
                    if (_yVelocity > 0)
                    {
                        _spriteAnimator.StartAnimation(_view.SpriteRenderer,
                            AnimState.Jump, false, _walkAnimationsSpeed);
                    }
                    else
                    {
                        _spriteAnimator.StartAnimation(_view.SpriteRenderer,
                            AnimState.Falling, false, _walkAnimationsSpeed);
                    }
                }
                _yVelocity += _g * Time.deltaTime;
                _view.Transform.position += Vector3.up * (Time.deltaTime * _yVelocity);
            }

            _spriteAnimator.Update(goSideWay ? Mathf.Abs(_xAxisInput) : 1.0f);
        }

        private void GoSideWay()
        {
            _view.Transform.position += 
                Vector3.right * (Time.deltaTime * _walkSpeed * _xAxisInput);
            _view.Transform.localScale = (_xAxisInput < 0 ? _leftScale : _rightScale);
        }

        public bool IsGrounded()
        {
            return _view.Transform.position.y <= 
                _groundLevel + float.Epsilon && _yVelocity <= 0;
        }
    }
}