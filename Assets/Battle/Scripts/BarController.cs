using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarController : MonoBehaviour
{
    [SerializeField] private Image barFill;

    public void SetFillPercentage(float percentage)
    {
        barFill.transform.localScale = new Vector3(percentage, 1, 1);
    }
}
