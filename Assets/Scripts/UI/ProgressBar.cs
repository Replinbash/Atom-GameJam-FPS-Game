using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    [SerializeField] private Image _fill;
    [SerializeField] private Text _amount;

    private int _baseValue;
    private int _maxValue;

    public void SetValues(int baseValue, int maxValue)
    {
        _baseValue = baseValue;
        _maxValue = maxValue;

        _amount.text = _baseValue.ToString();
        CalculateFillAmount();
    }

    public void CalculateFillAmount()
    {
        var _fillAmount = (float)_baseValue / (float)_maxValue;
        _fill.fillAmount = _fillAmount;
    }
}
