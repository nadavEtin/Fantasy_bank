using GameCore.Utility.Jsons;
using GameEvent;
using System.Collections.Generic;

public interface IStoriesRefs
{
    Dictionary<StoryType, Dictionary<int, EventDataSerialized>> AllStories { get; }
    void InitSetup();
    EventDataSerialized LoadSpecificStory(int idKey, int type = -1);
    //EventDataSerialized LoadSpecificStory(string titleKey);
    //void SaveEvent(LoanStoryDataSerialized data);
    void SaveStory(EventDataSerialized data, StoryType type);
}