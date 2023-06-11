using UnityEngine;

namespace GameCore.Factories
{
    public class GenericObjectFactory : BaseGameObjectFactory
    {
        public GenericObjectFactory(EventBus.EventBus eventBus)
        {
            
        }
        
        public override GameObject Create()
        {
            return null;
        }
    }
}