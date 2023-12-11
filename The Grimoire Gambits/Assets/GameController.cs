using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [SerializeField] private GameObject player = null;
    [SerializeField] private GameObject enemy = null;
    [SerializeField] private Slider playerHealth = null;
    [SerializeField] private Slider enemyHealth = null;
    [SerializeField] private Button attackBtn = null;
    [SerializeField] private Button healBtn = null;

    private bool isPlayerTurn = true;

    // Method to handle attacks
    private void Attack(GameObject target, float damage)
    {
        if (target == enemy)
        {
            enemyHealth.value -= damage;
        }
        else
        {
            playerHealth.value -= damage;
        }

        ChangeTurn(); // Switch to the other player's turn
    }

    // Method to handle healing
    private void Heal(GameObject target, float amount)
    {
        if (target == enemy)
        {
            enemyHealth.value += amount;
        }
        else
        {
            playerHealth.value += amount;
        }

        ChangeTurn(); // Switch to the other player's turn
    }

    // Method called when the Attack button is clicked
    public void BtnAttack()
    {
        Attack(enemy, 10);
    }

    // Method called when the Heal button is clicked
    public void BtnHeal()
    {
        Heal(player, 5);
    }

    // Method to switch turns between player and enemy
    private void ChangeTurn()
    {
        isPlayerTurn = !isPlayerTurn; // Toggle the turn

        if (!isPlayerTurn)
        {
            // Disable buttons during the enemy's turn
            attackBtn.interactable = false;
            healBtn.interactable = false;

            StartCoroutine(EnemyTurn()); // Start the enemy's turn
        }
        else
        {
            // Enable buttons during the player's turn
            attackBtn.interactable = true;
            healBtn.interactable = true;
        }
    }

    // Coroutine for handling the enemy's turn
    private IEnumerator EnemyTurn()
    {
        yield return new WaitForSeconds(3); // Wait for 3 seconds before the enemy acts

        int random = Random.Range(1, 3);

        if (random == 1)
        {
            Attack(player, 12); // Enemy attacks the player
        }
        else
        {
            Heal(enemy, 3); // Enemy heals itself
        }

        ChangeTurn(); // Switch back to the player's turn
    }
}
