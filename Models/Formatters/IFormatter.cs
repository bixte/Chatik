using System.Threading.Tasks;

namespace Chatik.Models
{
    public interface IFormatter<T>
    {
        public Task<T> FormatteAsync();
    }
}
