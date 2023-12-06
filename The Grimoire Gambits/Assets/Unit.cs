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
        // Subtract damage directly from current HP
        CurrentHP -= damageAmount;

        // Use the property to check if the unit is still alive
        return IsAlive;
    }

    public void Heal(int amount)
    {
        CurrentHP += amount;
    }
}
