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

    [SerializeField] private Slider playerHealthSlider = null;
    [SerializeField] private Slider enemyHealthSlider = null;
    [SerializeField] private Button attackBtn = null;
    [SerializeField] private Button healBtn = null;

    private bool isPlayerTurn = true;
    private PlayerController playerController; // Reference to PlayerController script

    // Start is called before the first frame update
    void Start()
    {
        state = BattleState.START;
        StartCoroutine(SetupBattle());

        // Get reference to PlayerController script
        playerController = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
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
        // Set canMove to false in PlayerController to prevent player movement during battle
        playerController.canMove = false;

        int damageDealt = playerUnit.damage;

        bool isDead = enemyUnit.TakeDamage(damageDealt);
        enemyHUD.SetHP(enemyUnit.CurrentHP, enemyUnit.maxHP);

        dialogueText.text = "The attack is successful, dealing " + damageDealt + " damage!";

        yield return new WaitForSeconds(2f);

        if (isDead)
        {
            state = BattleState.WON;
        }
        else
        {
            state = BattleState.ENEMYTURN;
            StartCoroutine(PerformEnemyTurn());
        }

        EndBattle();
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

        EndBattle();
    }

    void EndBattle()
    {
        // Set canMove to true in PlayerController to allow player movement after battle
        playerController.canMove = true;

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
        // Set canMove to false in PlayerController to prevent player movement during battle
        playerController.canMove = false;

        int healAmount = 10; // You can adjust the healing amount as needed

        playerUnit.Heal(healAmount);
        playerHUD.SetHP(playerUnit.CurrentHP, playerUnit.maxHP);

        dialogueText.text = "You feel renewed strength! Healed for " + healAmount + " HP.";

        yield return new WaitForSeconds(1f);

        state = BattleState.ENEMYTURN;
        StartCoroutine(PerformEnemyTurn());

        EndBattle();
    }

    void Attack(GameObject target, float damage)
    {
        if (target == enemyPrefab)
        {
            enemyHealthSlider.value -= damage;
        }
        else
        {
            playerHealthSlider.value -= damage;
        }

        ChangeTurn();
    }

    void Heal(GameObject target, float amount)
    {
        if (target == enemyPrefab)
        {
            enemyHealthSlider.value += amount;
        }
        else
        {
            playerHealthSlider.value += amount;
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

        attackBtn.interactable = !attackBtn.interactable;
        healBtn.interactable = !healBtn.interactable;

        if (!isPlayerTurn)
        {
            StartCoroutine(PerformEnemyTurn());
        }
        else
        {
            if (state != BattleState.WON && state != BattleState.LOST)
            {
                state = BattleState.PLAYERTURN;  // Set the state to PLAYERTURN only if the battle is still ongoing
                PlayerTurn();
            }
        }
    }
}
