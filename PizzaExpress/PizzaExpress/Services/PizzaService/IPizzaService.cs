using PizzaExpress.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PizzaExpress.Services.PizzaService
{
    public interface IPizzaService
    {
        Task<IEnumerable<PizzaTopping>> Get();

        IEnumerable<PizzaToppingsModel> GetTop20(IEnumerable<PizzaTopping> pizzaToppings);
    }
}
