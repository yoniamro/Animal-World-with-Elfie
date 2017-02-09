using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(AddComponentsBot))]
public class AddComponentHandler : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        AddComponentsBot bot = target as AddComponentsBot;

        if (GUILayout.Button("Add Components!"))
        {
            bot.AddDesiredComponents();
        }

    }
}
