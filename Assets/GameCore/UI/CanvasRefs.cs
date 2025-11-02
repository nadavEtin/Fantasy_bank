using UnityEngine;
using UnityEngine.UI;

public class CanvasRefs : MonoBehaviour, ICanvasRefs
{
    [SerializeField] private RectTransform _storyEvenCountdownHolder;

    public RectTransform StoryEventCountdownHolder => _storyEvenCountdownHolder;
}
