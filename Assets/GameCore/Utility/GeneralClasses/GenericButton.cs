using System;
using UnityEngine;

namespace GameCore.Utility.GeneralClasses
{
    public class GenericButton : MonoBehaviour, IGenericButton
    {
        private Action _btnClickCallback;
        private int _id;

        //TODO: remove the id if its not used
        public void Setup(Action cb, int id)
        {
            _btnClickCallback = cb;
            _id = id;
        }

        private void OnMouseDown()
        {
            Debug.Log("btn was clicked!");
            _btnClickCallback?.Invoke();
        }
    }
}
