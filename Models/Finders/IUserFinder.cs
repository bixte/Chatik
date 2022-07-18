using Chatik.DataModels;
using System.Threading.Tasks;

namespace Chatik.Models.Finders
{
    public interface IUserFinder
    {
        public  Task<User> FindAsync();
    }
}
