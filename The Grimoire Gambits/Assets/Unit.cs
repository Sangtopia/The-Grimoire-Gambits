using System.Collections;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public string unitName;
    public int unitLevel;

    public int damage;

    public int maxHP;
    private int currentHP;

    // Use properties for encapsulation
    public int CurrentHP
    {
        get { return currentHP; }
        private set { currentHP = Mathf.Clamp(value, 0, maxHP); }
    }

    // Use properties for public fields whenever applicable
    public bool IsAlive => CurrentHP > 0;


    public bool TakeDamage(int damageAmount)
{
    CurrentHP -= damageAmount;

    if (CurrentHP <= 0)
    {
        CurrentHP = 0; // Ensure health doesn't go below 0
        return false;  // The unit is not alive
    }

    return true; // The unit is still alive
}

    public void Heal(int amount)
    {
        CurrentHP += amount;
    }
}
