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
        [SerializeField] private bool _marchingSqareIsOn = false;
        [SerializeField] private bool _borderIsOn = true;
        private GameInitializer _gameInitializer;

        [SerializeField] private GenerateLevelView _generateLevelView;
        private GeneratorLevelController _generatorLevelController;

        private void Awake()
        {
            _gameInitializer = new GameInitializer(_animationSpeed, _playerStartHealth, _uIView);
            _generatorLevelController = new GeneratorLevelController(_generateLevelView, 
                _marchingSqareIsOn, _borderIsOn);
            _generatorLevelController.Awake();
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
            for(int i = 0; i < _gameInitializer.LiftControllers.Count; i++)
            {
                _gameInitializer.LiftControllers[i].Update();
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