using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHUD : MonoBehaviour
{
    [SerializeField] private ProgressBar _chargeBar, _healthBar, _staminaBar;

    public void UptadeHealth(int currentHealth, int maxHealth) => _healthBar.SetValues(currentHealth, maxHealth);

    public void UptadeCharge(int currentCharge, int maxCharge) => _chargeBar.SetValues(currentCharge, maxCharge);

    public void UptadeStamina(int currentStamina, int maxStamina) => _staminaBar.SetValues(currentStamina, maxStamina);
}



