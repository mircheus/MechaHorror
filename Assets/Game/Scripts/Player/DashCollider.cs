using System;
using UnityEngine;
using UnityEngine.Events;

namespace Game.Scripts.Player
{
    [RequireComponent(typeof(BoxCollider))]
    public class DashCollider : MonoBehaviour
    {
        private BoxCollider _collider;
        
        public event UnityAction DashTriggered;

        private void OnEnable()
        {
            _collider = GetComponent<BoxCollider>();
        }
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out IDestroyable destroyable))
            {
                DashTriggered?.Invoke();
                destroyable.Destroy();
            }
        }
    }
}
