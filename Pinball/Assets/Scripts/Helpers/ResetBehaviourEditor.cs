
#if UNITY_EDITOR
using UnityEditor;
#endif

using UnityEngine;


#if UNITY_EDITOR
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
#endif