﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace NikolayT2DGame
{
    public class MainHeroPhysicsWalker
    {
        public Transform PlayerTransform
        {
            get
            {
                return _view.transform;
            }
        }

        private readonly LevelObjectView _view;
        private readonly SpriteAnimator _spriteAnimator;
        private readonly ContactsPoller _contactsPoller;

        private const string _verticalAxisName = "Vertical";
        private const string _horizontalAxisName = "Horizontal";
        private const KeyCode _jumpButton = KeyCode.Space;

        private const float _walkAnimationsSpeed = 40.0f;
        private const float _idleAnimationsSpeed = 10.0f;
        private const float _walkSpeed = 200.0f;
        private const float _jumpForse = 17.0f;
        private const float _flyThresh = 1.0f;
        private const float _jumpThresh = 0.1f;
        private const float _movingThresh = 0.01f;

        private bool _doJump;
        private bool _walks;
        private float _goSideWay = 0;
        private float _newVelocity;

        
        
        public MainHeroPhysicsWalker(LevelObjectView view, SpriteAnimator
            spriteAnimator)
        {
            _view = view;
            _spriteAnimator = spriteAnimator;
            _contactsPoller = new ContactsPoller(_view.Collider2D);
        }

        public void Update()
        {
            if (Input.GetKey(_jumpButton)) _doJump = true;
            else _doJump = false;
            _goSideWay = Input.GetAxis(_horizontalAxisName);
        }

        public void FixedUpdate()
        {
            _contactsPoller.FixedUpdate();

            _walks = Mathf.Abs(_goSideWay) > _movingThresh;

            if (_walks) _view.SpriteRenderer.flipX = _goSideWay < 0;
            _newVelocity = 0f;
            if (_walks &&
                (_goSideWay > 0 || !_contactsPoller.HasLeftContacts) &&
                (_goSideWay < 0 || !_contactsPoller.HasRightContacts))
            {
                _newVelocity = Time.fixedDeltaTime * _walkSpeed * _goSideWay;
            }
            _view.Rigidbody2D.velocity = _view.Rigidbody2D.velocity.Change(x: _newVelocity + 
                (_contactsPoller.IsGrounded ? _contactsPoller.GroundVelocity.x : 0));
            if (_contactsPoller.IsGrounded && _doJump &&
                Mathf.Abs(_view.Rigidbody2D.velocity.y -
                _contactsPoller.GroundVelocity.y) <= _jumpThresh)
            {
                _view.Rigidbody2D.AddForce(Vector3.up * _jumpForse, ForceMode2D.Impulse);
            }

            //animations
            if (_contactsPoller.IsGrounded)
            {
                var track = _walks ? AnimState.Walk : AnimState.Idle;
                _spriteAnimator.StartAnimation(_view.SpriteRenderer, track, true,
                    _walks ? _walkAnimationsSpeed * Mathf.Abs(_goSideWay) : 
                    _idleAnimationsSpeed);
            }
            else if (Mathf.Abs(_view.Rigidbody2D.velocity.y) > _flyThresh)
            {
                if (_view.Rigidbody2D.velocity.y > 0)
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
            _spriteAnimator.Update();
        }
    }
}