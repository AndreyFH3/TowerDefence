using Levels.Info;
using UnityEditor;
using UnityEngine;

namespace EditorScripts
{
    [CustomEditor(typeof (LevelInfoContainer))]
public class LevelConfigEditor : Editor
{
        private int selectedIndex = 0; // индекс выбранного уровня

        public override void OnInspectorGUI()
        {
            // Рисуем стандартные поля (список уровней и т.п.)
            DrawDefaultInspector();

            LevelInfoContainer container = (LevelInfoContainer)target;

            if (container.LevelsInfo.Length == 0)
            {
                EditorGUILayout.HelpBox("Нет уровней в контейнере!", MessageType.Warning);
                return;
            }

            // Выбор уровня через popup
            string[] levelNames = System.Array.ConvertAll(container.LevelsInfo, l => l.LevelId);
            selectedIndex = EditorGUILayout.Popup("Выбранный уровень", selectedIndex, levelNames);

            LevelInfo currentLevel = container.LevelsInfo[selectedIndex];

            EditorGUILayout.Space();

            if (GUILayout.Button("Собрать точки из выделенного объекта"))
            {
                if (Selection.activeGameObject != null)
                {
                    Transform parent = Selection.activeGameObject.transform;

                    Vector3[] points = new Vector3[parent.childCount];
                    for (int i = 0; i < parent.childCount; i++)
                    {
                        points[i] = parent.GetChild(i).position;
                    }

                    // Передаём точки в LevelInfo
                    currentLevel.SetPoints(points);

                    EditorUtility.SetDirty(container);
                    AssetDatabase.SaveAssets();

                    Debug.Log($"Точки для уровня '{currentLevel.LevelId}' обновлены: {points.Length} шт.");
                }
                else
                {
                    Debug.LogWarning("Выдели объект с дочерними точками!");
                }
            }
            if (GUILayout.Button("Отчистить"))
            {
                if (Selection.activeGameObject != null)
                {
                    Vector3[] array = new Vector3[0];

                    currentLevel.SetPoints(array);

                    EditorUtility.SetDirty(container);
                    AssetDatabase.SaveAssets();

                    Debug.Log($"Точки для уровня '{currentLevel.LevelId}' удалены");
                }
                else
                {
                    Debug.LogWarning("Выдели объект с дочерними точками!");
                }
            }
        }
    }
}