using GameCore;
using Reflex.Attributes;
using UnityEngine;
using UnityEngine.Events;


public class TestingScript : MonoBehaviour
{
    [Inject] private GameDirector _gameDirector;

    private void Start()
    {
        _gameDirector.PubStartGame();
    }
}
