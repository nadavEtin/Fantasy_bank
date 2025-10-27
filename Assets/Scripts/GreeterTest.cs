using Reflex.Attributes;
using System.Collections.Generic;
using UnityEngine;

public class GreeterTest : MonoBehaviour
{
    [Inject] private IEnumerable<string> _greetingMessage;

    private void Start()
    {
        Debug.Log(string.Join(" ", _greetingMessage));
    }
}
