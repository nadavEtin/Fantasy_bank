using Assets.GameCore.Utility.ObjectPool;
using GameCore.EventBus;
using GameCore.ScriptableObjects;
using GameCore.Utility.GeneralClasses;
using GameCore.Utility.Screen;
using System;
using TMPro;
using UnityEngine;
using VContainer;

namespace GameEvent.EventResolution
{
    public class EventResolutionView : MonoBehaviour, IPoolable
    {
        [SerializeField] private GameObject _background;
        [SerializeField] private GameObject _confirmButtonObj;
        [SerializeField] private TextMeshPro _titleText, _mainText;

        private ScreenParams _screenParams;
        private IGameEventSettings _gameSettings;
        private IGenericButton _confirmBtn;
        private Action<BaseEventParams> _closeResolutionCb;
        private int _eventId;

        public Action<GameObject> ObjectPoolCb { get; set; }

        [Inject]
        private void Construct(ScreenParams screenParams, IGameEventSettings settings)
        {
            _screenParams = screenParams;
            _gameSettings = settings;
        }

        private void Start()
        {
            _confirmBtn = _confirmButtonObj.GetComponent<IGenericButton>();
            _background.transform.localScale =
                new Vector3(_gameSettings.EventResolutionViewSize.x * _screenParams.ScreenWidth,
                    _gameSettings.EventResolutionViewSize.y * _screenParams.ScreenHeight);
            _confirmBtn.Setup(ConfirmBtnClicked, 1);
        }

        public void Setup(string titleText, string mainText, int id, Action<BaseEventParams> callback)
        {
            _titleText.text = titleText;
            _mainText.text = mainText;
            _closeResolutionCb = callback;
            _eventId = id;
        }

        private void ConfirmBtnClicked()
        {
            //TODO: apply effects of the event resolving, if any

            //continue to the next resolution
            _closeResolutionCb.Invoke(null);
            ExecutePoolCb();
        }

        public void SetupReturnToPoolCb(Action<GameObject> cb)
        {
            ObjectPoolCb = cb;
        }

        public void ExecutePoolCb()
        {
            ObjectPoolCb(gameObject);
        }
    }
}