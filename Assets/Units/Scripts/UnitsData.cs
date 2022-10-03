using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "UnitsData", menuName = "ScriptableObjects/UnitsData", order = 1)]
public class UnitsData : ScriptableObject
{
    [SerializeField] private List<Unit> allies;
    [SerializeField] private List<Unit> enemies;

    public List<Unit> GetAllyUnits() => allies;

    public List<Unit> GetEnemyUnits() => enemies;

    public Unit GetRandomAllyUnit() => allies[Random.Range(0, allies.Count)];

    public Unit GetRandomEnemyUnit() => enemies[Random.Range(0, enemies.Count)];
}
