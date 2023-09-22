using GameCore.Utility.Jsons;
using GameEvent;

public interface IStoriesRefs
{
    void InitSetup();
    EventDataSerialized LoadSpecificStory(int idKey, int type = -1);
    //EventDataSerialized LoadSpecificStory(string titleKey);
    //void SaveEvent(LoanStoryDataSerialized data);
    void SaveStory(EventDataSerialized data, StoryType type);
}