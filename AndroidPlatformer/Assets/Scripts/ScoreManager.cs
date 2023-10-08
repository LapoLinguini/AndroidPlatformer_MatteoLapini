using UnityEngine;
using UnityEngine.Events;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] int score;
    public UnityEvent<int> OnScoreChanged;

    private void OnEnable()
    {
        Enemy.OnScoreAdded += AddScore;
    }
    private void OnDisable()
    {       
        Enemy.OnScoreAdded -= AddScore;
    }
    void AddScore(int _score)
    {
        score += _score;
        OnScoreChanged?.Invoke(score);
    }
    public void ResetScore()
    {
        score = 0;
        OnScoreChanged?.Invoke(score);
    }
}
