using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour, IDamageable
{
    [Header("Health Stats:")]
    [SerializeField] float _maxHealth;
    [SerializeField] float _currentHealth;

    [Header("Knockback:")]
    [SerializeField] float _knockForce;
    [Tooltip("Time in seconds during which the player is unable to use controls, as it has been knockbacked")][SerializeField] float _knockTime;
    public static bool isKnocked = false;

    [Header("Invincibility Stats:")]
    [Tooltip("Time in second during which the player is invincible after taking a hit")][SerializeField] float _invincibilityTime;
    [SerializeField] float _amplitude;
    [SerializeField] float _frequence;
    [SerializeField] float _startingPoint;
    [SerializeField] float _offset;

    bool invincible = false;

    [SerializeField] List<SpriteRenderer> playerRenderers;

    public static Action<float, float> OnHealthSubtracted;
    public static Action OnDeathAction;

    private void Start()
    {
        _currentHealth = _maxHealth;
        invincible = false;
        isKnocked = false;
        OnHealthSubtracted?.Invoke(_currentHealth, _maxHealth);
    }
    public void Damage(float _damage)
    {
        if (invincible) return;

        _currentHealth -= _damage;

        OnHealthSubtracted?.Invoke(_currentHealth, _maxHealth);

        if (_currentHealth <= 0)
        {
            OnDeathAction?.Invoke();
            Destroy(gameObject);
        }
    }
    public void Knockback(Vector2 _direction)
    {
        if (invincible) return;

        StartCoroutine(InvincibilityPeriod());

        Movement.rb.velocity = Vector2.zero;
        Movement.rb.AddForce(_direction * _knockForce, ForceMode2D.Impulse);
    }
    IEnumerator InvincibilityPeriod()
    {
        invincible = true;
        float time = 0;
        float alpha;

        while (time < _invincibilityTime)
        {
            time += Time.deltaTime;

            isKnocked = time > _knockTime ? false : true;

            alpha = Mathf.Sin(time * _frequence + Mathf.PI * _startingPoint) * _amplitude + _offset;

            foreach (var renderer in playerRenderers)
            {
                Color rendererColor = renderer.GetComponent<SpriteRenderer>().color;
                rendererColor.a = alpha;
                renderer.GetComponent<SpriteRenderer>().color = rendererColor;
            }
            yield return null;
        }
        foreach (var renderer in playerRenderers)
        {
            Color rendererColor = renderer.GetComponent<SpriteRenderer>().color;
            rendererColor.a = 1;
            renderer.GetComponent<SpriteRenderer>().color = rendererColor;
        }
        invincible = false;
    }
}
