using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace NikolayT2DGame
{
    public class GameInitializer
    {
        public PlayerCamera PlayerCamera { get; private set; }
        public MainHeroPhysicsWalker PlayerController { get; private set; }
        public List<SpriteAnimator> CoinsAnimation { get; private set; } = 
            new List<SpriteAnimator>();
        public List<SpriteAnimator> EnvironmentsAnimation { get; private set; } = 
            new List<SpriteAnimator>();

        private readonly Camera _mainCamera;
        private readonly LevelObjectView _playerView;
        private readonly List<LevelObjectView> _coinsView = new List<LevelObjectView>();
        private readonly List<LevelObjectView> _environmentsView = 
            new List<LevelObjectView>();

        public GameInitializer(float defaultAnimationSpeed)
        {
            _mainCamera = Camera.main;
            
            var objectsView = Object.FindObjectsOfType<LevelObjectView>();
            foreach(LevelObjectView objectView in objectsView)
            {
                if(objectView.CompareTag(TagManager.PLAYER))
                {
                    _playerView = objectView;
                }
                if(objectView.CompareTag(TagManager.COIN))
                {
                    _coinsView.Add(objectView);
                }
                if(objectView.CompareTag(TagManager.ENVIRONMENT))
                {
                    _environmentsView.Add(objectView);
                }
            }

            PlayerController = new MainHeroPhysicsWalker(_playerView, 
                new SpriteAnimator(_playerView.SpriteAnimatorConfig));
            PlayerCamera = new PlayerCamera(_mainCamera.transform, _playerView.transform);

            for(int i = 0; i < _coinsView.Count; i++)
            {
                CoinsAnimation.Add(new SpriteAnimator(_coinsView[i].SpriteAnimatorConfig));
                CoinsAnimation[i].StartAnimation(_coinsView[i].SpriteRenderer,
                    AnimState.Idle, true, defaultAnimationSpeed);
            }

            for(int i = 0; i < _environmentsView.Count; i++)
            {
                EnvironmentsAnimation.Add(
                    new SpriteAnimator(_environmentsView[i].SpriteAnimatorConfig));
                EnvironmentsAnimation[i].StartAnimation(_environmentsView[i].SpriteRenderer,
                    AnimState.Idle, true, defaultAnimationSpeed);
            }
        }
    }
}