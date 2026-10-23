using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class BubbleSpawner : MonoBehaviour
{
    Camera cam;
    public GameObject obj;
    public TargetSpawner ts;
    public Transform bubbleParent;
    GameObject tst;
    public float waitScnds = 1.5f;
    int count = 0;
    public int targetSpawnInterval = 5;
    public int margin = 1;

    public GameLogic gl;
    char[] alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();

    public GameEffects ge;
    public SettingManager settings;

    void Awake()
    {
        cam = Camera.main;
    }
    IEnumerator Waiter(GameObject bubble)
    {
        HashSet<int> hs = new();
        bool isDuplicate = false;
        int previousLen = 0;

        while (gl.scrnTimer>0f)
        {
            int left = (int)cam.ViewportToWorldPoint(new Vector3(0f, 0f, 0f)).x;
            int right = (int)cam.ViewportToWorldPoint(new Vector3(1f, 0f, 0f)).x;
            int len = (right - margin) - (left + margin);
            
            if (len != previousLen)
            {
                hs.Clear();
                previousLen = len;
            }

            int rnd = Random.Range(left+margin, right-margin);

            if(hs.Count >= previousLen)
            {
                hs.Clear();
            }

            tst = Instantiate(bubble, bubbleParent);
            count+=1;
            BubbleMovement mv = tst.GetComponent<BubbleMovement>();
            BubbleBehaviour behaviour = tst.GetComponent<BubbleBehaviour>();
            behaviour.ge = ge;
            mv.SetSpeed(gl.bubbleSpeed);
            mv.SetDirection(settings.GetDirection());

            if(count==targetSpawnInterval)
            {
                ts.SpawnTarget(tst);
                count = 0;
            }
            else
            {
                if (gl.GetCurrentStage() == GameLogic.GameStage.Numbers)
                    AssignRandNum(tst);
                else if (gl.GetCurrentStage() == GameLogic.GameStage.Alphabet)
                    AssignRandAlph(tst);
            }

            if (hs.Contains(rnd))
            {
                isDuplicate = true;
            }    
            while (isDuplicate)
            {
                rnd = Random.Range(left+margin, right-margin);
                if (!hs.Contains(rnd))
                {
                    isDuplicate = false;
                    break;
                }
            }

            hs.Add(rnd);

            float spawnY = bubble.transform.position.y;
            if(settings.bubbleDirection == SettingManager.BubbleDirection.Up)
                spawnY = -spawnY;

            Vector3 spawnPosition = new(rnd, spawnY, 0);
            tst.transform.position = spawnPosition;

            yield return new WaitForSeconds(waitScnds);

            
                
        }
        // Debug.Log("spawner ended");
    }

    public void StartSpawning()
    {
        count = 0;
        StartCoroutine(Waiter(obj));
    }
    public int GetCount()
    {
        return count;
    }
    void AssignRandNum(GameObject obj)
    {
        int rnd = Random.Range(1,11);
        BubbleData data = obj.GetComponent<BubbleData>();
        while (rnd.ToString()==gl.GetTarget())
            rnd = Random.Range(1,11);
        data.SetValue(rnd.ToString());
    }
    void AssignRandAlph(GameObject obj)
    {
        int rnd = Random.Range(0,26);
        BubbleData data = obj.GetComponent<BubbleData>();;
        while (alphabet[rnd].ToString()==gl.GetTarget())
            rnd = Random.Range(0,26);
        data.SetValue(alphabet[rnd].ToString());
    }
}
