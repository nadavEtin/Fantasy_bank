using GameCore.Utility.Jsons;
using GameEvent;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;

[CreateAssetMenu(fileName = "StoriesRefs", menuName = "Scriptable Objects/Stories References")]
public class StoriesRefs : ScriptableObject, IStoriesRefs
{
    public Dictionary<StoryType, Dictionary<string, EventDataSerialized>> AllStories { private set; get; }

    [SerializeField] private string _storiesApiUrl = "http://localhost/fantasy_bank_api/get_stories.php";
    [SerializeField] private bool _loadFromApi = true;

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

        if (_loadFromApi && TryLoadStoriesFromApi())
            return;

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

    // ponytail: blocks boot for localhost; swap to async InitSetup if API isn't local/fast
    private bool TryLoadStoriesFromApi()
    {
        using var req = UnityWebRequest.Get(_storiesApiUrl);
        var op = req.SendWebRequest();
        while (!op.isDone) { }

        if (req.result != UnityWebRequest.Result.Success)
        {
            Debug.LogWarning($"Stories API failed ({req.error}), falling back to local file.");
            return false;
        }

        return FillStoriesFromJson(req.downloadHandler.text);
    }

    private void LoadStoriesFromFile()
    {
        if (!File.Exists(_eventsFilePath))
            return;

        FillStoriesFromJson(File.ReadAllText(_eventsFilePath));
    }

    private bool FillStoriesFromJson(string jsonString)
    {
        var eventsDataContainer = JsonUtility.FromJson<StoriesDataContainerObj>(jsonString);
        if (eventsDataContainer == null)
            return false;

        AllStories[StoryType.Other].Clear();
        AllStories[StoryType.Loan].Clear();

        if (eventsDataContainer.RegularEvents != null)
        {
            foreach (var story in eventsDataContainer.RegularEvents)
                AllStories[StoryType.Other].Add(story.key, story.value);
        }
        if (eventsDataContainer.LoanEvents != null)
        {
            foreach (var story in eventsDataContainer.LoanEvents)
                AllStories[StoryType.Loan].Add(story.key, story.value);
        }

        return true;
    }

    private void WriteDataToFile()
    {
        var dataContainer = new StoriesDataContainerObj(AllStories[StoryType.Other], AllStories[StoryType.Loan]);
        var jsonStr = JsonUtility.ToJson(dataContainer, true);
        File.WriteAllText($"{_SA_path}/{_eventsFileName}", jsonStr);
    }
}
