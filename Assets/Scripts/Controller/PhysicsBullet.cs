using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace NikolayT2DGame
{
    public class PhysicsBullet : IDisposable
    {
        private readonly LevelObjectView _view;
        private readonly LevelObjectView _player;
        private readonly PlayerController _playerController;
        private const int _power = 1;
        
        public PhysicsBullet(LevelObjectView view, LevelObjectView player,
            PlayerController playerController)
        {
            _view = view;
            _view.gameObject.SetActive(false);
            _player = player;
            _playerController = playerController;
            _player.OnLevelObjectContact += OnLevelObjectContact;
        }

        public void Throw(Vector3 position, Vector3 velocity)
        {
            _view.gameObject.SetActive(false);
            _view.Transform.position = position;
            _view.Rigidbody2D.velocity = Vector2.zero;
            _view.Rigidbody2D.angularVelocity = 0;
            _view.gameObject.SetActive(true);
            _view.Rigidbody2D.AddForce(velocity, ForceMode2D.Impulse);
        }

        private void OnLevelObjectContact(LevelObjectView contactView)
        {
            if (contactView == _view)
            {
                _playerController.GetDamage(_power);
                _view.gameObject.SetActive(false);
                
            }
        }

        public void Dispose()
        {
            _player.OnLevelObjectContact -= OnLevelObjectContact;
        }
    }

}