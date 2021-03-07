using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace NikolayT2DGame
{
    public class PlayerController
    {
        public readonly Transform PlayerTransform;
        public readonly PlayerData PlayerData;
        private readonly MainHeroPhysicsWalker _mainHeroPhysicsWalker;
        
        public Action PlayerHealthChanged;

        public PlayerController(LevelObjectView playerView, int playerStartHealth)
        {
            var animationConfig =
                Resources.Load<SpriteAnimatorConfig>(LoadPathManager.PLAYER_ANIMATION_CFG);
            _mainHeroPhysicsWalker = new MainHeroPhysicsWalker(playerView,
                new SpriteAnimator(animationConfig));
            PlayerData = new PlayerData(playerStartHealth);
            PlayerTransform = _mainHeroPhysicsWalker.PlayerTransform;
        }

        public void GetDamage(int damage)
        {
            PlayerData.Health -= damage;
            Debug.Log($"осталось здоровья {PlayerData.Health}");
            PlayerHealthChanged?.Invoke();
        }

        public void Update()
        {
            _mainHeroPhysicsWalker.Update();
        }

        public void FixedUpdate()
        {
            _mainHeroPhysicsWalker.FixedUpdate();
        }
    }
}