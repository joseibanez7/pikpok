using System;
using UnityEngine;
using UnityEngine.UI;

public class Unit : MonoBehaviour
{
    [SerializeField] private UnitStats stats;
    [SerializeField] private Image characterPreview;
    [SerializeField] private string unitName;

    public Action<string> OnUnitActionPerformed;

    public Sprite CharacterPreview => characterPreview.sprite;
    public string Name => unitName;
    public bool IsEnemy => isEnemy;
    public float CurrentHealthPercentage => currentHealth / stats.maxHealth;

    private float currentHealth;
    private bool isEnemy;

    public void Setup(bool isEnemy)
    {
        currentHealth = stats.maxHealth;
        this.isEnemy = isEnemy;
    }

    public void Attack(Unit target, Action<float, Unit, Unit> OnAttackPerformed)
    {
        target.ReceiveDamage(this, OnAttackPerformed);
    }

    public void Heal(Action<float, Unit> OnHealingPerformed)
    {
        float healingAmount = stats.healingPower;
        if (currentHealth + healingAmount > stats.maxHealth)
            healingAmount = stats.maxHealth - currentHealth;

        currentHealth += healingAmount;

        if (healingAmount > 0)
            OnUnitActionPerformed?.Invoke($"{unitName} healed! +{stats.healingPower}HP");
        else
            OnUnitActionPerformed?.Invoke($"{unitName} already has max HP!");

        OnHealingPerformed(healingAmount, this);
    }

    public void ReceiveDamage(Unit unitAttackSource, Action<float, Unit, Unit> OnDamageReceived)
    {
        float effectiveDamage = unitAttackSource.stats.damage * stats.shield;
        currentHealth = currentHealth - effectiveDamage < 0 ? 0 : currentHealth - effectiveDamage;
        if (effectiveDamage > 0)
            OnUnitActionPerformed?.Invoke($"{unitName} received damage from {unitAttackSource.Name}! -{effectiveDamage}HP");
        OnDamageReceived?.Invoke(effectiveDamage, this, unitAttackSource);
    }
}
