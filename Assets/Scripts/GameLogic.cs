using UnityEngine;
using TMPro;
using System.Collections;

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
    public float TargetTimer = 60.0f;
    public float numStageDuration = 60.0f;
    public float alphStageDuration = 60.0f;
    public float ChangeSpeedTime = 30f;
    public float targetChangeAt = 10f;


    bool TimerEnded;
    string Target;

    [Header("Other")]
    public BubbleSpawner spawner;
    BubbleData data;
    public Transform bubbleParent;
    

    int Score = 0;
    char[] alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();

    
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
        TargetTimer = numStageDuration;
        bubbleSpeed = normalBubbleSpeed;
        TimerEnded = false;
        RandNumTarget();
        StartCoroutine(ChangeTarget());
    }
    void StartAlphStage()
    {
        currentStage = GameStage.Alphabet;
        txt.text = "";
        TargetTimer = alphStageDuration;
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
        float rnd = Random.Range(1,11);
        displayedTarget.text = rnd.ToString();
        Target = rnd.ToString();
        //test
        return rnd;
    }
    public float RandAlphTarget()
    {
        int rnd = Random.Range(0,26);
        displayedTarget.text = alphabet[rnd].ToString();
        Target = alphabet[rnd].ToString();
        //test
        return rnd;
    }
//############################


//          OTHERS
//############################
    void CheckTime()
    {
        if(TimerEnded)
            return;

        TargetTimer -= Time.deltaTime;

        ChangeSpeed();
        
        if (TargetTimer <= 0f)
        {
            TargetTimer = 0f;
            TimerEnded = true;
            displayedTimer.text = "0";
            NextStage();
            // Debug.Log("time's up");
            return;
        }
        displayedTimer.text = Mathf.CeilToInt(TargetTimer).ToString("#");
    }
    public void CheckScore(GameObject obj)
    {
        data = obj.GetComponent<BubbleData>();
        if(Target == data.GetValue())
        {
            Score+=1;
            // Debug.Log(Score);
            displayedScore.text = Score.ToString();    
        }
        
    }

    void ChangeSpeed()
    {
        if(TargetTimer <= ChangeSpeedTime)
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
