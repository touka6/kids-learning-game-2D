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
        EnhancedTouch.EnhancedTouchSupport.Disable();
        EnhancedTouch.TouchSimulation.Disable();
    }
    private void FingerDown(EnhancedTouch.Finger finger)
    {
        Vector2 FingerPosition = finger.currentTouch.screenPosition;
        Vector3 WorldPoint = cam.ScreenToWorldPoint(FingerPosition);
        Collider2D hit = Physics2D.OverlapPoint(WorldPoint);
        if (Physics2D.OverlapPoint(WorldPoint))
        {
            // Debug.Log("finger");
            Destroy(hit.gameObject);
            gl.IncScore();
        }
    }
    void Update()
    {
        Vector2 MousePosition = Mouse.current.position.ReadValue();
        Vector3 WorldPoint = cam.ScreenToWorldPoint(MousePosition);
        Collider2D hit = Physics2D.OverlapPoint(WorldPoint);
        // hit.tag == ;
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            if (Physics2D.OverlapPoint(WorldPoint))
            {
                // Debug.Log("cursor");
                Destroy(hit.gameObject);
                gl.IncScore();
            }
            
        }
    }
}
