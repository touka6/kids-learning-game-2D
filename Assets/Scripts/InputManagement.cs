using UnityEngine;
using UnityEngine.InputSystem;
using EnhancedTouch = UnityEngine.InputSystem.EnhancedTouch;

public class InputManagement : MonoBehaviour
{
    public Camera cam;
    public GameLogic gl;
    public GameEffects ge;
    
    private void OnEnable()
    {
        EnhancedTouch.EnhancedTouchSupport.Enable();
        EnhancedTouch.TouchSimulation.Enable();
        EnhancedTouch.Touch.onFingerDown += FingerDown;
    }
    private void OnDisable()
    {
        EnhancedTouch.Touch.onFingerDown -= FingerDown;
        EnhancedTouch.TouchSimulation.Disable();
        EnhancedTouch.EnhancedTouchSupport.Disable();
    }
    private void FingerDown(EnhancedTouch.Finger finger)
    {
        // Debug.Log(
        // "Finger: " + finger.index +
        // " Position: " + finger.currentTouch.screenPosition +
        // " | Active touches: " + EnhancedTouch.Touch.activeTouches.Count
        // );
        Vector2 FingerPosition = finger.currentTouch.screenPosition;
        Vector3 WorldPoint = cam.ScreenToWorldPoint(FingerPosition);
        Collider2D hit = Physics2D.OverlapPoint(WorldPoint);
        if (hit == null)
        {
            ge.PlayMissSound();
            ge.ShowMissText(FingerPosition);
        }
        else
        {
            // Debug.Log("finger");
            HitBubble(hit, gl.CheckScore(hit.gameObject), FingerPosition);
        }
    }
    void Update()
    {
        Vector2 MousePosition = Mouse.current.position.ReadValue();
        Vector3 WorldPoint = cam.ScreenToWorldPoint(MousePosition);
        Collider2D hit = Physics2D.OverlapPoint(WorldPoint);
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            if (hit == null)
            {
                ge.PlayMissSound();
                ge.ShowMissText(MousePosition);
            }
            else
            {
                // Debug.Log("cursor");
                HitBubble(hit, gl.CheckScore(hit.gameObject), MousePosition);
            }
            
        }
    }
    void HitBubble(Collider2D hit, bool check, Vector2 pos)
    {
        ge.BubblePopParticle(hit.transform.position);

        if (check == true)
        {
            ge.PlayHitSound();
            ge.ShowHitText(pos);
        }
        else
        {
            ge.PlayFailSound();
            ge.ShowFailText(pos);
        }
            
        
        Destroy(hit.gameObject);
    }
}
