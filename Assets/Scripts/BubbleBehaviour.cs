using System;
using UnityEngine;
using UnityEngine.InputSystem;
using EnhancedTouch = UnityEngine.InputSystem.EnhancedTouch;

public class BubbleBehaviour : MonoBehaviour
{
    
    public GameObject obj;
    public GameObject gnd;
    
    void Update()
    {
        
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(obj);
        // Debug.Log("hit");
    }

}
