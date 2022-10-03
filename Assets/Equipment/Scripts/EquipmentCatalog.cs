using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EquipmentCatalog", menuName = "ScriptableObjects/EquipmentCatalog", order = 1)]
public class EquipmentCatalog : ScriptableObject
{
    [SerializeField] private List<EquipmentData> equipmentCatalog;

    public List<EquipmentData> GetCatalog() =>
        equipmentCatalog;

    public List<EquipmentData> GetCatalogByType(EquipmentType type) =>
        equipmentCatalog.FindAll(equipment => equipment.type == type);
}
