namespace Entities.Core
{
    public interface IComponent
    {
        void OnAdd();
        void OnRemove();
        void OnDestroy();
    }
}