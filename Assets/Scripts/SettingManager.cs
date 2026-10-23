using System;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingManager : MonoBehaviour
{
    public enum Difficulty
    {
        Easy,
        Medium,
        Hard,
    }
    public enum StageTime
    {
        OneMinute,
        TwoMinutes,
        ThreeMinutes,
        FourMinutes,
        FiveMinutes,
    }
    public enum BubbleDirection
    {
        Down,
        Up
    }
    public GameLogic gl;
    public BubbleSpawner bs;
    public AudioMixer audioMixer;
    public GameObject upperRect;
    public GameObject lowerRect;

    [Header("Game Settings")]
    public Difficulty difficulty;
    public StageTime stageTime;

    [Header("Bubble direction")]
    public BubbleDirection bubbleDirection = BubbleDirection.Down;
    // public Toggle directionToggle;
    
    

    [Header("Audio Settings")]
    [Range(-80f, 0f)] public float masterVolume = 0f;
    [Range(-80f, 0f)] public float musicVolume = -10f;
    [Range(-80f, 0f)] public float sfxVolume = 0f;
    [Range(-80f, 0f)] public float voiceVolume = 0f;

    void Start()
    {
        SetStageTime();
        SetDifficulty();
        SetAudioVolume();

        // directionToggle.SetIsOnWithoutNotify(bubbleDirection == BubbleDirection.Up);
        // directionToggle.onValueChanged.AddListener(SetBubbleDirectionToggle);

        SetBubbleDirection();
    }


    void SetAudioVolume()
    {
        audioMixer.SetFloat("MasterVol", masterVolume);
        audioMixer.SetFloat("MusicVol", musicVolume);
        audioMixer.SetFloat("SFXVol", sfxVolume);
        audioMixer.SetFloat("VoiceVol", voiceVolume);
    }
    public void SetDifficulty()
    {
        if (difficulty == Difficulty.Easy)
        {
            gl.normalBubbleSpeed = 1.5f;
            gl.fastBubbleSpeed = 2f;
        }
        else if (difficulty == Difficulty.Medium)
        {
            gl.normalBubbleSpeed = 1.5f;
            gl.fastBubbleSpeed = 2.5f;
        }
        else if (difficulty == Difficulty.Hard)
        {
            gl.normalBubbleSpeed = 2f;
            gl.fastBubbleSpeed = 3f;
        }
    }
    public void SetStageTime()
    {
        float time = 60f;

        if (stageTime == StageTime.OneMinute)
            time = 60f;
        else if (stageTime == StageTime.TwoMinutes)
            time = 120f;
        else if (stageTime == StageTime.ThreeMinutes)
            time = 180f;
        else if (stageTime == StageTime.FourMinutes)
            time = 240f;
        else if (stageTime == StageTime.FiveMinutes)
            time = 300f;

        gl.numStageDuration = time;
        gl.alphStageDuration = time;

        gl.ChangeSpeedTime = time/2f;
        // gl.targetChangeAt = time/4f;
    }
    public void SetBubbleDirection()
    {
        if (bubbleDirection == BubbleDirection.Up)
        {
            upperRect.SetActive(true);
            lowerRect.SetActive(false);
        }
        else
        {
            upperRect.SetActive(false);
            lowerRect.SetActive(true);
        }
    }
    public void SetBubbleDirectionToggle(bool isUp)
    {
        if (isUp)
            bubbleDirection = BubbleDirection.Up;
        else
            bubbleDirection = BubbleDirection.Down;
        
        SetBubbleDirection();
    }
    public BubbleMovement.Direction GetDirection()
    {
        if (bubbleDirection == BubbleDirection.Up)
            return BubbleMovement.Direction.Ascending;
        else
            return BubbleMovement.Direction.Descending;
    }
}
