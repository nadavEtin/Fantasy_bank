using UnityEngine;

namespace GameCore.ScriptableObjects
{
    [CreateAssetMenu(fileName = "RoundSettings", menuName = "Scriptable Objects/Round settings")]
    public class RoundSettings : ScriptableObject
    {
        [SerializeField, Min(1)] private int _eventsPerRound = 20;
        [SerializeField, Min(0)] private int _minLoanCost = 10;
        [SerializeField, Min(0)] private int _maxLoanCost = 100;
        [SerializeField, Range(5, 95)] private int _minSuccessChance = 5;
        [SerializeField, Range(5, 95)] private int _maxSuccessChance = 95;
        [SerializeField, Min(1)] private int _minDuration = 1;
        [SerializeField, Min(1)] private int _maxDuration = 5;

        public int EventsPerRound => _eventsPerRound;
        public int MinLoanCost => _minLoanCost;
        public int MaxLoanCost => _maxLoanCost;
        public int MinSuccessChance => _minSuccessChance;
        public int MaxSuccessChance => _maxSuccessChance;
        public int MinDuration => _minDuration;
        public int MaxDuration => _maxDuration;

        private void OnValidate()
        {
            _eventsPerRound = Mathf.Max(1, _eventsPerRound);
            _minLoanCost = Mathf.Max(0, _minLoanCost);
            _maxLoanCost = Mathf.Max(_minLoanCost, _maxLoanCost);
            _minSuccessChance = Mathf.Clamp(_minSuccessChance, 5, 95);
            _maxSuccessChance = Mathf.Clamp(_maxSuccessChance, _minSuccessChance, 95);
            _minDuration = Mathf.Max(1, _minDuration);
            _maxDuration = Mathf.Max(_minDuration, _maxDuration);
        }
    }
}
