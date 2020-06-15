using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ItemTypeEditor : MonoBehaviour
{
    [CustomEditor(typeof(InventorySystem))]
    public class TestWriter : Editor
    {
        InventorySystem inventorySystem;
        public string filePath = "Assets/";
        [Tooltip("The name of the created class.")] public string fileName = "ItemTypes";

        private void OnEnable()
        {
            inventorySystem = (InventorySystem)target;
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            EditorGUILayout.BeginVertical("box");
            GUILayout.Label("'Save your created item types upon editing the ItemTypes list in the Inventory System'");
            EditorGUILayout.Space();
            filePath = EditorGUILayout.TextField("Path", filePath);
            fileName = EditorGUILayout.TextField("Name", fileName);
            EditorGUILayout.Space();
            if (GUILayout.Button("Save"))
            {
                EditorMethods.WriteToEnum(filePath, fileName, inventorySystem.itemTypes);
            }
            EditorGUILayout.EndVertical();
        }
    }
}
