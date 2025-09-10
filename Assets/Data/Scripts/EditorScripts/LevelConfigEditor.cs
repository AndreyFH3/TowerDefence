using Levels.Info;
using UnityEditor;
using UnityEngine;

namespace EditorScripts
{
    [CustomEditor(typeof (LevelInfoContainer))]
public class LevelConfigEditor : Editor
{
        private int selectedIndex = 0; // ������ ���������� ������

        public override void OnInspectorGUI()
        {
            // ������ ����������� ���� (������ ������� � �.�.)
            DrawDefaultInspector();

            LevelInfoContainer container = (LevelInfoContainer)target;

            if (container.LevelsInfo.Length == 0)
            {
                EditorGUILayout.HelpBox("��� ������� � ����������!", MessageType.Warning);
                return;
            }

            // ����� ������ ����� popup
            string[] levelNames = System.Array.ConvertAll(container.LevelsInfo, l => l.LevelId);
            selectedIndex = EditorGUILayout.Popup("��������� �������", selectedIndex, levelNames);

            LevelInfo currentLevel = container.LevelsInfo[selectedIndex];

            EditorGUILayout.Space();

            if (GUILayout.Button("������� ����� �� ����������� �������"))
            {
                if (Selection.activeGameObject != null)
                {
                    Transform parent = Selection.activeGameObject.transform;

                    Vector3[] points = new Vector3[parent.childCount];
                    for (int i = 0; i < parent.childCount; i++)
                    {
                        points[i] = parent.GetChild(i).position;
                    }

                    // ������� ����� � LevelInfo
                    currentLevel.SetPoints(points);

                    EditorUtility.SetDirty(container);
                    AssetDatabase.SaveAssets();

                    Debug.Log($"����� ��� ������ '{currentLevel.LevelId}' ���������: {points.Length} ��.");
                }
                else
                {
                    Debug.LogWarning("������ ������ � ��������� �������!");
                }
            }
            if (GUILayout.Button("���������"))
            {
                if (Selection.activeGameObject != null)
                {
                    Vector3[] array = new Vector3[0];

                    currentLevel.SetPoints(array);

                    EditorUtility.SetDirty(container);
                    AssetDatabase.SaveAssets();

                    Debug.Log($"����� ��� ������ '{currentLevel.LevelId}' �������");
                }
                else
                {
                    Debug.LogWarning("������ ������ � ��������� �������!");
                }
            }
        }
    }
}