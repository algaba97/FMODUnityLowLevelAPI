using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ScenePreset))]
public class ScenePresetEditor : Editor
{
    public UnityEngine.Object source;
    UnityEngine.Object[] temp;
    UnityEngine.Object currentObject;
    UnityEngine.Object selectedObject = null;

    public override void OnInspectorGUI()
    {

        ScenePreset scenePreset = (ScenePreset)target;


        if (Event.current.type == EventType.Layout)
        {

        }

        for (int i = 0; i < scenePreset.Scenes.Count; i++)
        {
            GUILayout.BeginHorizontal();
            scenePreset.Scenes[i] = GUILayout.TextField(scenePreset.Scenes[i]);

            if (GUILayout.Button("-", GUILayout.Width(50f)))
            {
                scenePreset.Scenes.RemoveAt(i);

            }
            GUILayout.EndHorizontal();
        }

        if (GUILayout.Button("AddScene"))
        {
            scenePreset.Scenes.Add(string.Empty);

        }

        EditorUtility.SetDirty(scenePreset);
    }
}
