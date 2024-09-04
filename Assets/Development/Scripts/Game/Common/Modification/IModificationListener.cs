namespace Modification
{
    public interface IModificationListener<T>
    {
        void OnModificationUpdate(T value);
    }
}

