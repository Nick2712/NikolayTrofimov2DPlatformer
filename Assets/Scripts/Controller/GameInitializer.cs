using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace NikolayT2DGame
{
    public class GameInitializer
    {
        public PlayerCamera PlayerCamera { get; private set; }
        public PlayerController PlayerController { get; private set; }
        public SpriteAnimator BonfireAnimation { get; private set; }
        public CoinsManager CoinsManager { get; private set; }
        public LevelObjectView _playerView { get; private set; }

        private readonly Camera _mainCamera;
        private readonly UIController _uIController;
        
        public GameInitializer(float defaultAnimationSpeed, int playerStartHealth, UIView uIView)
        {
            _mainCamera = Camera.main;

            List<LevelObjectView> coinsView = new List<LevelObjectView>();
            List<LevelObjectView> bonfiresView = new List<LevelObjectView>();
            var objectsView = Object.FindObjectsOfType<LevelObjectView>();
            foreach(LevelObjectView objectView in objectsView)
            {
                if(objectView.CompareTag(TagManager.PLAYER))
                {
                    _playerView = objectView;
                }
                if(objectView.CompareTag(TagManager.COIN))
                {
                    coinsView.Add(objectView);
                }
                if(objectView.CompareTag(TagManager.ENVIRONMENT))
                {
                    bonfiresView.Add(objectView);
                }
            }

            PlayerController = new PlayerController(_playerView, playerStartHealth);

            PlayerCamera = new PlayerCamera(_mainCamera.transform, _playerView.transform);

            var animationConfig = 
                Resources.Load<SpriteAnimatorConfig>(LoadPathManager.COIN_ANIMATION_CFG);
            CoinsManager = new CoinsManager(_playerView, coinsView, 
                new SpriteAnimator(animationConfig));
            
            animationConfig = 
                Resources.Load<SpriteAnimatorConfig>(LoadPathManager.BONFIRE_ANIMATION_CFG);
            BonfireAnimation = new SpriteAnimator(animationConfig);

            for(int i = 0; i < bonfiresView.Count; i++)
            {
                BonfireAnimation.StartAnimation(bonfiresView[i].SpriteRenderer,
                    AnimState.Idle, true, defaultAnimationSpeed);
            }

            _uIController = new UIController(uIView, PlayerController);
        }
    }
}