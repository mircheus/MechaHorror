using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TargetEffects
{
    public GameObject hitParticle;
    public GameObject respawnParticle;
    public List<GameObject> deathParticles = new List<GameObject>();
    public AudioClip destroySound; // Sound to play on destruction
    public AudioClip respawnSound; // Sound to play on respawn
}