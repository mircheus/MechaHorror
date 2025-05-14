using System;
using Hertzole.GoldPlayer;
using Hertzole.GoldPlayer.Example;
using UnityEngine;
using UnityEngine.Events;

namespace Game.Scripts
{
    [RequireComponent(typeof(BoxCollider))]
    public class TriggerZone : MonoBehaviour
    {
        [SerializeField] private TriggerSide triggerSide;
        
        private int _id;
        // private TriggerSide _triggerSide;

        public event UnityAction<TriggerSide> PlayerEnter; 

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                Debug.Log("Triggered by player: " + other.name);
                PlayerEnter?.Invoke(triggerSide);
            }
        }

        // public void Init(TriggerSide triggerSide)
        // {
        //     _triggerSide = triggerSide;
        //     Debug.Log("Initetialized trigger zone: " + _triggerSide);
        //     // _id = id;
        // }
    }
    
    public enum TriggerSide
    {
        Left,
        Right,
        Forward,
        Backward
    }
}