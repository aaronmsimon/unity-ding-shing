using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ShingController))]
public class ShingEditor : Editor
{
    private SerializedProperty speed;
    private SerializedProperty groundLayer;

    private SerializedProperty includePointOfInterest;
    private SerializedProperty pointOfInterest;
    private SerializedProperty returnPoint;


    private void OnEnable()
    {
        speed = serializedObject.FindProperty("speed");
        groundLayer = serializedObject.FindProperty("groundLayer");

        includePointOfInterest = serializedObject.FindProperty("includePointOfInterest");
        pointOfInterest = serializedObject.FindProperty("pointOfInterest");
        returnPoint = serializedObject.FindProperty("returnPoint");

    }

    public override void OnInspectorGUI()
    {
        //serializedObject.Update();
        EditorGUILayout.PropertyField(speed);
        EditorGUILayout.PropertyField(groundLayer);

        EditorGUILayout.PropertyField(includePointOfInterest);
        if (includePointOfInterest.boolValue)
        {
            EditorGUILayout.PropertyField(pointOfInterest);
            EditorGUILayout.PropertyField(returnPoint);
        }
        serializedObject.ApplyModifiedProperties();
    }
}
