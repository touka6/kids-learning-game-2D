using System;
using UnityEngine;

public class BubbleMovement : MonoBehaviour
{
    public enum Direction
    {
        Ascending,
        Descending,
    }
    float speed = 2.5f;
    Vector3 y = new(0f,1f,0f);
    Direction currentDirection = Direction.Descending;

    public void SetDirection(Direction direction)
    {
        currentDirection = direction;

    }

    public void SetSpeed(float newSpeed)
    {
        speed = newSpeed;
    }
    
    void Update()
    {
        Descending();
        // Ascending();
        // if(currentDirection == Direction.Descending)
    }
    public void Descending()
    {   
        transform.position += speed * Time.deltaTime * Vector3.down;
    }
    public void Ascending()
    {
        transform.position += speed * Time.deltaTime * Vector3.up;
    }
}
