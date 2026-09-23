using UnityEngine;
using UnityEngine.InputSystem;
using EnhancedTouch = UnityEngine.InputSystem.EnhancedTouch;

public class InputManagement : MonoBehaviour
{
    public Camera cam;
    public GameLogic gl;
    
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
        if (hit != null)
        {
            // Debug.Log("finger");
            gl.CheckScore(hit.gameObject);
            Destroy(hit.gameObject);
        }
    }
    void Update()
    {
        Vector2 MousePosition = Mouse.current.position.ReadValue();
        Vector3 WorldPoint = cam.ScreenToWorldPoint(MousePosition);
        Collider2D hit = Physics2D.OverlapPoint(WorldPoint);
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            if (hit != null)
            {
                // Debug.Log("cursor");
                gl.CheckScore(hit.gameObject);
                Destroy(hit.gameObject);
            }
            
        }
    }
}
