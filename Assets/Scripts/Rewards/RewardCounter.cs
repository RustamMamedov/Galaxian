using UnityEngine;

[CreateAssetMenu]
public class RewardCounter : ScriptableObject
{
    private const string HIGH_SCORE = "highScore";

    public IntegerValue currentScore;
    public IntegerValue highScore;

    public void AddReward(int reward)
    {
        currentScore.Value += reward;
        UpdateHighScore();
    }

    public void InitScore()
    {
        currentScore.Value = 0;
        highScore.Value = PlayerPrefs.GetInt(HIGH_SCORE, 0);
    }

    private void UpdateHighScore()
    {
        if (currentScore.Value <= highScore.Value)
            return;

        highScore.Value = currentScore.Value;
        PlayerPrefs.SetInt(HIGH_SCORE, highScore.Value);
    }
}
