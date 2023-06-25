using System;
using GameCore.Utility.Screen;
using TMPro;
using UnityEngine;

namespace GameEvent.EventCountdown
{
    public class EventCountdown : MonoBehaviour
    {
        [SerializeField] private Transform _retractedBasePos;
        [SerializeField] private TextMeshPro _eventName, _countdownNum;

        private ScreenParams _screenParams;
        private Vector2 _defaultHiddenPos;
        private bool _extended;

        private void Construct(ScreenParams screenParams)
        {
            _screenParams = screenParams;
            
        }

        private void Start()
        {
            transform.position = new Vector3(_retractedBasePos.position.x, transform.position.y);
        }

        private void OnMouseDown()
        {
            if(_extended)
                PullbackView();
            else
                ExtendView();
        }

        private void ExtendView()
        {
            
        }

        private void PullbackView()
        {
            
        }
    }
}
