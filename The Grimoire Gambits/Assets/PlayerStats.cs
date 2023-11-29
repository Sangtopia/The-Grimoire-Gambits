using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerStats : MonoBehaviour
{
    public int health = 100;
    public int defense = 10;
    public int attack = 20;
    public int experience = 0;
    public int level = 1;
    public float experienceMultiplier = 1.5f;

    private int experienceToNextLevel;

    private void Start()
    {
        CalculateExperienceForNextLevel();
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
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
