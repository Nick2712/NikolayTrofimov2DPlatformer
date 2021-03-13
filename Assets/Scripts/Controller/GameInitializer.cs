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
        public LevelObjectView PlayerView { get; private set; }
        public List<CannonController> CannonControllers { get; private set; }
        public List<LiftController> LiftControllers { get; private set; }

        private readonly Camera _mainCamera;
        private readonly UIController _uIController;
        private readonly LazerController _lazerController;
        
        public GameInitializer(float defaultAnimationSpeed, int playerStartHealth, 
            UIView uIView)
        {
            _mainCamera = Camera.main;
            

            List<LevelObjectView> coinsView = new List<LevelObjectView>();
            List<LevelObjectView> bonfiresView = new List<LevelObjectView>();
            List<LevelObjectView> lazersView = new List<LevelObjectView>();
            var objectsView = Object.FindObjectsOfType<LevelObjectView>();
            foreach(LevelObjectView objectView in objectsView)
            {
                if(objectView.CompareTag(TagManager.PLAYER))
                {
                    PlayerView = objectView;
                }
                if(objectView.CompareTag(TagManager.COIN))
                {
                    coinsView.Add(objectView);
                }
                if(objectView.CompareTag(TagManager.ENVIRONMENT))
                {
                    bonfiresView.Add(objectView);
                }
                if (objectView.CompareTag(TagManager.LAZER))
                {
                    lazersView.Add(objectView);
                }
            }

            PlayerController = new PlayerController(PlayerView, playerStartHealth);

            _lazerController = new LazerController(lazersView, PlayerView, PlayerController);

            CannonControllers = new List<CannonController>();
            var cannons = Object.FindObjectsOfType<CannonView>();
            if(cannons.Length > 0)
            {
                foreach(CannonView cannon in cannons)
                {
                    var cannonController = new CannonController(cannon, this);
                    CannonControllers.Add(cannonController);
                }
            }

            LiftControllers = new List<LiftController>();
            var lifts = Object.FindObjectsOfType<LiftView>();
            if (lifts.Length > 0)
            {
                foreach (LiftView lift in lifts)
                {
                    var liftController = new LiftController(lift);
                    LiftControllers.Add(liftController);
                }
            }

            PlayerCamera = new PlayerCamera(_mainCamera.transform, PlayerView.transform);

            var animationConfig = 
                Resources.Load<SpriteAnimatorConfig>(LoadPathManager.COIN_ANIMATION_CFG);
            CoinsManager = new CoinsManager(PlayerView, coinsView, 
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