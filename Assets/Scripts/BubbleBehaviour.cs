using System;
using UnityEngine;
using UnityEngine.InputSystem;
using EnhancedTouch = UnityEngine.InputSystem.EnhancedTouch;

public class BubbleBehaviour : MonoBehaviour
{
    public GameObject obj;
    public GameObject gnd;
    public GameEffects ge;

    void OnCollisionEnter2D(Collision2D collision)
    {
        ge.BubblePopParticle(transform.position);
        Destroy(obj);
        // Debug.Log("hit");
    }

}
