using UnityEngine;
using TMPro;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;

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
    public CanvasGroup cg;

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
        StartCoroutine(ChangeTarget(GameStage.Numbers));
    }
    void StartAlphStage()
    {
        currentStage = GameStage.Alphabet;
        txt.text = "";
        scrnTimer = alphStageDuration;
        bubbleSpeed = normalBubbleSpeed;
        TimerEnded = false;
        RandAlphTarget();
        StartCoroutine(ChangeTarget(GameStage.Alphabet));
    }
    void FinishedStage()
    {
        cg.alpha=1f;
        currentStage = GameStage.Finished;
        if (Score >= 3)
        {
            txt.text = $"You Win!\nYour Score: {Score}";
            ge.StartCongratsParticle();
        }
        else
        {
            txt.text = $"You Lost!\nYour Score: {Score}";
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
        {
            // ge.StopPlayingTarget();
            return;
        }
            

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


//        IEnumerator
//############################    
    IEnumerator Starter(GameStage nextStage)
    {
        currentStage = GameStage.Transition;
        TimerEnded = true;

        if (nextStage != GameStage.Numbers)
        {
            yield return new WaitUntil(() => bubbleParent.childCount == 0);
            ge.StopPlayingTarget();
        }

        if (nextStage == GameStage.Finished)
        {
            yield return new WaitForSeconds(1f);
            ge.StopPlayingTarget();
            FinishedStage();
            yield break;
        }
        
        
        txt.text = "1";
        cg.alpha = 1f;
        cg.DOFade(0f,0.5f).SetDelay(0.3f);
        yield return null;
        ge.PlayStartCountDown();
        yield return new WaitForSeconds(1f);

        ge.PlayStartCountDown();
        txt.text = "2";
        cg.alpha = 1f;
        cg.DOFade(0f,0.5f).SetDelay(0.3f);
        yield return new WaitForSeconds(1f);

        ge.PlayStartCountDown();
        txt.text = "3";
        cg.alpha = 1f;
        cg.DOFade(0f,0.5f).SetDelay(0.3f);
        yield return new WaitForSeconds(1f);

        if (nextStage == GameStage.Numbers)
        {
            cg.alpha = 1f;
            txt.text = "Stage 1";
            cg.DOFade(0f,0.5f).SetDelay(0.5f);
            ge.PlayEndCountDown();
            yield return new WaitForSeconds(1f);

            txt.text = "";
            StartNumStage();
            spawner.StartSpawning();
        }
        else if (nextStage == GameStage.Alphabet)
        {
            cg.alpha = 1f;
            txt.text = "Stage 2";
            cg.DOFade(0f,0.5f).SetDelay(0.5f);
            ge.PlayEndCountDown();
            yield return new WaitForSeconds(1f);

            txt.text = "";
            StartAlphStage();
            spawner.StartSpawning();
        }
        
    }
    IEnumerator ChangeTarget(GameStage stage)
    {
        while (currentStage==stage)
        {
            yield return new WaitForSeconds(targetChangeAt);

            if(currentStage != stage)
                yield break;

            if(currentStage == GameStage.Numbers)
                RandNumTarget();
            else if (currentStage == GameStage.Alphabet)
                RandAlphTarget();
        }
    }
//############################
}
