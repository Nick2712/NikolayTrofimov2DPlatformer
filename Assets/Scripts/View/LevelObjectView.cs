using System;
using UnityEngine;


namespace NikolayT2DGame
{
    public class LevelObjectView : MonoBehaviour
    {
        public Transform Transform;
        public SpriteRenderer SpriteRenderer;
        public Rigidbody2D Rigidbody2D;
        public Collider2D Collider2D;
        
        public Action<LevelObjectView> OnLevelObjectContact { get; set; }

        void OnTriggerEnter2D(Collider2D collider)
        {
            var levelObject = collider.gameObject.GetComponent<LevelObjectView>();
            OnLevelObjectContact?.Invoke(levelObject);
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            var levelObject = collision.gameObject.GetComponent<LevelObjectView>();
            OnLevelObjectContact?.Invoke(levelObject);
        }
    }
}