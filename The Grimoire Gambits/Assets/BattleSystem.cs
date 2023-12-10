using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public enum BattleState { START, PLAYERTURN, ENEMYTURN, WON, LOST }

public class BattleSystem : MonoBehaviour
{
    public GameObject playerPrefab;
    public GameObject enemyPrefab;

    public Transform playerBattleStation;
    public Transform enemyBattleStation;

    Unit playerUnit;
    Unit enemyUnit;

    public Text dialogueText;

    public BattleHUD playerHUD;
    public BattleHUD enemyHUD;

    public BattleState state;

    // Start is called before the first frame update
    void Start()
    {
        state = BattleState.START;
        StartCoroutine(SetupBattle());
    }

    IEnumerator SetupBattle()
    {
        GameObject playerGO = Instantiate(playerPrefab, playerBattleStation);
        playerUnit = playerGO.GetComponent<Unit>();

        GameObject enemyGO = Instantiate(enemyPrefab, enemyBattleStation);
        enemyUnit = enemyGO.GetComponent<Unit>();

        dialogueText.text = "A wild " + enemyUnit.unitName + " approaches...";

        playerHUD.SetHUD(playerUnit);
        enemyHUD.SetHUD(enemyUnit);

        yield return new WaitForSeconds(2f);

        state = BattleState.PLAYERTURN;
        PlayerTurn();
    }


    IEnumerator PlayerAttack()
{
    int damageDealt = playerUnit.damage; // Use the attack damage

    bool isDead = enemyUnit.TakeDamage(damageDealt);

    // Update the HP display using the modified SetHP method
    enemyHUD.SetHP(enemyUnit.CurrentHP, enemyUnit.maxHP);

    dialogueText.text = "The attack is successful, dealing " + damageDealt + " damage!"; // Provide feedback on the damage dealt

    yield return new WaitForSeconds(2f);

    if (isDead)
    {
        state = BattleState.WON;
        EndBattle();
    }
    else
    {
        state = BattleState.ENEMYTURN;
        StartCoroutine(EnemyTurn());
    }
}

// Repeat the same modification for the enemy's turn
IEnumerator EnemyTurn()
{
    dialogueText.text = enemyUnit.unitName + " attacks!";

    yield return new WaitForSeconds(1f);

    bool isDead = playerUnit.TakeDamage(enemyUnit.damage);

    // Update the HP display using the modified SetHP method
    playerHUD.SetHP(playerUnit.CurrentHP, playerUnit.maxHP);

    yield return new WaitForSeconds(1f);

    if (isDead)
    {
        state = BattleState.LOST;
        EndBattle();
    }
    else
    {
        state = BattleState.PLAYERTURN;
        PlayerTurn();
    }
}

    void EndBattle()
    {
        if (state == BattleState.WON)
        {
            dialogueText.text = "You won the battle!";
        }
        else if (state == BattleState.LOST)
        {
            dialogueText.text = "You were defeated.";
        }
    }

    void PlayerTurn()
    {
        dialogueText.text = "Choose an action:";
    }

    IEnumerator PlayerHeal()
{
    playerUnit.Heal(5);

    // Update the HP display using the modified SetHP method
    playerHUD.SetHP(playerUnit.CurrentHP, playerUnit.maxHP);

    dialogueText.text = "You feel renewed strength!";

    yield return new WaitForSeconds(2f);

    state = BattleState.ENEMYTURN;
    StartCoroutine(EnemyTurn());
}


    public void OnAttackButton()
    {
        if (state != BattleState.PLAYERTURN)
            return;

        StartCoroutine(PlayerAttack());
    }

    public void OnHealButton()
    {
        if (state != BattleState.PLAYERTURN)
            return;

        StartCoroutine(PlayerHeal());
    }
}