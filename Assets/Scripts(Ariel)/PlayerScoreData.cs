using UnityEngine;

[CreateAssetMenu(fileName = "PlayerScoreData", menuName = "Data/Player Score Data")]
public class PlayerScoreData : ScriptableObject
{
    public int score;

    public void ResetScore()
    {
        score = 0;
    }

    public void AddScore(int amount)
    {
        score += amount;
    }
}

