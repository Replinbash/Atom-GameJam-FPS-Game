using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour 
{
    [SerializeField] private Image _barSprite;
    [SerializeField] private Text _amount;

    public void UpdateBar(int baseValue, int maxValue)
    {
		_barSprite.fillAmount = (float)baseValue / (float)maxValue;
		_amount.text = baseValue.ToString();
    }
}
