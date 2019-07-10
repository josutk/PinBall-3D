#if UNITY_EDITOR
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;
#endif

using UnityEngine;
using static Conditions;
using static LightBehaviour;
using Object = UnityEngine.Object;


#if UNITY_EDITOR
[CustomEditor(typeof(LightBehaviour))]
public class LightBehaviourEditor : Editor
{
    private string[] turnOnMethods;
    private int turnOnMethodsIndex = 0;
    private string[] turnOffMethods;
    private int turnOffMethodsIndex = 0;
    private LightBehaviour lightBehaviour;
    private SerializedProperty objectsToLight;
    private SerializedProperty onCollision;
    private SerializedProperty lightSelf;
    private SerializedProperty turnOffOnExit;
    private SerializedProperty onTrigger;
    private SerializedProperty onCustom;
    private SerializedProperty turnOnMethodName;
    private SerializedProperty turnOffMethodName;

    void OnEnable()
    {
        lightBehaviour = (LightBehaviour) target;
        objectsToLight = serializedObject.FindProperty("objectsToLight");
        onCollision = serializedObject.FindProperty("onCollision");
        lightSelf = serializedObject.FindProperty("lightSelf");
        turnOffOnExit = serializedObject.FindProperty("turnOffOnExit");
        onTrigger = serializedObject.FindProperty("onTrigger");
        onCustom = serializedObject.FindProperty("onCustom");
        turnOnMethodName = serializedObject.FindProperty("turnOnMethodName");
        turnOffMethodName = serializedObject.FindProperty("turnOffMethodName");
        
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        onCollision.boolValue = EditorGUILayout.Toggle("Light on Collision", onCollision.boolValue);
        lightSelf.boolValue = EditorGUILayout.Toggle("Light self", lightSelf.boolValue);
        turnOffOnExit.boolValue = EditorGUILayout.Toggle("Turn off on exit", turnOffOnExit.boolValue);

        ShowLightSelfInList();

        onTrigger.boolValue = EditorGUILayout.Toggle("Light on Trigger", onTrigger.boolValue);

        onCustom.boolValue = EditorGUILayout.Toggle("Light on Custom", onCustom.boolValue);

        OnCustom();

        EditorGUILayout.PropertyField(objectsToLight, true);
        serializedObject.ApplyModifiedProperties();
    }

    private void OnCustom()
    {
        if (onCustom.boolValue)
        {
            turnOnMethods = typeof(Conditions.LightOnConditions)
                .GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static)
                .Where(x => x.DeclaringType == typeof(Conditions.LightOnConditions))
                .Select(x => x.Name)
                .ToArray();

            turnOnMethodsIndex = 
                EditorGUILayout
                .Popup(Array.IndexOf(turnOnMethods, turnOnMethodName.stringValue), turnOnMethods);

            if(turnOnMethodsIndex == -1 && turnOnMethods.Length > 0 )
            {
                turnOnMethodName.stringValue = turnOnMethods[0];
            }
            else
            {
                turnOnMethodName.stringValue = turnOnMethods[turnOnMethodsIndex];
            }

            turnOffMethods = typeof(Conditions.LightOffConditions)
                .GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static)
                .Where(x => x.DeclaringType == typeof(Conditions.LightOffConditions))
                .Select(x => x.Name)
                .ToArray();

            turnOffMethodsIndex = 
                EditorGUILayout
                .Popup(Array.IndexOf(turnOffMethods, turnOffMethodName.stringValue), turnOffMethods);

            if(turnOffMethodsIndex == -1 && turnOffMethods.Length > 0 )
            {
                turnOffMethodName.stringValue = turnOffMethods[0];
            }
            else
            {
                turnOffMethodName.stringValue = turnOffMethods[turnOffMethodsIndex];
            }

            serializedObject.ApplyModifiedProperties();
        }
    }

    private void ShowLightSelfInList()
    {
        Object firstObject = null;

        if (objectsToLight.arraySize > 0)
        {
            firstObject = objectsToLight.GetArrayElementAtIndex(0).objectReferenceValue;
        }

        if (lightSelf.boolValue)
        {
            if (firstObject == null || firstObject.ToString() != lightBehaviour.gameObject.ToString())
            {
                objectsToLight.arraySize++;
                objectsToLight.GetArrayElementAtIndex(0).objectReferenceValue = lightBehaviour.gameObject;
            }
        }
        else if (firstObject != null && firstObject.ToString() == lightBehaviour.gameObject.ToString())
        {
            objectsToLight.DeleteArrayElementAtIndex(0);
            objectsToLight.arraySize--;
        }

        serializedObject.ApplyModifiedProperties();
    }
}
#endif