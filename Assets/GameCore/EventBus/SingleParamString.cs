namespace GameCore.EventBus
{
    public class SingleParamString : BaseEventParams
    {
        public string Value;

        public SingleParamString(string value)
        {
            Value = value;
        }
    }
}