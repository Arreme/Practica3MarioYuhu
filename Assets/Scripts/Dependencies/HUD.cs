using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class HUD : MonoBehaviour
{
    public Text score;
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
        score.text = scoreManager.getPoints().ToString();
    }
}
