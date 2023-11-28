using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    // Basic player attributes
    public int health = 100;
    public int defense = 10;
    public int attack = 20;

    // Experience and leveling attributes
    public int experience = 0;
    public int level = 1;
    public float experienceMultiplier = 1.5f;  // Multiplier for the next level's experience requirement


    private void Start()
    {
        // Initial calculation of experience needed for the next level
        experienceToNextLevel = CalculateExperienceForNextLevel();
    }

    // Experience required for the next level
    public int experienceToNextLevel;

    // Function to handle player taking damage
    public void TakeDamage(int damage)
    {
        health -= damage;

        // Check if the player is out of health
        if (health <= 0)
        {
            SceneManager.LoadScene("Game Over Scene"); 
        }
    }

    // Function to handle gaining experience
    public void GainExperience(int xp)
    {
        experience += xp;

        // Check if the player has gained enough experience to level up
        if (experience >= experienceToNextLevel)
        {
            LevelUp();
        }
    }

    // Function to handle leveling up
    private void LevelUp()
    {
        level++;

        // Recalculate experience needed for the next level
        experienceToNextLevel = CalculateExperienceForNextLevel();
    }

    // Function to calculate experience required for the next level
    private int CalculateExperienceForNextLevel()
    {
        // Calculate the next level's experience requirement based on the multiplier
        int nextLevelExperience = (int)(experienceToNextLevel * experienceMultiplier);

        // You can implement additional logic here if needed

        return nextLevelExperience;
    }

}