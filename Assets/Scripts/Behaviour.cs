using UnityEngine;
using UnityEngine.InputSystem;
using EnhancedTouch = UnityEngine.InputSystem.EnhancedTouch;

public class Behaviour : MonoBehaviour
{
    public Camera cam;
    public GameObject obj;
    public int count = 10;
    Vector2 v2 = new(10f, 10f);

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
        if (Physics2D.OverlapPoint(WorldPoint))
        {
            Debug.Log("finger");
        }
    }

    void Update()
    {
        Vector2 MousePosition = Mouse.current.position.ReadValue();
        Vector3 WorldPoint = cam.ScreenToWorldPoint(MousePosition);
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            if (Physics2D.OverlapPoint(WorldPoint))
            {
                Debug.Log("cursor");
            }
            
        }
    }
    void CreateObj()
    {
        // for(int i=1; i<=10; i++)
        // {
        //     Instantiate(obj,)
        // }
    }
}
