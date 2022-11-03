using UnityEngine;

public abstract class Health : MonoBehaviour
{
    public float health = 100f;
    public bool isDead;

    public abstract void TakeDamage(float damage);
    public abstract void TakeHealth(float Health);
    public abstract void Die();
}