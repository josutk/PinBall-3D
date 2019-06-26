using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandupScript : MonoBehaviour
{
    Rigidbody rb;

    // Amount of units that the object will recoil when hit.
    public const float RECOIL = 0.03F;

    // "Resistance" of the standup when hit. The actual velocity
    // Will Be proportional to the colision speed.
    public const float RECOIL_BACK_SPEED = 0.3F;

    private float startTime;
    private float recoilSpeed;
    private float journeyLength;
    private Vector3 startPosition;
    private Vector3 endPosition;

    private Status status = Status.IDLE;

    // Component hit count
    public int hitCount = 0;

    // Score given when the component is hit.
    private const int COMPONENT_SCORE = 100;

    private enum Status
    {
        HIT,
        RESTORE,
        IDLE
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();        
        startPosition = transform.position;
    }

    void Update()
    {
        if(status != Status.IDLE)
        {
            Move();
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if(CollidedWithSphere(collision.gameObject.tag))
        {
            hitCount++;
            Debug.Log($"Hit Count == {hitCount}");
            // TODO(Roger): game.score += COMPONENT_SCORE;
            
            SetupRecoilMovement(collision);
        }
    }

    private bool CollidedWithSphere(string tag)
    {
        if(tag == "Sphere")
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void SetupRecoilMovement(Collision collision)
    {
        // Set end position of interpolation based on the RECOIL variable and the hit angle
        Vector3 normal = collision.contacts[0].normal.normalized;
        endPosition = startPosition + (normal * RECOIL);
        journeyLength = Vector3.Distance(startPosition, endPosition);
        startTime = Time.time;
        recoilSpeed = RECOIL_BACK_SPEED * collision.relativeVelocity.magnitude;
        
        status = Status.HIT;
    }

    private void SetupRestoreMovement()
    {
        status = Status.RESTORE;
        startTime = Time.time;
        journeyLength = Vector3.Distance(endPosition, startPosition);
    }
    
    private void Move()
    {
        float distCovered = (Time.time - startTime) * recoilSpeed;

        float fracJourney = distCovered / journeyLength;

        if(IsStatusHit())
        {
            if(IsLerpEnded(fracJourney))
            {
                SetupRestoreMovement();
            }
            else
            {
                transform.position = Vector3.Lerp(startPosition, endPosition, fracJourney);
            }
        }
        else if(IsStatusRestore())
        {
            if(IsLerpEnded(fracJourney))
            {
                status = Status.IDLE;
            }
                transform.position = Vector3.Lerp(endPosition, startPosition, fracJourney);
        }
    }

    private bool IsStatusHit()
    {
        if(status == Status.HIT)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private bool IsStatusRestore()
    {
        if(status == Status.RESTORE)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private bool IsStatusIdle()
    {
        if(status == Status.IDLE)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private bool IsLerpEnded(float fracJourney)
    {
        if(fracJourney >= 1.0F)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
