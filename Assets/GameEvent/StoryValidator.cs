using Bank;
using GameCore.Utility.Jsons;
using GameEvent.StoryView;
using GameEvent.LoanEvent;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GameEvent
{
    public class StoryValidator : IStoryValidator
    {
        private IBankBalance _bankBalance;
        private IStoriesRefs _storyRefs;

        private List<int> _completedEvents;
        //private Dictionary<StoryType, Dictionary<int, EventDataSerialized>> _unavailableEvents;
        private List<IGameDataEvent> _unavailableEvents;
        private List<IGameDataEvent> _availableEventsPool;

        public StoryValidator(IBankBalance bankBalance, IStoriesRefs storyRefs)
        {
            _completedEvents = new List<int>();
            _unavailableEvents = new List<IGameDataEvent>();
            _availableEventsPool = new List<IGameDataEvent>();
            _bankBalance = bankBalance;
            _storyRefs = storyRefs;
            //_unavailableEvents = _storyRefs.AllStories;            
        }

        public void GameStart()
        {
            foreach (var storyType in _storyRefs.AllStories)
            {
                switch (storyType.Key)
                {
                    case StoryType.Other:
                        break;
                    case StoryType.Loan:
                        foreach (var story in storyType.Value.Values)
                        {
                            var loan = (LoanStoryDataSerialized)story;
                            var loanData = new LoanGameEventData(loan, loan.loanCost, loan.chanceOfSuccess);
                            _unavailableEvents.Add(loanData);
                        }
                        break;
                    default:
                        break;
                }                
            }
            UpdateAvailableEvents();
        }

        public void EventCompleted(int id)
        {
            _completedEvents.Add(id);
            _availableEventsPool.Remove(_availableEventsPool.FirstOrDefault(a => a.ID == id));
            UpdateAvailableEvents();
        }

        public void UpdateAvailableEvents()
        {
            foreach (var eve in _unavailableEvents.ToList())
            {
                if (EventRequirementsMet(eve.EventRequirements))
                {
                    //Add the event to the pool of available events
                    _availableEventsPool.Add(eve);

                    //Remove the event from the general pool of unavailable events
                    _unavailableEvents.Remove(eve);
                }
            }
        }

        public bool StoryEventValidationEntry(IGameDataEvent eventData)
        {
            var res = false;
            if (GeneralEventValidation(eventData))
            {
                switch (eventData.EventType)
                {
                    case StoryType.Loan:
                        res = LoanTypeValidation(eventData);
                        break;
                    default:
                        break;
                }
            }

            return res;
        }

        public List<IGameDataEvent> GetStoriesForCurrentTurn()
        {
            Random random = new Random();
            var result = new List<IGameDataEvent>();
            UpdateAvailableEvents();
            for (int i = 0; i < 3; i++)
            {
                if (_availableEventsPool.Count > 0)
                {
                    var rnd = random.Next(_availableEventsPool.Count - 1);
                    result.Add(_availableEventsPool[rnd]);
                    _availableEventsPool.RemoveAt(rnd);
                }
                else
                {
                    //Debug.Log("out of events");
                    break;
                }
            }

            return result;
        }

        private bool GeneralEventValidation(IGameDataEvent eventView)
        {
            return EventRequirementsMet(eventView.EventRequirements);
        }

        private bool EventRequirementsMet(IReadOnlyCollection<int> requirements)
        {
            if (requirements == null || requirements.Count == 0)
                return true;

            //all event requirements for event "eve" have been completed
            return requirements.All(req => _completedEvents.Contains(req));
        }

        private bool LoanTypeValidation(IGameDataEvent eventData)
        {
            var loanData = (ILoanGameDataEvent)eventData;
            return _bankBalance.GoldBalance >= loanData.LoanPrice;
        }
    }
}