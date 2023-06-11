using System.Collections.Generic;
using GameCore.Utility.Jsons;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    [ExecuteAlways]
    public class EventEditor : EditorWindow
    {
        private bool loanType;
        private string titleString = "Hello World";
        private string areaText;
        private int eventDuration, eventId;
        
        private List<int> eventRequirements;

        [MenuItem("Window/Event Editor")]
        static void Init()
        {
            // Get existing open window or if none, make a new one:
            EventEditor window = (EventEditor)GetWindow(typeof(EventEditor));
            window.Show();
        }

        private void OnGUI()
        {
            loanType = EditorGUILayout.Toggle("Loan", loanType);
            EditorStyles.textField.wordWrap = true;
            eventId = EditorGUILayout.IntField("ID", eventId);
            
            titleString = EditorGUILayout.TextField("Title", titleString);
            areaText = EditorGUILayout.TextArea(areaText, GUILayout.Height(50));
            eventDuration = EditorGUILayout.IntField("Duration", eventDuration);
            if (GUILayout.Button("Save"))
                Save();
        }

        private void Save()
        {
            //var data = new EventDataSerialized(eventId, eventDuration, loanType, )
        }
    }
}