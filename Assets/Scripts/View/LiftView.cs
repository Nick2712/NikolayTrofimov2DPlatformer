using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace NikolayT2DGame
{
    public class LiftView : MonoBehaviour
    {
        public SliderJoint2D SliderJoint;
        public float LiftMovingDistance;
        private JointMotor2D _jointMotor2D;

        private void Awake()
        {
            _jointMotor2D = SliderJoint.motor;
        }

        public void JointMotorSpeedChanger(float speed)
        {
            _jointMotor2D.motorSpeed = speed;
            SliderJoint.motor = _jointMotor2D;
        }
    }
}