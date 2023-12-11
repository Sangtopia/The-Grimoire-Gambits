using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public enum BattleState { START, PLAYERTURN, ENEMYTURN, WON, LOST }

public class CombatSystems : MonoBehaviour
{
    public GameObject playerPrefab;
    public GameObject enemyPrefab;

    public Transform playerBattleStation;
    public Transform enemyBattleStation;

    protected Unit playerUnit;
    protected Unit enemyUnit;

    public Text dialogueText;

    public BattleHUD playerHUD;
    public BattleHUD enemyHUD;

    protected BattleState state;

    [SerializeField] private Slider playerHealth = null;
    [SerializeField] private Slider enemyHealth = null;
    [SerializeField] private Button attackBtn = null;
    [SerializeField] private Button healBtn = null;

    private bool isPlayerTurn = true;

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

        yield return new WaitForSeconds(1f);

        state = BattleState.PLAYERTURN;
        PlayerTurn();
    }

    IEnumerator PlayerAttack()
    {
        int damageDealt = playerUnit.damage;

        bool isDead = enemyUnit.TakeDamage(damageDealt);

        enemyHUD.SetHP(enemyUnit.CurrentHP, enemyUnit.maxHP);

        dialogueText.text = "The attack is successful, dealing " + damageDealt + " damage!";

        yield return new WaitForSeconds(2f);

        if (isDead)
        {
            state = BattleState.WON;
            EndBattle();
        }
        else
        {
            state = BattleState.ENEMYTURN;
            StartCoroutine(PerformEnemyTurn());
        }
    }

    IEnumerator PerformEnemyTurn()
    {
        if (!isPlayerTurn)
        {
            int randomAction = Random.Range(0, 3); // 0 represents attack, 1 represents heal

            if (randomAction == 0)
            {
                dialogueText.text = enemyUnit.unitName + " attacks!";
                yield return new WaitForSeconds(1f);

                bool isDead = playerUnit.TakeDamage(enemyUnit.damage);
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
            else
            {
                int healAmount = 5; // You can adjust the healing amount as needed

                dialogueText.text = enemyUnit.unitName + " heals for " + healAmount + " HP!";
                yield return new WaitForSeconds(1f);

                enemyUnit.Heal(healAmount);
                enemyHUD.SetHP(enemyUnit.CurrentHP, enemyUnit.maxHP);

                yield return new WaitForSeconds(1f);

                state = BattleState.PLAYERTURN;
                PlayerTurn();
            }
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
        int healAmount = 10; // You can adjust the healing amount as needed

        playerUnit.Heal(healAmount);

        playerHUD.SetHP(playerUnit.CurrentHP, playerUnit.maxHP);

        dialogueText.text = "You feel renewed strength! Healed for " + healAmount + " HP.";

        yield return new WaitForSeconds(1f);

        state = BattleState.ENEMYTURN;
        StartCoroutine(PerformEnemyTurn());
    }

    void Attack(GameObject target, float damage)
    {
        if (target == enemyPrefab)
        {
            enemyHealth.value -= damage;
        }
        else
        {
            playerHealth.value -= damage;
        }

        ChangeTurn();
    }

    void Heal(GameObject target, float amount)
    {
        if (target == enemyPrefab)
        {
            enemyHealth.value += amount;
        }
        else
        {
            playerHealth.value += amount;
        }

        ChangeTurn();
    }

    public void BtnAttack()
    {
        Attack(enemyPrefab, 10);
    }

    public void BtnHeal()
    {
        Heal(playerPrefab, 5);
    }

    void ChangeTurn()
    {
        isPlayerTurn = !isPlayerTurn;

        if (!isPlayerTurn)
        {
            attackBtn.interactable = false;
            healBtn.interactable = false;

            StartCoroutine(PerformEnemyTurn());
        }
        else
        {
            attackBtn.interactable = true;
            healBtn.interactable = true;

            if (state != BattleState.WON && state != BattleState.LOST)
            {
                state = BattleState.PLAYERTURN;  // Set the state to PLAYERTURN only if the battle is still ongoing
                PlayerTurn();
            }
        }
    }
}
