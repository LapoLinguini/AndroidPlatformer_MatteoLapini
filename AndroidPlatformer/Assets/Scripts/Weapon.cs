using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] float _damage;
    [SerializeField] Transform _playerT;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out IDamageable hit))
        {
            hit.Damage(_damage);
            hit.Knockback(new Vector2(Mathf.Sign((collision.transform.position - _playerT.position).normalized.x), 1));
        }
    }
}
