using Cinemachine;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject playerPrefab;
    [Space]
    [SerializeField] UnityEvent OnDeathEvent;
    [SerializeField] UnityEvent OnWinEvent;

    public static bool WonOrLost = false;
    private void OnEnable()
    {
        PlayerHealth.OnDeathAction += OnDeathTrigger;
        WinFlag.OnWinAction += OnWinTrigger;
    }
    private void OnDisable()
    {
        PlayerHealth.OnDeathAction -= OnDeathTrigger;
        WinFlag.OnWinAction -= OnWinTrigger;
    }
    void OnDeathTrigger()
    {
        WonOrLost = true;
        OnDeathEvent?.Invoke();
    }
    void OnWinTrigger()
    {
        WonOrLost = true;
        OnWinEvent?.Invoke();
    }
    public void RespawnPlayer()
    {
        WonOrLost = false;
        GameObject _player = Instantiate(playerPrefab, CheckpointManager.respawnPoint, Quaternion.identity);
        CinemachineVirtualCamera virtualCam = FindObjectOfType<CinemachineVirtualCamera>();
        virtualCam.Follow = _player.transform;
    }
    public void PlayAgain()
    {
        WonOrLost = false;
        SceneManager.LoadScene("1-01");
    }
}
