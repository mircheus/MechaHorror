using UnityEngine;
using UnityEngine.UI;

namespace Game.Scripts.Player
{
    public class EnergyPanelUI : MonoBehaviour
    {
        [Header("References:")] 
        [SerializeField] private EnergyMediator energyMediator;
        [SerializeField] private Image radarModeImage;
        [SerializeField] private Image movementModeImage;
        [SerializeField] private Image shooterModeImage;

        private void OnEnable()
        {
            energyMediator.OnRadarModeEvent += OnRadarMode;
            energyMediator.OnMovementModeEvent += OnMovementMode;
            energyMediator.OnShooterModeEvent += OnShooterMode;
        }

        private void OnDisable()
        {
            energyMediator.OnRadarModeEvent -= OnRadarMode;
            energyMediator.OnMovementModeEvent -= OnMovementMode;
            energyMediator.OnShooterModeEvent -= OnShooterMode;
        }

        private void OnRadarMode()
        {
            radarModeImage.gameObject.SetActive(true);
            movementModeImage.gameObject.SetActive(false);
            shooterModeImage.gameObject.SetActive(false);
        }

        private void OnMovementMode()
        {
            radarModeImage.gameObject.SetActive(false);
            movementModeImage.gameObject.SetActive(true);
            shooterModeImage.gameObject.SetActive(false);
        }

        private void OnShooterMode()
        {
            radarModeImage.gameObject.SetActive(false);
            movementModeImage.gameObject.SetActive(false);
            shooterModeImage.gameObject.SetActive(true);
        }
    }
}