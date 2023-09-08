using System.Collections.Generic;
using GameCore.Utility.Jsons;
using GameEvent;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    [ExecuteAlways]
    public class EventEditor : EditorWindow
    {
        private EditorEventsData _eventsData;
        
        private bool loanType;
        private string eventName, resolutionName, resolutionText;
        private string eventText, eventRequirements;
        private int eventDuration, eventId;
        private List<int> eventRequirementsList = new();
        
        //loan
        private int loanCost, chanceOfSuccess;

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
        }

        private void OnGUI()
        {
            EditorStyles.textField.wordWrap = true;
            loanType = EditorGUILayout.Toggle("Loan", loanType);
            if (loanType)
            {
                loanCost = EditorGUILayout.IntField("Loan cost", loanCost);
                chanceOfSuccess = EditorGUILayout.IntField("Success chance", chanceOfSuccess);
            }
            eventId = EditorGUILayout.IntField("ID", eventId);
            
            //separate the ids with a comma ,
            eventRequirements = EditorGUILayout.TextField("Requirements", eventRequirements);
            eventName = EditorGUILayout.TextField("Title", eventName);
            eventText = EditorGUILayout.TextArea(eventText, GUILayout.Height(40));
            resolutionName = EditorGUILayout.TextField("Resolution Name", resolutionName);
            resolutionText = EditorGUILayout.TextArea(resolutionText, GUILayout.Height(40));
            eventDuration = EditorGUILayout.IntField("Duration", eventDuration);
            
            //save btn
            if (GUILayout.Button("Save"))
                Save();
            
            //load btn
            if (GUILayout.Button("Load"))
                Load();
            
            //clear btn
            if (GUILayout.Button("Clear"))
                Clear();
        }

        private void Clear()
        {
            eventName = "";
            eventText = "";
            eventDuration = 0;
            eventRequirements = "";
            eventId = 0;
            loanCost = 0;
            chanceOfSuccess = 0;
        }

        private void Save()
        {
            if (eventId == 0)
            {
                Debug.LogError("id cannot be 0!");
                return;
            }
            
            ParseCommaSeperatedRequirements(eventRequirements);
            var type = loanType ? 1 : 2;
            switch (type)
            {
                case 1:
                    var loanEv = new LoanEventDataSerialized(eventId, eventDuration, type, eventRequirementsList, eventName, eventText, resolutionName, resolutionText, loanCost, chanceOfSuccess);
                    _eventsData.SaveEvent(loanEv);
                    break;
                default:
                    var regEv = new EventDataSerialized(eventId, eventDuration, type, eventRequirementsList, eventName, eventText, resolutionName, resolutionText);
                    _eventsData.SaveEvent(regEv);
                    break;
            }
        }

        private void Load()
        {
            EventDataSerialized eventData = null;
            if (eventId == 0 && eventName == string.Empty)
            {
                Debug.Log("ID or even title needed to load");
                return;
            }
            if (eventId != 0)
            {
                eventData = _eventsData.LoadSpecificEvent(eventId);
                
            }
            else if (eventName != string.Empty)
            {
                eventData = _eventsData.LoadSpecificEvent(eventName);
                
            }
            
            if (eventData == null)
            {
                Debug.Log("no event with that ID or name");
                return;
            }
            
            if (eventData.type == (int)GameEventType.Loan)
            { 
                var loan = (LoanEventDataSerialized)eventData;
                loanType = true;
                loanCost = loan.loanCost;
                chanceOfSuccess = loan.chanceOfSuccess;
            }
            
            SetLoadedParameters(eventData);
        }

        private void SetLoadedParameters(EventDataSerialized data)
        {
            eventName = data.name;
            eventDuration = data.eventDuration;
            eventText = data.text;
            eventId = data.id;
            eventRequirements = ParseIntArrayIntoString(data.eventRequirements);
        }

        private void ParseCommaSeperatedRequirements(string str)
        {
            if (string.IsNullOrEmpty(str)) return;
            
            eventRequirementsList.Clear();
            int i;
            var strArr = str.Split(',');
            foreach (var s in strArr)
                if (int.TryParse(s, out i))
                    eventRequirementsList.Add(i);
        }

        private string ParseIntArrayIntoString(int[] arr)
        {
            if (arr == null || arr.Length == 0) return "";

            return string.Join(",", arr);
        }
    }
}