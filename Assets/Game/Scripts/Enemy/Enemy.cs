using Game.Scripts;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public void TakeDamage(int amount)
    {
        DamagePopUpGenerator.current.CreatePopUp(transform.position, amount.ToString(), Color.red);
    }
}
