using UnityEngine;

namespace Game.Scripts
{
    public class ToggleVisibility : MonoBehaviour
    {
        [SerializeField] private GameObject targetObject; // Object to toggle
        [SerializeField] private float toggleInterval = 1f; // Time in seconds between toggles

        private float _timer = 0f;

        private void Start()
        {
            if (targetObject == null)
                targetObject = gameObject; // Default to self if not assigned
        }

        private void Update()
        {
            _timer += Time.deltaTime;

            if (_timer >= toggleInterval)
            {
                _timer = 0f;
                targetObject.SetActive(!targetObject.activeSelf);
            }
        }
    }
}