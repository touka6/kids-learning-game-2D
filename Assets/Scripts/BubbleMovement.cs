using UnityEngine;

public class BubbleMovement : MonoBehaviour
{
    public float speed = 1.5f;
    void Update()
    {
        transform.position += speed * Time.deltaTime * Vector3.down;
    }
}
