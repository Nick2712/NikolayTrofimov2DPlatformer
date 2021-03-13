using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace NikolayT2DGame
{
    public class LazerController : IDisposable
    {
        private readonly List<LevelObjectView> _lazersView;
        private readonly LevelObjectView _playerView;
        private readonly PlayerController _player;
        const int _power = 1;

        public LazerController(List<LevelObjectView> lazersView, LevelObjectView playerView,
            PlayerController player)
        {
            _lazersView = lazersView;
            _playerView = playerView;
            _player = player;
            _playerView.OnLevelObjectContact += OnLevelObjectContact;
        }

        private void OnLevelObjectContact(LevelObjectView contactView)
        {
            if (_lazersView.Contains(contactView))
            {
                _player.GetDamage(_power);
            }
        }

        public void Dispose()
        {
            _playerView.OnLevelObjectContact -= OnLevelObjectContact;
        }
    }
}