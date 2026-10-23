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
    public Direction currentDirection = Direction.Descending;
    // public GameObject ascendingTrail;
    // public GameObject descendingTrail;
    public ParticleSystem ascendingTrail;
    public ParticleSystem descendingTrail;

    
    // public void BubbleTrailEffect(Vector3 pos)
    // {
    //     // gameObject.GetComponentInChildren<ParticleSystem>()
    //     // transform.Find("BubbleRisingUnderwater").gameObject.SetActive(true);
    //     // gameObject.transform.Find("BubbleDescendingUnderwater").gameObject.SetActive(true);
    // }
    public void SetDirection(Direction direction)
    {
        currentDirection = direction;

        if (currentDirection == Direction.Ascending)
        {
            if(!ascendingTrail.gameObject.activeSelf) ascendingTrail.gameObject.SetActive(true);

            descendingTrail.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
            ascendingTrail.Play(true);
            descendingTrail.gameObject.SetActive(false);
            // ascendingTrail.SetActive(true);
            // descendingTrail.SetActive(false);
        }
        else
        {
          if(!descendingTrail.gameObject.activeSelf)  descendingTrail.gameObject.SetActive(true);
            ascendingTrail.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
            descendingTrail.Play(true);
            ascendingTrail.gameObject.SetActive(false);
            // ascendingTrail.SetActive(false);
            // descendingTrail.SetActive(true);
        }

    }

    public void SetSpeed(float newSpeed)
    {
        speed = newSpeed;
    }
    
    void Update()
    {
        if(currentDirection == Direction.Descending)
            Descending();
        else
            Ascending();
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
