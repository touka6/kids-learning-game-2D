using UnityEngine;

public class BubbleMovement : MonoBehaviour
{
    float speed = 2.5f;
    public void SetSpeed(float newSpeed)
    {
        speed = newSpeed;
    }
    void Update()
    {
        transform.position += speed * Time.deltaTime * Vector3.down;
    }
}
