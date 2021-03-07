using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NikolayT2DGame
{
    public class Lessons : MonoBehaviour
    {
        [SerializeField] private UIView _uIView;
        [SerializeField] private int _animationSpeed = 10;
        [SerializeField] private int _bulletsCount = 5;
        [SerializeField] private int _playerStartHealth = 3;
        private GameInitializer _gameInitializer;

        private void Awake()
        {
            _gameInitializer = new GameInitializer(_animationSpeed, _playerStartHealth, _uIView);
        }

        private void Update()
        {
            _gameInitializer.PlayerController.Update();
            _gameInitializer.CoinsManager.Update();
            _gameInitializer.BonfireAnimation.Update();
            for(int i = 0; i < _gameInitializer.CannonControllers.Count; i++)
            {
                _gameInitializer.CannonControllers[i].Update();
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