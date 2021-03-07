using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace NikolayT2DGame
{
    public class UIController
    {
        private readonly UIView _uIView;
        private readonly PlayerData _playerData;
        private readonly PlayerController _playerController;
        
        public UIController(UIView uIView, PlayerController playerController)
        {
            _uIView = uIView;
            _playerData = playerController.PlayerData;
            _playerController = playerController;
            _playerController.PlayerHealthChanged += OnPlayerHealthChanged;
        }

        public void OnPlayerHealthChanged()
        {
            foreach(Image image in _uIView.HealthIcons)
            {
                image.gameObject.SetActive(false);
            }
            for(int i = 0; i < _playerData.Health; i++)
            {
                _uIView.HealthIcons[i].gameObject.SetActive(true);
            }
        }
    }
}