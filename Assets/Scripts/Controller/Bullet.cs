using UnityEngine;


namespace NikolayT2DGame
{
    public class Bullet
    {
        private readonly float _radius = 0.3f;
        private Vector3 _velocity;

        private readonly float _groundLevel;
        private readonly float _g = -10;

        private readonly GameObject _view;

        public Bullet(GameObject view, float grounLevel)
        {
            _view = view;
            _view.SetActive(false);
            _groundLevel = grounLevel;
        }

        public void Update()
        {
            if (IsGrounded())
            {
                SetVelocity(_velocity.Change(y: -_velocity.y));
                _view.transform.position = 
                    _view.transform.position.Change(y: _groundLevel + _radius);
            }
            else
            {
                SetVelocity(_velocity + Vector3.up * _g * Time.deltaTime);
                _view.transform.position += _velocity * Time.deltaTime;
            }
        }

        public void Throw(Vector3 position, Vector3 velocity)
        {
            _view.transform.position = position;
            SetVelocity(velocity);
            _view.SetActive(true);
        }

        private void SetVelocity(Vector3 velocity)
        {
            _velocity = velocity;
            var angle = Vector3.Angle(Vector3.left, _velocity);
            var axis = Vector3.Cross(Vector3.left, _velocity);
            _view.transform.rotation = Quaternion.AngleAxis(angle, axis);

        }

        private bool IsGrounded()
        {
            return _view.transform.position.y <= 
                _groundLevel + _radius + float.Epsilon && _velocity.y <= 0;
        }
    }
}