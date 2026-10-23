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
    public AudioClip music;
    public AudioClip startCountdownSound;
    public AudioClip endCountdownSound;
    public AudioClip[] CongratsSound;

    [Header("AudioSource")]
    public AudioSource SFX_audioSource;
    public AudioSource Voice_audioSource;
    public AudioSource Music_audioSource;
    public AudioSource Countdown_audioSource;

    [Header("ParticleSystems")]
    public ParticleSystem POPps;
    public ParticleSystem[] Congratsps;
    // public ParticleSystem TRAILps;

    [Header("Others")]
    public TMP_Text txt;
    public CanvasGroup cg;
    public GameObject FeedBackTxt;
    public Transform canvas;
    public TMP_Text countdown;



    Coroutine voiceCoroutine;


//#################################
//       PLAY ParticleEffect
    public void BubblePopParticle(Vector3 pos)
    {
        ParticleSystem effect = Instantiate(POPps, pos, Quaternion.identity);
        Destroy(effect.gameObject, 1f);
    }
    IEnumerator CongratsParticle()
    {
        PlayCongratsSound();
        yield return new WaitForSeconds(1f);
        foreach (ParticleSystem ps in Congratsps)
        {
            Vector3 pos = new(Random.Range(-6f,6f), Random.Range(-3f,3f), 0f);

            ParticleSystem effect = Instantiate(ps, pos, Quaternion.identity);
            
            PlayFireworkSound();

            yield return new WaitForSeconds(1f);

            Destroy(effect, 20f);
        }
    }
    public void StartCongratsParticle()
    {
        StartCoroutine(CongratsParticle());
    }


//#################################
//           PLAY SFX
    public void PlayHitSound()
    {
        SFX_audioSource.volume = 0.7f;
        SFX_audioSource.PlayOneShot(hitSound);
        
    }
    public void PlayFailSound()
    {
        SFX_audioSource.volume = 1.0f;
        SFX_audioSource.PlayOneShot(failSound);
    }
    public void PlayMissSound()
    {
        SFX_audioSource.volume = 1.0f;
        SFX_audioSource.PlayOneShot(missSound);
    }
    public void PlayStartCountDown()
    {
        Countdown_audioSource.PlayOneShot(startCountdownSound);
    }
    public void PlayEndCountDown()
    {
        Countdown_audioSource.PlayOneShot(endCountdownSound);
    }
    public void PlayFireworkSound()
    {
        SFX_audioSource.PlayOneShot(CongratsSound[1]);
    }
    public void PlayCongratsSound()
    {
        SFX_audioSource.PlayOneShot(CongratsSound[0]);
    }

//#################################
//      PLAY TARGET VOICE
    public void PlayNum(string num, float scnds)
    {
        AudioClip ac = Resources.Load<AudioClip>("Voices/Numbers/" + num);
        if (ac != null)
        {
            // if (voiceCoroutine!=null)
            //     StopCoroutine(voiceCoroutine);
            StopPlayingTarget();
            voiceCoroutine = StartCoroutine(Waiter(ac, scnds));

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
            if (voiceCoroutine!=null)
                StopCoroutine(voiceCoroutine);

            voiceCoroutine = StartCoroutine(Waiter(ac, scnds));
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
    public void StopPlayingTarget()
    {
        if(voiceCoroutine != null)
        {
            StopCoroutine(voiceCoroutine);
            voiceCoroutine = null;
        }
        Voice_audioSource.Stop();
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

//#################################
//            OTHERS
    IEnumerator WaitFor(float time)
    {
        yield return new WaitForSeconds(time);
    }
}
