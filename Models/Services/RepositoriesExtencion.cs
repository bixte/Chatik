using Chatik.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Chatik.Models.Services
{
    public static class RepositoriesExtencion
    {
        public static void AddRepositories(this IServiceCollection services)
        {
            services.AddTransient<DialogsRepository>();
             services.AddTransient<MessagesRepository>();
        }
    }
}
