using GameCore.ScriptableObjects;
using GameCore.Utility.GeneralClasses;
using GameCore.Utility.Screen;
using UnityEngine;
using VContainer;

namespace GameEvent.EventResolution
{
    public class EventResolutionView : MonoBehaviour
    {
        [SerializeField] private GameObject _background;
        [SerializeField] private GameObject _confirmButtonObj;

        private ScreenParams _screenParams;
        private IGameEventSettings _gameSettings;
        private IGenericButton _confirmBtn;

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

        private void ConfirmBtnClicked()
        {

        }
    }
}