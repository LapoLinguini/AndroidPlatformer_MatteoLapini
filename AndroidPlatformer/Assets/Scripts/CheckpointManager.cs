using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
    public static Vector2 respawnPoint;

    private void OnEnable()
    {
        CheckPoint.OnCheckpointEnter += UpdateLastCheckpoint;
    }
    private void OnDisable()
    {
        CheckPoint.OnCheckpointEnter -= UpdateLastCheckpoint;
    }
    void UpdateLastCheckpoint(Vector2 _lastCheckpoint)
    {
        respawnPoint = _lastCheckpoint;
    }
}
