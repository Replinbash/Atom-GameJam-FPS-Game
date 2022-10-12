using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    protected internal int health;
    protected internal int maxHealth;
    protected internal bool isDead;

    private void Start()
    {
        InitVariables();
    }

    public virtual void CheckHealth()
    {
        if (health <= 0)
        {
            health = 0;
            Die();
        }

        if (health >= maxHealth)
        {
            health = maxHealth;
        }
    }

    protected virtual void Die()
    {
        isDead = true;      
    }

    public bool IsDead()
    {
        return isDead;
    }

    public void SetHealthTo(int healthToSetTo)
    {
        health = healthToSetTo;
        CheckHealth();
    }

    public void TakeDamage(int damage)
    {
        int healthAfterDamage = health - damage;
        SetHealthTo(healthAfterDamage);
    }

    public void Heal(int heal)
    {
        int healthAfterHeal = health + heal;
        SetHealthTo(healthAfterHeal);
    }

    public virtual void InitVariables()
    {
        maxHealth = 100; // default value
        SetHealthTo(maxHealth);
        isDead = false;
    }
}
        