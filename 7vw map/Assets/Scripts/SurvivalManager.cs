using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SurvivalManager : MonoBehaviour
{
    [Header("Hunger")]
    [SerializeField] private float _maxHunger = 100f;
    [SerializeField] private float _hungerDepletionRate = 1f;
    private float _currentHunger;
    public float HungerPercent => _currentHunger / _maxHunger;

    [Header("Thirst")]
    [SerializeField] private float _maxThirst = 100f;
    [SerializeField] private float _thirstDepletionRate = 1f;
    private float _currentThirst;
    public float ThirstPercent => _currentThirst / _maxThirst;

    [Header("Mental")]
    [SerializeField] private float _maxMental = 100f;
    [SerializeField] private float _MentalDepletionRate = 0.01f;
    private float _currentMental;
    public float MentalPercent => _currentMental / _maxMental;

    [Header("Stamina")]
    [SerializeField] private float _maxStamina = 100f;
    [SerializeField] private float _staminaDepletionRate = 1f;
    [SerializeField] private float _staminaRechargeRate = 2f;
    [SerializeField] private float _staminaRechargeDelay = 1f;
    private float _currentStamina;
    private float _currentStaminaDelayCounter;
    public float StaminaPercent => _currentStamina / _maxStamina;

    [Header("Health")]
    [SerializeField] private float _maxHealth = 100f;
    private float _currentHealth;
    public float HealthPercent => _currentHealth / _maxHealth;

    [Header("Player References")]
    [SerializeField] private PlayerMovment _player;

    public static UnityAction OnPlayerDied;

    public bool hasStamina = true;

    public static SurvivalManager instance;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    private void Start()
    {
        _currentHunger = _maxHunger;
        _currentThirst = _maxThirst;
        _currentStamina = _maxStamina;
        _currentMental = _maxMental;
        _currentHealth = _maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        _currentHunger -= _hungerDepletionRate * Time.deltaTime;
        _currentThirst -= _thirstDepletionRate * Time.deltaTime;
        _currentMental -= _MentalDepletionRate * Time.deltaTime;

        if(_currentHunger <= 0 || _currentThirst <= 0 || _currentMental <= 0 || _currentHealth <= 0)
        {
            OnPlayerDied?.Invoke();
            _currentHunger = 0;
            _currentThirst = 0;
            _currentMental = 0;
            _currentHealth = 0;
        }

        if (_player.isSprinting)
        {
            _currentStamina -= _staminaDepletionRate * Time.deltaTime;
            if (_currentStamina <= 0) _currentStamina = 0;
            _currentStaminaDelayCounter = 0;
        }
        if(!_player.isSprinting && _currentStamina < _maxStamina)
        {
            if(_currentStaminaDelayCounter < _staminaRechargeDelay)
            {
                _currentStaminaDelayCounter += Time.deltaTime;
            }

            if (_currentStaminaDelayCounter >= _staminaRechargeDelay)
            {
                _currentStamina += _staminaRechargeRate * Time.deltaTime;
                if (_currentStamina > _maxStamina)
                {
                    _currentStamina = _maxStamina;
                }
            }
        }

        if(_currentStamina > 0)
        {
            hasStamina = true;
        }else if (_currentStamina == 0)
        {
            hasStamina = false;
        }

    }

    public void ReplenishHungerThirst(float hungerAmount, float thirstAmount)
    {
        _currentHunger += hungerAmount;
        _currentThirst += thirstAmount;

        if (_currentHunger > _maxHunger) _currentHunger = _maxHunger;
        if (_currentThirst > _maxThirst) _currentThirst = _maxThirst;
    }

    public void loseHealth(float amount)
    {
        _currentHealth -= amount;
    }


}
