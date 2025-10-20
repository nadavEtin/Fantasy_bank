using GameCore.Utility.Jsons;
using GameEvent;
using System.Collections.Generic;

public interface IStoriesRefs
{
    Dictionary<StoryType, Dictionary<string, EventDataSerialized>> AllStories { get; }
    void InitSetup();
    EventDataSerialized LoadSpecificStory(string stringKey, int type = -1);
    //EventDataSerialized LoadSpecificStory(string titleKey);
    //void SaveEvent(LoanStoryDataSerialized data);
    void SaveStory(EventDataSerialized data, StoryType type);
}