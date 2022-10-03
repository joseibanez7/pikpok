using UnityEngine;

public class StoreCatalogInstantiator : MonoBehaviour
{
    [SerializeField] private EquipmentCatalog equipmentCatalog;
    [SerializeField] private UIStoreItem storeItemPrefab;

    private void Awake()
    {
        foreach (StoreItemData itemData in equipmentCatalog.GetCatalog())
        {
            UIStoreItem newStoreItem = Instantiate(storeItemPrefab, transform);
            newStoreItem.Setup(itemData);
        }
    }
}
