using UnityEngine;

namespace Game.Scripts.Enemies.FourLegEnemy.States
{
    public class DeadState : IState
    {
        private readonly ParticleSystem _deathParticles;
        private readonly MonoBehaviour _monoBehaviour;

        public DeadState(MonoBehaviour monoBehaviour, ParticleSystem deathParticles)
        {
            _deathParticles = deathParticles;
            _monoBehaviour = monoBehaviour;
        }

        public void Enter()
        {
            Debug.Log("Entering Dead State");
            _deathParticles.Play();
            // _monoBehaviour.gameObject.SetActive(false);
            // Optionally, you can add logic to remove the enemy from the game or trigger any other effects.
        }

        public void Execute()
        {

        }

        public void Exit()
        {
           
        }
    }
}