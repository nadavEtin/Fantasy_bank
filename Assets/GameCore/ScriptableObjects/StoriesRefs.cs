using GameCore.Utility.Jsons;
using GameEvent;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[CreateAssetMenu(fileName = "StoriesRefs", menuName = "Scriptable Objects/Stories References")]
public class StoriesRefs : ScriptableObject, IStoriesRefs
{
    public Dictionary<StoryType, Dictionary<string, EventDataSerialized>> AllStories { private set; get; }

    private string _SA_path;
    private string _eventsFileName = "EventsData.json";
    private string _eventsFilePath;
    //private StoriesDataContainerObj _eventsDataContainer;

    /*    private void OnEnable()
        {
            InitSetup();
        }*/

    public void InitSetup()
    {
        _SA_path = $"{Application.dataPath}/StreamingAssets";
        _eventsFilePath = $"{_SA_path}/{_eventsFileName}";
        AllStories = new Dictionary<StoryType, Dictionary<string, EventDataSerialized>>
        {
            { StoryType.Other, new Dictionary<string, EventDataSerialized>() },
            { StoryType.Loan, new Dictionary<string, EventDataSerialized>() }
        };

        LoadStoriesFromFile();        
    }

    public void SaveStory(EventDataSerialized data, StoryType type)
    {
        /*var storyList = _eventsDataContainer.regularEvents;

        switch (type)
        {
            case StoryType.Loan:
                storyList = _eventsDataContainer.loanEvents;
                break;
            default:
                break;
        }*/

        //search for this event by id
        var eventName = data.name.ToLower();
        var existingEvent = AllStories[type].ContainsKey(eventName);   //.FirstOrDefault(e => e.key == data.id);

        //replace it if exists otherwise add it
        if (existingEvent)
            AllStories[type][eventName] = data;
        else
            AllStories[type].Add(eventName, data);
        WriteDataToFile();
    }

    /*public void SaveEvent(LoanStoryDataSerialized data)
    {
        //search for this event by id
        var existingEvent = _eventsDataContainer.loanEvents.FirstOrDefault(e => e.key == data.id);

        //replace it if exists otherwise add it
        if (existingEvent != null)
            _eventsDataContainer.loanEvents.Remove(existingEvent);

        _eventsDataContainer.loanEvents.Add(new DictionaryWrapper<LoanStoryDataSerialized>(data.id, data));
        WriteDataToFile();
    }*/

    public EventDataSerialized LoadSpecificStory(string keyName, int type = -1)
    {
        var keyLower = keyName.ToLower();
        if (type > 0)
        {
            var expectedType = (StoryType)type;
            
            if (AllStories[expectedType].ContainsKey(keyLower))
                return AllStories[expectedType][keyLower];
        }
        else
        {
            foreach (var dicType in AllStories)
            {
                if (dicType.Value.ContainsKey(keyLower))
                    return dicType.Value[keyLower];
            }
        }

        Debug.Log($"Event id {keyLower} not found");
        return null;
        /*if (AllStories[type].ContainsKey(idKey))
            return AllStories[type][idKey];
        else
            return null;*/
    }

    /*public EventDataSerialized LoadSpecificStory(string titleKey)
    {
        return _eventsDataContainer.GetSpecificEvent(titleKey);
    }*/

    private void LoadStoriesFromFile()
    {
        if (File.Exists(_eventsFilePath))
        {
            var jsonString = File.ReadAllText(_eventsFilePath);
            var eventsDataContainer = JsonUtility.FromJson<StoriesDataContainerObj>(jsonString);
            //TODO: make this more efficient
            foreach (var story in eventsDataContainer.RegularEvents)
            {
                AllStories[StoryType.Other].Add(story.key, story.value);
            }
            foreach (var story in eventsDataContainer.LoanEvents)
            {
                AllStories[StoryType.Loan].Add(story.key, story.value);
            }
        }
        /*else
            _eventsDataContainer = new StoriesDataContainerObj();*/
    }

    private void WriteDataToFile()
    {
        var dataContainer = new StoriesDataContainerObj(AllStories[StoryType.Other], AllStories[StoryType.Loan]);
        var jsonStr = JsonUtility.ToJson(dataContainer, true);
        File.WriteAllText($"{_SA_path}/{_eventsFileName}", jsonStr);
    }
}
