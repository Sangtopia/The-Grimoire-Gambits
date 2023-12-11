using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterStats : MonoBehaviour
{
    // Common attributes for both Player and Unit
    public string characterName;
    public int level;

    // Player-specific attributes
    public int health = 100;
    public int defense = 10;
    public int attack = 20;
    public int experience = 0;
    public float experienceMultiplier = 1.5f;

    // Unit-specific attributes
    public int damage;

    public int maxHP;
    private int currentHP;

    // Use properties to provide controlled access to fields
    public int CurrentHP
    {
        get { return currentHP; }
        private set { currentHP = Mathf.Clamp(value, 0, maxHP); }
    }

    // Use properties for public fields whenever applicable
    public bool IsAlive => CurrentHP > 0;

    private int experienceToNextLevel;

    private void Start()
    {
        CalculateExperienceForNextLevel();
    }

    public void TakeDamage(int damage)
    {
        CurrentHP -= damage;

        if (!IsAlive)
        {
            SceneManager.LoadScene("Game Over Scene");
        }
    }

    public void GainExperience(int xp)
    {
        experience += xp;

        if (experience >= experienceToNextLevel)
        {
            LevelUp();
        }
    }

    private void LevelUp()
    {
        level++;
        CalculateExperienceForNextLevel();
    }

    private void CalculateExperienceForNextLevel()
    {
        experienceToNextLevel = (int)(experienceToNextLevel * experienceMultiplier);
    }
}
