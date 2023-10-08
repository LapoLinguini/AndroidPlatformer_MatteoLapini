using UnityEngine;
public interface IDamageable
{
    public void Damage(float _damage);
    void Knockback(Vector2 _direction);
}
