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
        [Tooltip("The name of the created Item Types class.")] public string fileItemTypesName = "ItemTypes";
        [Tooltip("The name of the created Equipment Types class.")] public string fileEquipmentTypesName = "EquipmentTypes";

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

            fileItemTypesName = EditorGUILayout.TextField("Name", fileItemTypesName);
            fileEquipmentTypesName = EditorGUILayout.TextField("Name", fileEquipmentTypesName);

            EditorGUILayout.Space();

            if (GUILayout.Button("Save Item Types"))
            {
                EditorMethods.WriteToEnum(filePath, fileItemTypesName, inventorySystem.itemTypes);
            }

            if (GUILayout.Button("Save Equipment Types"))
            {
                EditorMethods.WriteToEnum(filePath, fileEquipmentTypesName, inventorySystem.equipmentTypes);
            }
            EditorGUILayout.EndVertical();
        }
    }
}
