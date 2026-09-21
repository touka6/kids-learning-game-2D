using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BubbleSpawner : MonoBehaviour
{
    private static WaitForSeconds _waitForSeconds1_5 = new WaitForSeconds(1.5f);
    public GameObject obj;
    GameObject tst;
    public int count = 10;
    public float waitScnds = 1.5f;
    int len = 11;
    void Start()
    {
        // CreateObj(obj);
        StartCoroutine(Waiter(obj));
    }

    IEnumerator Waiter(GameObject bubble)
    {
        HashSet<float> hs = new(len);
        bool isDuplicate = false;
        float tmp = float.PositiveInfinity;
        for(int i=1; i<=count; i++)
        {
            float rnd = Random.Range(-5,6);
            
            if(hs.Count == len)
            {
                tmp = rnd;
                hs.Clear();
            }

            tst = Instantiate(bubble);

            if (hs.Contains(rnd) || tmp==rnd)
            {
                isDuplicate = true;
            }    
            while (isDuplicate)
            {
                rnd = Random.Range(-5,6);
                if (!hs.Contains(rnd) && !(tmp==rnd))
                {
                    isDuplicate = false;
                    break;
                }
            }

            hs.Add(rnd);
            Vector3 spawnPosition = new(rnd, bubble.transform.position.y, 0);
            tst.transform.position = spawnPosition;

            yield return new WaitForSeconds(waitScnds);
        }
    }
}
