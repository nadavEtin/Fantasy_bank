using System;

namespace GameCore.Utility.GeneralClasses
{
    public interface IGenericButton
    {
        void Setup(Action cb, int id);
    }
}