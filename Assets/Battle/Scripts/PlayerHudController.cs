using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHudController : MonoBehaviour
{
    [SerializeField] private Image characterPreview;
    [SerializeField] private BarController healthBarController;
    [SerializeField] private TextMeshProUGUI unitName;

    private Unit unit;

    public void Setup(Unit unit)
    {
        this.unit = unit;
        characterPreview.sprite = unit.CharacterPreview;
        unitName.text = unit.Name;
        UpdateHealth();
    }

    public void UpdateHealth()
    {
        healthBarController.SetFillPercentage(unit.CurrentHealthPercentage);
    }
}
