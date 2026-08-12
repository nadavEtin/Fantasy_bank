using Bank;
using GameCore.ScriptableObjects;
using GameEvent.LoanEvent;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GameEvent
{
    public class StoryValidator : IStoryValidator
    {
        private const int GeneratedEventIdStart = 1000;

        private IBankBalance _bankBalance;
        private RoundSettings _roundSettings;

        private List<int> _completedEvents;
        private List<IGameDataEvent> _unavailableEvents;
        private List<IGameDataEvent> _availableEventsPool;

        private Random _random;

        public StoryValidator(IBankBalance bankBalance, RoundSettings roundSettings)
        {
            _completedEvents = new List<int>();
            _unavailableEvents = new List<IGameDataEvent>();
            _availableEventsPool = new List<IGameDataEvent>();
            _bankBalance = bankBalance;
            _roundSettings = roundSettings;
            _random = new Random();           
        }

        public void GameStart()
        {
            _completedEvents.Clear();
            _unavailableEvents.Clear();
            _availableEventsPool.Clear();

            for (var i = 0; i < _roundSettings.EventsPerRound; i++)
            {
                var cost = Roll(_roundSettings.MinLoanCost, _roundSettings.MaxLoanCost);
                var successChance = Roll(_roundSettings.MinSuccessChance, _roundSettings.MaxSuccessChance);
                var duration = Roll(_roundSettings.MinDuration, _roundSettings.MaxDuration);
                var eventText = $"Cost: {cost}\nSuccess chance: {successChance}%\nDuration: {duration} turns";

                _unavailableEvents.Add(new LoanGameEventData(
                    GeneratedEventIdStart + i,
                    eventText,
                    $"Loan Offer #{i + 1}",
                    "Loan Completed",
                    "The loan has reached its resolution.",
                    duration,
                    null,
                    _bankBalance,
                    cost,
                    successChance,
                    StoryType.Loan,
                    Array.Empty<int>()));
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
            var result = new List<IGameDataEvent>();
            UpdateAvailableEvents();
            for (int i = 0; i < _roundSettings.EventsPerRound; i++)
            {
                if (_availableEventsPool.Count > 0)
                {
                    var rnd = _random.Next(_availableEventsPool.Count);
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

        private int Roll(int min, int max)
        {
            return _random.Next(min, max + 1);
        }
    }
}