using UnityEngine;
using TMPro;
using System.Collections;
using System.Collections.Generic;

public class GameLogic : MonoBehaviour
{
    public enum GameStage
    {
        Numbers,
        Transition,
        Alphabet,
        Finished
    }
    GameStage currentStage;

    [Header("UI")]
    public Canvas canvas;
    public TMP_Text displayedScore;
    public TMP_Text displayedTarget;
    public TMP_Text displayedTimer;
    public TMP_Text txt;

    [Header("Bubble Speed")]
    public float bubbleSpeed = 2.5f;
    public float normalBubbleSpeed = 2.5f;
    public float fastBubbleSpeed = 4f;

    [Header("Timer")]
    public float scrnTimer = 60.0f;
    public float numStageDuration = 60.0f;
    public float alphStageDuration = 60.0f;
    public float ChangeSpeedTime = 30f;
    public float targetChangeAt = 10f;
    public float playTargetInterval = 3f;


    bool TimerEnded;
    string Target;

    [Header("Other")]
    public BubbleSpawner spawner;
    BubbleData data;
    public Transform bubbleParent;
    public GameEffects ge;
    

    int Score = 0;
    char[] alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();

    HashSet<int> NumHS = new();
    HashSet<int> AlphHS = new();

    
    void Start()
    {
        // StartNumStage();
        StartCoroutine(Starter(GameStage.Numbers));
    }
    void Update()
    {
        CheckTime();
    }


//          STAGES
//############################
    void NextStage()
    {
        if(currentStage == GameStage.Numbers)
        {
            StartCoroutine(Starter(GameStage.Alphabet));
        }
        else if(currentStage == GameStage.Alphabet)
        {
            // currentStage = GameStage.Finished;
            // FinishedStage();
            StartCoroutine(Starter(GameStage.Finished));
        }
    }
    void StartNumStage()
    {
        currentStage = GameStage.Numbers;
        scrnTimer = numStageDuration;
        bubbleSpeed = normalBubbleSpeed;
        TimerEnded = false;
        RandNumTarget();
        StartCoroutine(ChangeTarget());
    }
    void StartAlphStage()
    {
        currentStage = GameStage.Alphabet;
        txt.text = "";
        scrnTimer = alphStageDuration;
        bubbleSpeed = normalBubbleSpeed;
        TimerEnded = false;
        RandAlphTarget();
        StartCoroutine(ChangeTarget());
    }
    void FinishedStage()
    {
        currentStage = GameStage.Finished;
        if (Score >= 3)
        {
            txt.text = "You Win!";
        }
        else
        {
            txt.text = "You Lost!";
        }
    }
    public GameStage GetCurrentStage()
    {
        return currentStage;
    }
//############################


//          TARGET
//############################    
    public float RandNumTarget()
    {
        if(NumHS.Count>=10)
            NumHS.Clear();
        int rnd = Random.Range(1,11);
        while (NumHS.Contains(rnd))
            rnd = Random.Range(1,11);

        NumHS.Add(rnd);
        displayedTarget.text = rnd.ToString();
        Target = rnd.ToString();
        ge.PlayNum(Target, playTargetInterval);
        return rnd;
    }
    public float RandAlphTarget()
    {
        if(AlphHS.Count>=26)
            AlphHS.Clear();
        int rnd = Random.Range(0,26);
        while (AlphHS.Contains(rnd))
            rnd = Random.Range(0,26);

        AlphHS.Add(rnd);
        displayedTarget.text = alphabet[rnd].ToString();
        Target = alphabet[rnd].ToString();
        ge.PlayAlph(Target, playTargetInterval);
        return rnd;
    }
//############################


//          OTHERS
//############################
    void CheckTime()
    {
        if(TimerEnded)
            return;

        scrnTimer -= Time.deltaTime;

        ChangeSpeed();
        
        if (scrnTimer <= 0f)
        {
            scrnTimer = 0f;
            TimerEnded = true;
            displayedTimer.text = "0";
            NextStage();
            // Debug.Log("time's up");
            return;
        }
        displayedTimer.text = Mathf.CeilToInt(scrnTimer).ToString("#");
    }
    public bool CheckScore(GameObject obj)
    {
        data = obj.GetComponent<BubbleData>();
        if(Target == data.GetValue())
        {
            Score+=1;
            // Debug.Log(Score);
            displayedScore.text = Score.ToString();  
            return true;  
        }
        return false;
    }

    void ChangeSpeed()
    {
        if(scrnTimer <= ChangeSpeedTime)
        {
            bubbleSpeed = fastBubbleSpeed;
        }
    }
    public string GetTarget()
    {
        return Target;
    }
    IEnumerator Starter(GameStage nextStage)
    {
        currentStage = GameStage.Transition;
        TimerEnded = true;

        if (nextStage != GameStage.Numbers)
        {
            yield return new WaitUntil(() => bubbleParent.childCount == 0);
        }

        if (nextStage == GameStage.Finished)
        {
            yield return new WaitForSeconds(1f);
            FinishedStage();
            yield break;
        }

        txt.text = "1";
        yield return new WaitForSeconds(1f);

        txt.text = "2";
        yield return new WaitForSeconds(1f);

        txt.text = "3";
        yield return new WaitForSeconds(1f);

        if (nextStage == GameStage.Numbers)
        {
            txt.text = "Stage 1";
            yield return new WaitForSeconds(1f);

            txt.text = "";
            StartNumStage();
            spawner.StartSpawning();
        }
        else if (nextStage == GameStage.Alphabet)
        {
            txt.text = "Stage 2";
            yield return new WaitForSeconds(1f);

            txt.text = "";
            StartAlphStage();
            spawner.StartSpawning();
        }
        
    }
    IEnumerator ChangeTarget()
    {
        while (currentStage==GameStage.Numbers || currentStage == GameStage.Alphabet)
        {
            yield return new WaitForSeconds(targetChangeAt);
            if(currentStage == GameStage.Numbers)
            {
                //yield...
                RandNumTarget();
            }
            else if (currentStage == GameStage.Alphabet)
            {
                RandAlphTarget();
            }
        }
    }
//############################
}
