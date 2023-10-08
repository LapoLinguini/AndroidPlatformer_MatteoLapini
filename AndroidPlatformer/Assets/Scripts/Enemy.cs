using System;
using System.Collections;
using UnityEngine;
public class Enemy : MonoBehaviour, IDamageable
{
    [Header("Enemy Type:")]
    [SerializeField] EnemySO EnemyProperties;

    //properties
    public float _health;
    int _damage;
    float _speed;
    int _damageMitigation;
    int _score;

    public static Action<int> OnScoreAdded;

    Rigidbody2D rb;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void Start()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, 0);
        _health = EnemyProperties._health;
        _damage = EnemyProperties._damage;
        _speed = EnemyProperties._speed;
        _damageMitigation = EnemyProperties._damageMitigation;
        _score = EnemyProperties._score;
    }
    public void Damage(float _damage)
    {
        _health -= (_damage / 100) * _damageMitigation;
        StartCoroutine(HitColor());

        if (_health <= 0)
        {
            OnScoreAdded?.Invoke(_score);
            Destroy(gameObject);
        }
    }
    public void Knockback(Vector2 _direction)
    {
        rb.velocity = Vector2.zero;
        rb.AddForce(_direction * 4, ForceMode2D.Impulse);
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out IDamageable hit) && collision.GetComponent<PlayerHealth>())
        {
            hit.Damage(_damage);
            hit.Knockback(new Vector2(Mathf.Sign((collision.transform.position - transform.position).normalized.x), 1));
        }
    }
    IEnumerator HitColor()
    {
        Color originalColor = gameObject.GetComponentInChildren<SpriteRenderer>().color;
        gameObject.GetComponentInChildren<SpriteRenderer>().color = Color.red;

        yield return new WaitForSeconds(0.1f);

        gameObject.GetComponentInChildren<SpriteRenderer>().color = originalColor;
    }

}
