using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveUpBehaviour : MonoBehaviour
{
    private bool move;
    private float journeyLength;
    private float startTime;
    private Vector3 start;
    
    public Vector3 aboveTablePosition;
    public float upSpeed;

    // Object that will be moved. If null, will try to move self.
    public GameObject objectToMove;

    // Check if you want to use the position relative to the parent. If false,
    // uses the position in relation to the world. Defaults to false.
    public bool useLocal = false;

    void Start()
    {
        move = false;

        if(objectToMove == null) objectToMove = transform.gameObject;

        if(useLocal == true) start = objectToMove.transform.localPosition;
        else start = objectToMove.transform.position;
    }
    
    void Update()
    {
        if(move)
        {
            Move(start, aboveTablePosition);
        }
    }

    void OnTriggerExit(Collider collider)
    {
        if(CollisionHelper.DidCollideWithSphere(collider.gameObject.tag))
        {
            move = true;
            startTime = Time.time;
            journeyLength = Vector3.Distance(start, aboveTablePosition);
        }
    }

    private void Move(Vector3 start, Vector3 end)
    {
        float distCovered = (Time.time - startTime) * upSpeed;

        float fracJourney = distCovered / journeyLength;

        // Set our position as a fraction of the distance between the markers.
        if(useLocal == true) objectToMove.transform.localPosition = Vector3.Lerp(start, end, fracJourney);
        else objectToMove.transform.position = Vector3.Lerp(start, end, fracJourney);

        if(fracJourney >= 1) // Ended
        {
            move = false;
            GetComponent<Collider>().isTrigger = false;
        }
    }
}
