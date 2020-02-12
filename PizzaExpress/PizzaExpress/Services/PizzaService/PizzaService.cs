using PizzaExpress.Models;
using PizzaExpress.Services.RequestProvider;
using PizzaExpress.Utils;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PizzaExpress.Services.PizzaService
{
    public class PizzaService : IPizzaService
    {
        private readonly IRequestProvider _requestProvider;

        public PizzaService(IRequestProvider requestProvider)
        {
            _requestProvider = requestProvider;
        }

        public async Task<IEnumerable<PizzaTopping>> Get()
        {
            return await _requestProvider.GetAsync<IEnumerable<PizzaTopping>>(Consts.BASE_URL);
        }

        public IEnumerable<PizzaToppingsModel> GetTop20(IEnumerable<PizzaTopping> pizzaToppings)
        {
            var orderedPizzaToppings = pizzaToppings.Select(p => p.Toppings.OrderBy(t => t));

            var separatedByCommaToppings = orderedPizzaToppings.Select(t => t.Aggregate((x, y) => x + ", " + y));

            var groupedToppings = separatedByCommaToppings
                .GroupBy(toppingsGroup => toppingsGroup)
                .Select(toppingsGroup => new PizzaToppingsModel()
                {
                    Combination = toppingsGroup.Key,
                    Frequency = toppingsGroup.Count()
                });

            var top20 = groupedToppings
                .Take(20)
                .OrderByDescending(x => x.Frequency)
                .ToList();

            for (int i = 0; i < top20.Count(); i++)
            {
                top20[i].Rank = i + 1;
            }

            return top20;
        }
    }
}
