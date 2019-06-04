using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ResetBehaviour : MonoBehaviour
{
    private bool _reset = false;

    public bool shouldReset
    {
        set
        {
            _reset = value;
        }
    }

    // Mark if should reset position.
    public bool resetPosition = false;

    // Object to reset position. Defaults to current object.
    public GameObject objectToResetPosition;

    // Mark if should reset trigger
    public bool resetTrigger = false;

    // Object with trigger. Defaults to current.
    public GameObject objectWithTrigger;

    // Will you reset the trigger to true or to false? Defaults to true;
    public bool resetTriggerTo = true;

    private Collider trigger;
    private Vector3 originalPosition;

    void Start()
    {
        if(resetPosition)
        {
            if(objectToResetPosition == null)
            {
                Debug.Log("Object to reset position is null!");
                objectToResetPosition = gameObject;
            }

            originalPosition = objectToResetPosition.transform.localPosition;
        }

        if(resetTrigger)
        {
            if(objectWithTrigger == null)
            {
                objectWithTrigger = gameObject;
            }

            trigger = objectWithTrigger.GetComponent<Collider>();        
        }

    }

    void Update()
    {
        if(_reset)
        {
            _reset = false;
            Reset();
        }
    }

    private void Reset()
    {
        Debug.Log("Resetting!");

        if(resetPosition)
        {
            Debug.Log("ResetPosition?");

            objectToResetPosition.transform.localPosition = originalPosition;
        }

        if(resetTrigger)
        {
            Debug.Log("Reset Trigger?");
            trigger.isTrigger = resetTriggerTo;
        }
    }

}

[CustomEditor(typeof(ResetBehaviour))]
public class ResetBehaviourEditor : Editor
{

    public override void OnInspectorGUI()
    {
        ResetBehaviour myResetBehaviour = (ResetBehaviour)target;

        myResetBehaviour.resetTrigger = EditorGUILayout.Toggle("Reset Trigger?", myResetBehaviour.resetTrigger);

        if(myResetBehaviour.resetTrigger)
        {
            myResetBehaviour.objectWithTrigger = (GameObject)EditorGUILayout.ObjectField("Object with trigger",
                myResetBehaviour.objectWithTrigger, typeof(GameObject), true);

            myResetBehaviour.resetTriggerTo = EditorGUILayout.Toggle("Reset Trigger To:", myResetBehaviour.resetTriggerTo);
        }

        myResetBehaviour.resetPosition = EditorGUILayout.Toggle("Reset Position?", myResetBehaviour.resetPosition);

        if(myResetBehaviour.resetPosition)
        {
            myResetBehaviour.objectToResetPosition =
                (GameObject)EditorGUILayout.ObjectField("Object to be reseted", myResetBehaviour.objectToResetPosition, typeof(GameObject), true);
        }

    }
}
