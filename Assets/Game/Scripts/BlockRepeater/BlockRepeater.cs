using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Scripts
{
    public class BlockRepeater : MonoBehaviour
    {
        [SerializeField] private GameObject blockToRepeatPrefab;
        [SerializeField] private TriggerZone[] triggerZones;
        [SerializeField] private float spawnOffset = 273.4f;

        private List<TriggerSide> _triggeredZones;
        
        private void OnEnable()
        {
            // InitTriggerZones();
            _triggeredZones = new List<TriggerSide>();
            SubscribeToTriggerZoneEvents();
        }

        private void OnDisable()
        {
            UnsubscribeFromTriggerZoneEvents();
        }

        // private void InitTriggerZones()
        // {
        //     triggerZones[0].Init(TriggerZoneSide.Right);
        //     triggerZones[1].Init(TriggerZoneSide.Left);
        //     triggerZones[2].Init(TriggerZoneSide.Forward);
        //     triggerZones[3].Init(TriggerZoneSide.Backward);
        // }

        private void OnPlayerEnter(TriggerSide side)
        {
            // if(_triggeredZones.Contains(side))
            // {
            //     return;
            // }
            //
            // SpawnBlockAt(side);
            // _triggeredZones.Add(side);
        }

        private void SpawnBlockAt(TriggerSide triggerSide)
        {
            Vector3 spawnPosition = Vector3.zero;
            Debug.Log("Spawn block at: " + triggerSide);
            switch (triggerSide)
            {
                case TriggerSide.Left:
                    spawnPosition = transform.position + new Vector3(-spawnOffset, 0, 0);
                    break;
                case TriggerSide.Right:
                    spawnPosition = transform.position + new Vector3(spawnOffset, 0, 0);
                    break;
                case TriggerSide.Forward:
                    spawnPosition = transform.position + new Vector3(0, 0, spawnOffset);
                    break;
                case TriggerSide.Backward:
                    spawnPosition = transform.position + new Vector3(0, 0, -spawnOffset);
                    break;
            }

            Instantiate(blockToRepeatPrefab, spawnPosition, Quaternion.identity);
        }

        private void SubscribeToTriggerZoneEvents()
        {
            foreach (var triggerZone in triggerZones)
            {
                triggerZone.PlayerEnter += OnPlayerEnter;
            }
        }
        
        private void UnsubscribeFromTriggerZoneEvents()
        {
            foreach (var triggerZone in triggerZones)
            {
                triggerZone.PlayerEnter -= OnPlayerEnter;
            }
        }
    }
}