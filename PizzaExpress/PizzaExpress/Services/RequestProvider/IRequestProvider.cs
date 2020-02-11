using System.Threading.Tasks;

namespace PizzaExpress.Services.RequestProvider
{
    public interface IRequestProvider
    {
        Task<TResult> GetAsync<TResult>(string uri);
    }
}
