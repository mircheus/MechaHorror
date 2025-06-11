using System;
using Game.Scripts.Player;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Scripts.MiniMap
{
    public class Minimap : MonoBehaviour
    {
        [Header("References: ")]
        [SerializeField] private EnergyMediator energyMediator;
        [SerializeField] private RawImage minimapImage;
        [SerializeField] private Image minimapBackground;

        private void OnEnable()
        {
            energyMediator.OnRadarModeEvent += OnRadarMode;
            energyMediator.OnMovementModeEvent += OnRadarModeDisabled;
            energyMediator.OnShooterModeEvent += OnRadarModeDisabled;
        }
        
        private void OnDisable()
        {
            energyMediator.OnRadarModeEvent -= OnRadarMode;
            energyMediator.OnMovementModeEvent -= OnRadarModeDisabled;
            energyMediator.OnShooterModeEvent -= OnRadarModeDisabled;
        }

        private void OnRadarMode()
        {
            minimapImage.gameObject.SetActive(true);
            minimapBackground.gameObject.SetActive(false);
        }

        private void OnRadarModeDisabled()
        {
            minimapImage.gameObject.SetActive(false);
            minimapBackground.gameObject.SetActive(true);
        }
    }
}