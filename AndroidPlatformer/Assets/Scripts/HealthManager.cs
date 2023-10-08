using UnityEngine;
using UnityEngine.Events;

public class HealthManager : MonoBehaviour
{
    public UnityEvent<float, float> OnHealthChanged;

    private void OnEnable()
    {
        PlayerHealth.OnHealthSubtracted += RemoveHealth;
    }
    private void OnDisable()
    {
        PlayerHealth.OnHealthSubtracted -= RemoveHealth;        
    }
    void RemoveHealth(float _currentHealth, float _maxHealth)
    {
        OnHealthChanged?.Invoke(_currentHealth, _maxHealth);
    }
}
