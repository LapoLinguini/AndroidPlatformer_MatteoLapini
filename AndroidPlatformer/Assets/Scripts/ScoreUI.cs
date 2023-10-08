using TMPro;
using UnityEngine;

public class ScoreUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _scoreText;

    public void ChangeScoreUI(int _score)
    {
        _scoreText.text = "Score: " + _score.ToString();
    }
}
