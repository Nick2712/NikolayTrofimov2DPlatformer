using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace NikolayT2DGame
{
    public class CoinsManager : IDisposable
    {
        private const float _animationsSpeed = 10;

        private readonly LevelObjectView _characterView;
        private readonly SpriteAnimator _spriteAnimator;
        private readonly List<LevelObjectView> _coinViews;

        public CoinsManager(LevelObjectView characterView, 
            List<LevelObjectView> coinViews, SpriteAnimator spriteAnimator)
        {
            _characterView = characterView;
            _spriteAnimator = spriteAnimator;
            _coinViews = coinViews;
            _characterView.OnLevelObjectContact += OnLevelObjectContact;

            foreach (var coinView in coinViews)
            {
                _spriteAnimator.StartAnimation(coinView.SpriteRenderer, AnimState.Idle,
                    true, _animationsSpeed);
            }
        }

        private void OnLevelObjectContact(LevelObjectView contactView)
        {
            if (_coinViews.Contains(contactView))
            {
                _spriteAnimator.StopAnimation(contactView.SpriteRenderer);
                _coinViews.Remove(contactView);
                GameObject.Destroy(contactView.gameObject);
                Debug.Log($"осталось монеток {_coinViews.Count}");
            }
        }

        public void Update()
        {
            _spriteAnimator.Update();
        }

        public void Dispose()
        {
            _characterView.OnLevelObjectContact -= OnLevelObjectContact;
        }
    }

}