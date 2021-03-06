using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace NikolayT2DGame
{
    public class PhysicsBullet
    {
        private readonly LevelObjectView _view;

        public PhysicsBullet(LevelObjectView view)
        {
            _view = view;
            _view.gameObject.SetActive(false);
        }

        public void Throw(Vector3 position, Vector3 velocity)
        {
            _view.gameObject.SetActive(false);
            _view.Transform.position = position;
            _view.Rigidbody2D.velocity = Vector2.zero;
            _view.Rigidbody2D.angularVelocity = 0;
            _view.gameObject.SetActive(true);
            _view.Rigidbody2D.AddForce(velocity, ForceMode2D.Impulse);
        }
    }

}