﻿using GameCore.Utility.Jsons;
using GameEvent.StoryView;
using System.Collections.Generic;

namespace GameEvent
{
    public interface IStoryValidator
    {
        List<IGameDataEvent> GetStoriesForCurrentTurn();
        void UpdateAvailableEvents();
        void EventCompleted(int id);
        bool EventValidationEntry(IGameDataEvent eventData);
        void GameStart();
    }
}