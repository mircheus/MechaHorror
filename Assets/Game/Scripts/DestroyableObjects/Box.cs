using Game.Scripts.Player;
using UnityEngine;

namespace Game.Scripts.DestroyableObjects
{
    public class Box : MonoBehaviour, IDestroyable
    {
        [SerializeField] private ParticleSystem destroyParticle;
        [SerializeField] private float destroyDelay = 0f;
        [SerializeField] private GameObject boxMesh;
        
        public void Destroy()
        {
            if(destroyParticle != null)
            {
                destroyParticle.Play();
            }
            
            Destroy(gameObject, destroyDelay);
        }
    }
}