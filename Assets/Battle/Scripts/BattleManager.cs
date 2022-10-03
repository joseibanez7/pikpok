using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BattleManager : MonoBehaviour
{
    [SerializeField] private Button attackButton, healButton;
    [SerializeField] private PlayerHudController myHudController, enemyHudController;
    [SerializeField] private UnitsData unitsData;
    [SerializeField] private GameObject losePanel, winPanel;
    [SerializeField] private TextMeshProUGUI battleLogs;

    private BattleState battleState;
    private Unit myUnit, enemyUnit;

    private void Awake()
    {
        attackButton.onClick.AddListener(OnAttackButtonClicked);
        healButton.onClick.AddListener(OnHealButtonClicked);

        battleState = BattleState.Start;
        Setup();
    }

    private void OnDestroy()
    {
        attackButton.onClick.RemoveListener(OnAttackButtonClicked);
        healButton.onClick.RemoveListener(OnHealButtonClicked);
    }

    private void OnAttackButtonClicked()
    {
        if (battleState != BattleState.MyTurn)
            return;
        myUnit.Attack(enemyUnit, OnAttackPerformed);
        UpdateBattleState(BattleState.EnemyTurn);
    }

    private void OnHealButtonClicked()
    {
        if (battleState != BattleState.MyTurn)
            return;
        myUnit.Heal(OnHealingPerformed);
        UpdateBattleState(BattleState.EnemyTurn);
    }

    private void Setup()
    {
        myUnit = unitsData.GetRandomAllyUnit();
        myUnit.Setup(false);
        myUnit.OnUnitActionPerformed += OnUnitPerformedAction;
        myHudController.Setup(myUnit);

        enemyUnit = unitsData.GetRandomEnemyUnit();
        enemyUnit.Setup(true);
        enemyUnit.OnUnitActionPerformed += OnUnitPerformedAction;
        enemyHudController.Setup(enemyUnit);

        battleLogs.text = "";
        UpdateBattleState(BattleState.MyTurn);
    }

    private void OnAttackPerformed(float damageAmount, Unit target, Unit source)
    {
        GetHudControllerByUnit(target).UpdateHealth();
    }

    private void OnHealingPerformed(float healingAmount, Unit target)
    {
        GetHudControllerByUnit(target).UpdateHealth();
    }

    private PlayerHudController GetHudControllerByUnit(Unit unit)
    {
        return unit.IsEnemy ? enemyHudController : myHudController;
    }

    private void UpdateBattleState(BattleState newBattleState)
    {
        if (myUnit.CurrentHealthPercentage <= 0)
        {
            battleState = BattleState.Lose;
            StartCoroutine(DisplayEndOfGame());
            return;
        }

        if (enemyUnit.CurrentHealthPercentage <= 0)
        {
            battleState = BattleState.Win;
            StartCoroutine(DisplayEndOfGame());
            return;
        }

        battleState = newBattleState;

        if (newBattleState == BattleState.EnemyTurn)
        {
            StartCoroutine(PerformEnemyAction());
        }
    }

    private IEnumerator PerformEnemyAction()
    {
        yield return new WaitForSeconds(1f);

        //Only try to heal if health is below half percentage
        if (enemyUnit.CurrentHealthPercentage <= 0.5f)
        {
            int random = UnityEngine.Random.Range(0, 10);
            if (random < 5)
                enemyUnit.Attack(myUnit, OnAttackPerformed);
            else
                enemyUnit.Heal(OnHealingPerformed);
        }
        else
        {
            enemyUnit.Attack(myUnit, OnAttackPerformed);
        }
        
        UpdateBattleState(BattleState.MyTurn);
    }

    private IEnumerator DisplayEndOfGame()
    {
        yield return new WaitForSeconds(1f);
        losePanel.SetActive(battleState == BattleState.Lose);
        winPanel.SetActive(battleState == BattleState.Win);
        Setup();
    }

    private void OnUnitPerformedAction(string actionLog)
    {
        battleLogs.text = actionLog;
    }
}