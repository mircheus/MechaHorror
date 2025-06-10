using System;
using UnityEngine;

namespace Game.Scripts.MiniMap
{
    public class FollowYRotation : MonoBehaviour
    {
        [Header("Minimap rotations")] public Transform playerReference;
        public float playerOffset = 10f;

        private void Update()
        {
            UpdatePosition();
        }

        private void UpdatePosition()
        {
            if (playerReference != null)
            {
                transform.position = new Vector3(playerReference.position.x, playerReference.position.y + playerOffset,
                    playerReference.position.z);
                transform.rotation = Quaternion.Euler(90f, 0f, 0f);
            }
        }

        private void OnDrawGizmos()
        {
            UpdatePosition();
        }
    }
}