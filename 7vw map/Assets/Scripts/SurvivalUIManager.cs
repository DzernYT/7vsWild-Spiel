using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SurvivalUIManager : MonoBehaviour
{
    [SerializeField] private SurvivalManager _survivalManager;
    [SerializeField] private Image _hungerMeter, _thirstMeter, _staminaMeter, _mentalMeter, _healthMeter;
    // Start is called before the first frame update
    private void FixedUpdate()
    {
        _hungerMeter.fillAmount = _survivalManager.HungerPercent;
        _thirstMeter.fillAmount = _survivalManager.ThirstPercent;
        _staminaMeter.fillAmount = _survivalManager.StaminaPercent;
        _mentalMeter.fillAmount = _survivalManager.MentalPercent;
        _healthMeter.fillAmount = _survivalManager.HealthPercent;
    }
}
