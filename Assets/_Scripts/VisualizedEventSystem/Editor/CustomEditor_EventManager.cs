using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

#if UNITY_EDITOR
using UnityEditor;
#endif

[CustomEditor(typeof(EventManager))]
public class CustomEditor_EventManager : Editor
{
    EventManager thisTarget;

    //having the list static will make it remember if it was true/false when switching objects in the editor
    static List<bool> foldOuts = new List<bool>();

    public override void OnInspectorGUI()
    {
        thisTarget = (EventManager)target;

        if (thisTarget.SubscribedObjects != null)
        {
            //create all the foldout variables
            if (foldOuts.Count < thisTarget.SubscribedObjects.Count)
            {
                //calculate the differnece between the number of keys and the number of folouts then compensate
                int diff = thisTarget.SubscribedObjects.Count - foldOuts.Count;
                for (int i = 0; i < diff; i++)
                {
                    foldOuts.Add(false);
                }
            }

            //go through all the keys
            for (int u = 0; u < thisTarget.SubscribedObjects.Keys.Count; u++)
            {
                string key = thisTarget.SubscribedObjects.Keys.ToList()[u];

                //make foldout groups for each key
                foldOuts[u] = EditorGUILayout.Foldout(foldOuts[u], key);

                //display all the subscribers to each key
                if (foldOuts[u])
                    for (int i = 0; i < thisTarget.SubscribedObjects[key].Count; i++)
                    {
                        EditorGUILayout.LabelField("" + thisTarget.SubscribedObjects[key][i]);
                    }
            }
        }
    }
}
