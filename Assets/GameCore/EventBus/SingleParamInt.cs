namespace GameCore.EventBus
{
    public class SingleParamInt : BaseEventParams
    {
        public int Value;

        public SingleParamInt(int value)
        {
            Value = value;
        }
    }
}