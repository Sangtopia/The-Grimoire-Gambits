using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum BattleState { START, PLAYERTURN, ENEMYTURN, WON, LOST }

public class BattleSystem : MonoBehaviour
{
    public GameObject playerPrefab;
    public GameObject enemyPrefab;

    public Transform playerBattleStation;
    public Transform enemyBattleStation;

    public Text dialogueText;

    Unit playerUnit;
    Unit enemyUnit;

    public BattleState state;

    private bool isPlayerTurn = true;
    private bool isCharging = false;

    // Start is called before the first frame update
    void Start()
    {
        state = BattleState.START;
        SetupBattle();
    }

    void SetupBattle()
    {
        GameObject playerGO = Instantiate(playerPrefab, playerBattleStation);
        playerUnit = playerGO.GetComponent<Unit>();

        GameObject enemyGO = Instantiate(enemyPrefab, enemyBattleStation);
        enemyUnit = enemyGO.GetComponent<Unit>();

        dialogueText.text = "A wild " + enemyUnit.unitName + " approaches..";
    }

    public void PlayerTurn()
    {
        isPlayerTurn = true;

 
        isPlayerTurn = false;

        // Call EnemyTurn to let the enemy take its turn
        state = BattleState.ENEMYTURN;
        StartCoroutine(EnemyTurn());
    }

    IEnumerator EnemyTurn()
    {
        // Wait for a short duration (you can replace this with your own logic)
        yield return new WaitForSeconds(1.5f);

        // Implement AI logic for the enemy's actions
        // Randomly choose actions with similar chances as the player
        float randomAction = Random.Range(0f, 1f);

        if (randomAction < 0.25f)
        {
            // 25% chance to attack
            EnemyPerformAttack();
        }
        else if (randomAction < 0.5f)
        {
            // 25% chance to defend
            EnemyPerformDefend();
        }
        else if (randomAction < 0.75f)
        {
            // 25% chance to charge
            EnemyPerformCharge();
        }
        else
        {
            // 25% chance to heal
            EnemyPerformHeal();
        }

        // After the enemy's turn, call PlayerTurn to let the player take the next turn
        state = BattleState.PLAYERTURN;
    }

    // Placeholder methods for player's actions
    private void PerformAttack()
    {
        // Implement attack logic
        int playerDamage = isCharging ? 40 : 20;  // Double damage if charging, replace with actual calculation
        // Apply damage to the enemy or perform other attack-related actions
    }

    private void PerformDefend()
    {
        // Implement defend logic
        // For simplicity, let's say defending gives a 50% chance to block incoming damage completely
        if (Random.Range(0f, 1f) < 0.5f)
        {
            // Defend successful, no damage taken
            Debug.Log("Defend successful! No damage taken.");
        }
        else
        {
            // Defend failed, apply damage as usual
            int incomingDamage = 10; 
            // Apply damage to the player or perform other attack-related actions
        }
    }

    // Placeholder method for the heal action
    private void PerformHeal()
    {
        // Implement heal logic
        int healAmount = 10; 
        // Heal the player or perform other healing-related actions
    }

    // Placeholder method for the charge action
    private void PerformCharge()
    {
        // Implement charge logic
        isCharging = true;
        Debug.Log("Player is charging for the next attack.");

        // Skip the next player turn
        SkipNextPlayerTurn();
    }

    // Method to skip the next player turn
    private void SkipNextPlayerTurn()
    {
        // You can implement additional logic if needed
        Debug.Log("Player will skip the next turn.");
    }

    // Placeholder methods for enemy's actions
    private void EnemyPerformAttack()
    {
        // Implement enemy's attack logic
        int enemyDamage = isCharging ? 40 : 20;  // Double damage if charging, replace with actual calculation
        // Apply damage to the player or perform other attack-related actions
    }

    private void EnemyPerformDefend()
    {
        // Implement enemy's defend logic
        // For simplicity, let's say defending gives a 50% chance to block incoming damage completely
        if (Random.Range(0f, 1f) < 0.5f)
        {
            // Defend successful, no damage taken
            Debug.Log("Enemy defended successfully! No damage taken.");
        }
        else
        {
            // Defend failed, apply damage as usual
            int incomingDamage = 10;  // Placeholder value, replace with actual calculation
            // Apply damage to the enemy or perform other attack-related actions
        }
    }

    private void EnemyPerformCharge()
    {
        // Implement enemy's charge logic
        Debug.Log("Enemy is charging for the next attack.");
        // Skip the next enemy turn (optional, depends on your game design)
    }

    private void EnemyPerformHeal()
    {
        // Implement enemy's heal logic
        int healAmount = 10;  // Placeholder value, replace with actual calculation
        // Heal the enemy or perform other healing-related actions
    }
}
