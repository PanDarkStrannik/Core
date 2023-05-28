namespace GameCore.Patterns
{
    public interface IDataDriver<TData>
    {
        public TData Data { get;}
        public void Initialize(TData data);
    }
}
