using System;
using Game.Scripts.Player;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Scripts.UI
{
    public class AimLockUI : MonoBehaviour
    {
        [SerializeField] private Camera mainCamera;
        [SerializeField] private AimLock aimLock;
        [SerializeField] private Image targetMarker;
        [SerializeField] private Transform currentTarget;

        private void OnEnable()
        {
            aimLock.TargetDetected += LockOnTarget;
            aimLock.TargetLost += ClearTarget;
        }

        private void OnDisable()
        {
            aimLock.TargetDetected -= LockOnTarget;
            aimLock.TargetLost -= ClearTarget;
        }

        private void Start()
        {
            targetMarker.gameObject.SetActive(false);
        }

        private void Update()
        {
            if (currentTarget != null)
            {
                Vector3 screenPos = mainCamera.WorldToScreenPoint(currentTarget.position);

                // Optional: Check if behind the camera
                if (screenPos.z > 0)
                {
                    targetMarker.gameObject.SetActive(true);
                    targetMarker.transform.position = screenPos;
                }
                else
                {
                    targetMarker.gameObject.SetActive(false);
                }
            }
            else
            {
                targetMarker.gameObject.SetActive(false);
            }
        }
        
        private void LockOnTarget(Transform target)
        {
            currentTarget = target;
        }
        
        private void ClearTarget()
        {
            currentTarget = null;
        }
    }
}