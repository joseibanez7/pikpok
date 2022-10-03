using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIStoreItem : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] private TextMeshProUGUI title;

    public void Setup(StoreItemData itemData)
    {
        image.sprite = itemData.sprite;
        title.text = itemData.name;
    }
}
