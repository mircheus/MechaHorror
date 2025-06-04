using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Scripts.Enemies.WardenCore
{
    public class WardenCorePlayerDetector : MonoBehaviour
    {
        [SerializeField] private PlayerDetected playerDetected;
        [SerializeField] private GameObject player;

        private void OnEnable()
        {
            DetectPlayer();
        }

        public void DetectPlayer()
        {
            playerDetected.SendEventMessage(gameObject, player);
        }
    }
}