using System;
using System.Linq;
using CodeBase.Logic;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace CodeBase.Editor
{
    [CustomEditor(typeof(UniqueId))]
    public class UniqueIdEditor : UnityEditor.Editor
    {
        private void OnEnable()
        {
            var unique = (UniqueId) target;

            if (string.IsNullOrWhiteSpace(unique.Id))
                Generate(unique);
            else
            {
                UniqueId[] uniqueIds = FindObjectsByType<UniqueId>(FindObjectsSortMode.None);

                if (uniqueIds.Any(other => other != unique && other.Id == unique.Id)) 
                    Generate(unique);
            }
        }

        private void Generate(UniqueId unique)
        {
            unique.SetId($"{unique.gameObject.scene.name}_{Guid.NewGuid().ToString()}");

            if (Application.isPlaying == false)
            {
                EditorUtility.SetDirty(unique);
                EditorSceneManager.MarkSceneDirty(unique.gameObject.scene);
            }
            
        }
    }
}