using System;
using CodeBase.Logic;
using CodeBase.Logic.EnemySpawners;
using CodeBase.StaticData;
using UnityEditor;
using UnityEngine;

namespace CodeBase.Editor
{
    [CustomEditor(typeof(SpawnMarker))]
    public class SpawnMarkerEditor : UnityEditor.Editor
    {
        [DrawGizmo(GizmoType.Active | GizmoType.Pickable | GizmoType.NonSelected)]
        public static void RenderCustomGizmo(SpawnMarker spawner, GizmoType gizmo)
        {
            Vector3 spawnPosition = spawner.transform.position;
            //Vector3 cameraForward = SceneView.currentDrawingSceneView.camera.transform.forward;
            float sphereRadius = 0.5f;
            float distanceFromCamera = sphereRadius;
            //Vector3 textPosition = spawnPosition - distanceFromCamera * cameraForward;

            Color defaultColor = Gizmos.color;
            Color color = GetColorByMonsterType(spawner.MonsterTypeId);
            color = new Color(color.r, color.g, color.b, 0.7f);
            Gizmos.color = color;
            Gizmos.DrawSphere(spawnPosition, sphereRadius);
            Gizmos.color = defaultColor;
            
            /*string text = spawner.MonsterTypeId.ToString();
            DrawString(text, spawnPosition, Color.white, Color.black);*/
        }
        private static Color GetColorByMonsterType(MonsterTypeId monsterType)
        {
            switch (monsterType)
            {
                case MonsterTypeId.None:
                    goto default;
                case MonsterTypeId.Lich:
                    return Color.red;
                case MonsterTypeId.Golem:
                    return Color.green;
                default:
                    throw new ArgumentOutOfRangeException(nameof(monsterType), monsterType, null);
            }
        }
        
        public static void DrawString(string text, Vector3 worldPos, Color? textColor = null, Color? backColor = null)
        {
            Handles.BeginGUI();
            var restoreTextColor = GUI.color;
            var restoreBackColor = GUI.backgroundColor;
 
            GUI.color = textColor ?? Color.white;
            GUI.backgroundColor = backColor ?? Color.black;
 
            var view = SceneView.currentDrawingSceneView;
            if (view != null && view.camera != null)
            {
                Vector3 screenPos = view.camera.WorldToScreenPoint(worldPos);
                if (screenPos.y < 0 || screenPos.y > Screen.height || screenPos.x < 0 || screenPos.x > Screen.width || screenPos.z < 0)
                {
                    GUI.color = restoreTextColor;
                    Handles.EndGUI();
                    return;
                }
                Vector2 size = GUI.skin.label.CalcSize(new GUIContent(text));          
                Rect rect = new Rect(screenPos.x - (size.x / 2), -screenPos.y + view.position.height + 4, size.x, size.y);
                GUI.Box(rect, text, EditorStyles.textField);
                GUI.Label(rect, text);
                GUI.color = restoreTextColor;
                GUI.backgroundColor = restoreBackColor;
            }
            Handles.EndGUI();
        }
    }
}