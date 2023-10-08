using UnityEngine;
[CreateAssetMenu(fileName = "Enemy", menuName = "ScriptableObjects/Enemies")]
public class EnemySO : ScriptableObject
{
    [Header("ENEMY STATS:")]
    public float _health;
    public int _damage;
    public float _speed;
    [Tooltip("How much less/more damage it takes in percent (Ex. 100% corresponds to full damage)")] public int _damageMitigation;

    [Header("SCORE TO ADD:")]
    public int _score;
}
