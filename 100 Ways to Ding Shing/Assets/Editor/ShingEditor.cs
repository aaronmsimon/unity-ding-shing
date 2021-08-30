using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ShingController))]
public class ShingEditor : Editor
{
    private SerializedProperty speed;
    private SerializedProperty groundLayer;
    private SerializedProperty fallDistThreshold;

    private SerializedProperty includePOI;
    private SerializedProperty pointOfInterest;
    private SerializedProperty returnPoint;


    private void OnEnable()
    {
        speed = serializedObject.FindProperty("speed");
        groundLayer = serializedObject.FindProperty("groundLayer");
        fallDistThreshold = serializedObject.FindProperty("fallDistThreshold");

        includePOI = serializedObject.FindProperty("includePOI");
        pointOfInterest = serializedObject.FindProperty("pointOfInterest");
        returnPoint = serializedObject.FindProperty("returnPoint");

    }

    public override void OnInspectorGUI()
    {
        //serializedObject.Update();
        EditorGUILayout.PropertyField(speed);
        EditorGUILayout.PropertyField(groundLayer);
        EditorGUILayout.PropertyField(fallDistThreshold);

        EditorGUILayout.PropertyField(includePOI);
        if (includePOI.boolValue)
        {
            EditorGUILayout.PropertyField(pointOfInterest);
            EditorGUILayout.PropertyField(returnPoint);
        }
        serializedObject.ApplyModifiedProperties();
    }
}
