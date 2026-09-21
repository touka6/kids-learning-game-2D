using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameLogic : MonoBehaviour
{
    public Canvas canvas;
    public TMP_Text displayedScore;
    public TMP_Text displayedTarget;
    public TMP_Text displayedTimer;

    int Score = 0;
    void Update()
    {
        
    }
    public void IncScore()
    {
        Score+=1;
        Debug.Log(Score);
        displayedScore.text = Score.ToString();    

    }
}
