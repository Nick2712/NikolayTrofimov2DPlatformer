using UnityEngine;


namespace NikolayT2DGame
{
    public class CannonView : MonoBehaviour
    {
        public Transform CannonBarrel;
        public Transform Emitter;
        public int BulletsCount = 5;
        public float BulletShootDelay = 1.0f;
        public float BulletStartSpeed = -2.0f;
    }
}