using System;
using Game.Scripts.Utilities;
using UnityEngine;
using UnityEngine.Events;

namespace Game.Scripts.Player
{
    public class BallCollider : MonoBehaviour
    {
        public event UnityAction BallCollision;
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Ball ball))
            {
                BallCollision?.Invoke();
            }
        }
    }
}
