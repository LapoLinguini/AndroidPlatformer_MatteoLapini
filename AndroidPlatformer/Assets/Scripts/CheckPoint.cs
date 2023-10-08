using System;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    [SerializeField] GameObject _Flag;

    public static Action<Vector2> OnCheckpointEnter;

    Animator anim;
    private void Awake()
    {
        anim = GetComponent<Animator>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerHealth>())
        {
            OnCheckpointEnter?.Invoke(transform.position);
            GetComponent<Collider2D>().enabled = false;
            anim.SetTrigger("CheckTaken");
        }
    }
}
