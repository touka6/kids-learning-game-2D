using UnityEngine;

public class ResizeToCamera : MonoBehaviour
{
    Camera cam;

    void Awake()
    {
        cam = Camera.main;
    }

    void Update()
    {
        float width = cam.orthographicSize * 2f * cam.aspect;

        transform.localScale = new Vector3(
            width,
            transform.localScale.y,
            transform.localScale.z
        );
    }
}
