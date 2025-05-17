using Hertzole.GoldPlayer;
using UnityEngine;

namespace Game.Scripts.Player
{
    public class EnergyMediator : MonoBehaviour
    {
        [SerializeField] private PlayerInputHandler playerInputHandler;
        [SerializeField] private GoldPlayerController playerController;
        [SerializeField] private MovementSpeeds movementSpeeds;
        [SerializeField] private MovementSpeeds nonMovementSpeeds;

        private void OnEnable()
        {
            playerInputHandler.OnRadarModeEvent += OnRadarMode;
            playerInputHandler.OnMovementModeEvent += OnMovementMode;
            playerInputHandler.OnShooterModeEvent += OnShooterMode;
        }

        private void OnRadarMode()
        {
            SwitchModes();
        }

        private void OnMovementMode()
        {
            SwitchModes();
            playerController.Movement.WalkingSpeeds = movementSpeeds;
        }

        private void OnShooterMode()
        {
            SwitchModes();
        }

        private void SwitchModes()
        {
            playerController.Movement.WalkingSpeeds = nonMovementSpeeds;
        }
    }
}