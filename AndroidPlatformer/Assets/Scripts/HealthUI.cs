using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    [SerializeField] Image _healthBar;

    public void ChangeHealthUI(float _currentHealth, float _maxHealth)
    {
        _healthBar.fillAmount = _currentHealth/_maxHealth;
    }
}
