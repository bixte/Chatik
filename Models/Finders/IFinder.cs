namespace Chatik.Models
{
    public interface IFinder<T>
    {
        public T FindAsync();
    }
}
