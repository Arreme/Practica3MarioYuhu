using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HUD : MonoBehaviour
{
    public TextMeshProUGUI score;
    private void Start()
    {
        DependencyContainer.GetDependency<IScoreManager>().scoreChangedDelegate += updateScore;
    }
    private void OnDestroy()
    {
        DependencyContainer.GetDependency<IScoreManager>().scoreChangedDelegate -= updateScore;
    }
    public void updateScore(IScoreManager scoreManager)
    {
        score.text = "Score: " + scoreManager.getPoints().ToString("0");
    }
}
