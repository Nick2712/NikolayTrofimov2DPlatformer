namespace NikolayT2DGame
{
    public class LiftController
    {
        private readonly LiftView _liftView;
        private readonly float _yTopPosition;
        private readonly float _yLowerPosition;
        private float _liftSpeed;

        public LiftController(LiftView liftView)
        {
            _liftView = liftView;
            _yLowerPosition = _liftView.transform.position.y;
            _yTopPosition = _yLowerPosition + _liftView.LiftMovingDistance;
            _liftSpeed = liftView.SliderJoint.motor.motorSpeed;
        }

        public void Update()
        {
            if(_liftView.transform.position.y > _yTopPosition && 
                _liftView.SliderJoint.motor.motorSpeed < 0 || 
                _liftView.transform.position.y < _yLowerPosition && 
                _liftView.SliderJoint.motor.motorSpeed > 0)
            {
                _liftSpeed *= -1;
                _liftView.JointMotorSpeedChanger(_liftSpeed);
            }
        }
    }
}