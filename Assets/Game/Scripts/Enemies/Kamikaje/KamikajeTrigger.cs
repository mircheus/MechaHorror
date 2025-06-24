using Game.Scripts.Player;
using UnityEngine;
using UnityEngine.Events;

namespace Game.Scripts.Enemies.Kamikaje
{
    [RequireComponent(typeof(Collider))]
    public class KamikajeTrigger : MonoBehaviour
    {
        public event UnityAction PlayerEntered;
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out PlayerHealth player))
            {
                PlayerEntered?.Invoke();
            }
        }
    }
}