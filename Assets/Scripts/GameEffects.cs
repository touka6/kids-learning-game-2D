using TMPro;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UIElements;
using System.Collections;

public class GameEffects : MonoBehaviour
{

    [Header("AudioClip")]
    public AudioClip hitSound;
    public AudioClip failSound;
    public AudioClip missSound;

    [Header("AudioSource")]
    public AudioSource SFX_audioSource;
    public AudioSource Voice_audioSource;

    [Header("Others")]
    public ParticleSystem POPps;
    // public ParticleSystem TRAILps;
    public TMP_Text txt;
    public CanvasGroup cg;
    public GameObject FeedBackTxt;
    public Transform canvas;
    public TMP_Text countdown;



    Coroutine voiceCoroutine;


    public void BubblePopParticle(Vector3 pos)
    {
        ParticleSystem effect = Instantiate(POPps, pos, Quaternion.identity);
        Destroy(effect.gameObject, 1f);
    }
    public void BubbleTrailEffect(Vector3 pos)
    {
        // ParticleSystem effect = Instantiate(TRAILps, pos, Quaternion.identity);
        // Destroy(effect.gameObject, 1f);
    }


//#################################
//           PLAY SFX
    public void PlayHitSound()
    {
        SFX_audioSource.PlayOneShot(hitSound);
    }
    public void PlayFailSound()
    {
        SFX_audioSource.PlayOneShot(failSound);
    }
    public void PlayMissSound()
    {
        SFX_audioSource.PlayOneShot(missSound);
    }


//#################################
//      PLAY TARGET VOICE
    public void PlayNum(string num, float scnds)
    {
        AudioClip ac = Resources.Load<AudioClip>("Voices/Numbers/" + num);
        if (ac != null)
        {
            // Voice_audioSource.PlayOneShot(ac);

            if (voiceCoroutine!=null)
                StopCoroutine(voiceCoroutine);

            voiceCoroutine = StartCoroutine(Waiter(ac, scnds));

            // Debug.Log("successfully found the audio");
        }
        else
        {
            Debug.Log("failed to find the audio");
            Debug.Log(num);
        }

    }
    public void PlayAlph(string letter, float scnds)
    {
        AudioClip ac = Resources.Load<AudioClip>("Voices/Alphabet/" + letter);
        
        if (ac != null)
        {
            // Voice_audioSource.PlayOneShot(ac);

            if (voiceCoroutine!=null)
                StopCoroutine(voiceCoroutine);

            voiceCoroutine = StartCoroutine(Waiter(ac, scnds));

            // Debug.Log("successfully found the audio");
        }
        else
        {
            Debug.Log("failed to find the audio");
            Debug.Log(letter);
        }
    }
    IEnumerator Waiter(AudioClip ac, float scnds)
    {
        yield return new WaitForSeconds(0.5f);
        while (true)
        {
            Voice_audioSource.PlayOneShot(ac);
            yield return new WaitForSeconds(scnds);
        }
    }


//#################################
//      SHOW TEXT (FEEDBACK)
    public void ShowText(Vector3 pos, string msg, Color32 color)
    {
        GameObject obj = Instantiate(FeedBackTxt, canvas);

        TMP_Text newTxt = obj.GetComponent<TMP_Text>();
        CanvasGroup newCg = obj.GetComponent<CanvasGroup>();

        newTxt.text = msg;
        newTxt.color = color;

        obj.transform.position = pos;

        newCg.alpha = 1f;
        newCg.DOFade(0f,0.3f).SetDelay(0.2f).OnComplete(() => Destroy(obj)) ;
    }
    public void ShowHitText(Vector3 pos)
    {
        ShowText(pos, "Great!", new Color(219, 241, 255));
    }
    public void ShowFailText(Vector3 pos)
    {
        ShowText(pos, "Oops!", new Color(253, 255, 219));
    }
    public void ShowMissText(Vector3 pos)
    {
        ShowText(pos, "Try Again!", new Color(255, 219, 225));
    }
}
