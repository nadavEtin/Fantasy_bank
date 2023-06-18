using System.Collections.Generic;
using GameCore.ScriptableObjects;
using GameCore.Utility.Jsons;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using VContainer;

namespace Editor
{
    [ExecuteAlways]
    public class EventEditor : EditorWindow
    {
        private EditorEventsData _eventsData;
        
        private bool loanType;
        private string titleString;
        private string eventText, eventRequirements;
        private int eventDuration, eventId;
        private List<int> eventRequirementsList = new();
        
        //loan
        private int loanCost, chanceOfSuccess;

        public EventEditor()
        {
            //var lifetime = GameObject.FindObjectOfType(typeof(GameLifetimeScope));
            //var res = Resources.Load<AssetRefs>("AssetRefs");
            
        }

        [MenuItem("Window/Event Editor")]
        private static void Init()
        {
            // Get existing open window or if none, make a new one:
            EventEditor window = (EventEditor)GetWindow(typeof(EventEditor));
            window.Show();
        }

        private void OnEnable()
        {
            _eventsData = new EditorEventsData();
            _eventsData.Init();
        }

        private void InitTwo()
        {
            
        }

        private void OnGUI()
        {
            EditorStyles.textField.wordWrap = true;
            loanType = EditorGUILayout.Toggle("Loan", loanType);
            if (loanType)
            {
                loanCost = EditorGUILayout.IntField("Loan cost", loanCost);
                chanceOfSuccess = EditorGUILayout.IntField("Loan cost", chanceOfSuccess);
            }
            eventId = EditorGUILayout.IntField("ID", eventId);
            
            //separate the ids with a comma ,
            eventRequirements = EditorGUILayout.TextField("Requirements", eventRequirements);
            titleString = EditorGUILayout.TextField("Title", titleString);
            eventText = EditorGUILayout.TextArea(eventText, GUILayout.Height(50));
            eventDuration = EditorGUILayout.IntField("Duration", eventDuration);
            
            //save btn
            if (GUILayout.Button("Save"))
                Save();
            
            //load btn
            if (GUILayout.Button("Load"))
                Load();
        }

        private void Save()
        {
            ParseCSRequirements(eventRequirements);
            var type = loanType ? 1 : 2;
            System.Object data;
            switch (type)
            {
                case 1:
                    data = new LoanEventDataSerialized(eventId, eventDuration, type, eventRequirementsList, titleString,
                        eventText, loanCost, chanceOfSuccess);
                    break;
                default:
                    data = new EventDataSerialized(eventId, eventDuration, type, eventRequirementsList, titleString,
                        eventText);
                    break;
            }
            
        }

        private void Load()
        {
            string key;
            if (eventId != 0)
                key = eventId.ToString();
            else if (titleString != string.Empty)
                key = titleString;
            else
            {
                Debug.Log("ID or even title needed to load");
                return;
            }
            
        }

        private void ParseCSRequirements(string str)
        {
            eventRequirementsList.Clear();
            int i;
            var strArr = str.Split(',');
            foreach (var s in strArr)
                if (int.TryParse(s, out i))
                    eventRequirementsList.Add(i);
        }
    }
}