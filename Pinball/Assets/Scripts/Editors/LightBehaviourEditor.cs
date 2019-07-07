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
    private MethodInfo[] methods;
    private int index = 0;

    private LightBehaviour lightBehaviour;
    private SerializedProperty property;

    void OnEnable()
    {
        lightBehaviour = (LightBehaviour) target;
        property = serializedObject.FindProperty("objectsToLight");
    }


    public override void OnInspectorGUI()
    {
        lightBehaviour.onCollision =  EditorGUILayout.Toggle("Light on Collision?", lightBehaviour.onCollision);
        lightBehaviour.lightSelf = EditorGUILayout.Toggle("Light Self?", lightBehaviour.lightSelf);
        lightBehaviour.turnOffOnExit = EditorGUILayout.Toggle("Turn off on exit?", lightBehaviour.turnOffOnExit);

        Object firstObject = null;

        if(property.arraySize > 0)
        {
            firstObject = property.GetArrayElementAtIndex(0).objectReferenceValue;
        }

        if(lightBehaviour.lightSelf)
        {
            if(firstObject == null || firstObject.ToString() != lightBehaviour.gameObject.ToString())
            {
                property.arraySize++;
                property.GetArrayElementAtIndex(0).objectReferenceValue = lightBehaviour.gameObject;
            }
        }
        else if(firstObject != null && firstObject.ToString() == lightBehaviour.gameObject.ToString())
        {
            property.DeleteArrayElementAtIndex(0);
            property.arraySize--;
        }

        
        lightBehaviour.onTrigger = EditorGUILayout.Toggle("Light on Trigger?", lightBehaviour.onTrigger);
        
        lightBehaviour.onCustom = EditorGUILayout.Toggle("Light on Custom?", lightBehaviour.onCustom);

        if(lightBehaviour.onCustom)
        {
            methods = 
                typeof(Conditions)
                .GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static)
                .Where(x => x.DeclaringType == typeof(Conditions))
                .ToArray();

            
            string[] names = methods.Select(x => x.Name).ToArray();
            int methodIndex = EditorGUILayout.Popup(index, names);
            lightBehaviour.turnOnCondition = 
                    (Conditions.Condition) methods[methodIndex].CreateDelegate(typeof(Conditions.Condition));
        }

        Debug.Log($"Size: {property.arraySize}");

        EditorGUILayout.PropertyField(property, true);
        serializedObject.Update();
        serializedObject.ApplyModifiedProperties();
    }
}
#endif