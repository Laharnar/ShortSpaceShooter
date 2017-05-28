

using System;
using UnityEngine.UI;

[System.Serializable]
public class Score {
    public int goalScore = 30;
    int score = 0;

    /// <summary>
    /// How many numbers are on score ui.
    /// </summary>
    const int scoreTextWidth = 4;

    public Score(int startingScore) {
        this.score = startingScore;
    }

    internal bool IsGoalMet() {
        return score >= goalScore;
    }

    public void AddScore(int score) {
        this.score += score;
    }

    internal void UpdateUi(Text ui) {
        if (score > 9999) ui.text = "Too much!";
        // Add zeros in front, if score is less than score width.
        string s = score.ToString();
        while (s.Length < scoreTextWidth)
            s = "0" + s;
        ui.text = s;
    }
}
