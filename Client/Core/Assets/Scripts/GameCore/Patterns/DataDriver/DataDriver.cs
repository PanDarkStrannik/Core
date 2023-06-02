namespace GameCore.Patterns
{
    public interface IController
    {
        public void Initialize(IData someData);
    }

    public interface IDataController<TData> : IController
    {
        public TData Data { get;}
        public void Initialize(TData data);
    }

    public interface IData
    {

    }
}
