namespace GameCore.Patterns
{
    public interface IDataController<TData>
    {
        public TData Data { get;}
        public void Initialize(TData data);
    }

    public interface IData
    {

    }
}
