using UnityEngine;

namespace Game.Scripts
{
    public class TransformFollower : MonoBehaviour
    {
        [SerializeField] private Transform target;
        [SerializeField] private float timeInterval = 1f;
        [SerializeField] private PulsingLight pulsingLight;

        private float _timer = 0f;
        
        private void Update()
        {
            _timer += Time.deltaTime;

            if (_timer >= timeInterval)
            {
                _timer = 0f;
                UpdatePosition();
            }
        }
        
        private void UpdatePosition()
        {
            if (target != null)
            {
                transform.position = target.position;
                pulsingLight.Pulse();
            }
        }
    }
}