namespace sharpcada.Core.Contracts;

public interface ICoreFactory<T, A>
{
    public T Create(A entity);
    public Dictionary<long, T> CreateDictionary(ICollection<A> entitis);
}
