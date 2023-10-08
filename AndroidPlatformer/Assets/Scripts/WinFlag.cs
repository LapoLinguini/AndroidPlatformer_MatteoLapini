using System;
using UnityEngine;

public class WinFlag : MonoBehaviour
{
    [SerializeField] GameObject _Flag;

    public static Action OnWinAction;

    Animator anim;
    private void Awake()
    {
        anim = GetComponent<Animator>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerHealth>())
        {
            OnWinAction?.Invoke();
            GetComponent<Collider2D>().enabled = false;
            anim.SetTrigger("CheckTaken");
        }
    }
}
