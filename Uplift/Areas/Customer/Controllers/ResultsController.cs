using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Uplift.Models;
using Uplift.DataAccess.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Azure.Search;
using Microsoft.Azure.Search.Models;

namespace Uplift.Controllers
{
    [Area("Customer")]
    public class ResultsController : Controller
    {
        private readonly ILogger<ResultsController> _logger;

        public ResultsController(ILogger<ResultsController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index(string id)
        {


            IConfigurationBuilder builder = new ConfigurationBuilder().AddJsonFile("azure-search.json");
            IConfigurationRoot configuration = builder.Build();

            SearchServiceClient serviceClient = CreateSearchServiceClient(configuration);

            string indexName = configuration["SearchIndexName"];

            ISearchIndexClient indexClient = serviceClient.Indexes.GetClient(indexName);

            SearchParameters parameters;
            DocumentSearchResult<Item> results;


            // Query 1 
            Console.WriteLine("Query 1: Search for term 'product' with no result trimming");

            parameters = new SearchParameters();

            /*
            {
                OrderBy = new[] { "Price" },
            };
            */
            results = indexClient.Documents.Search<Item>(id, parameters);
            List<Item> SearchResultsItems = new List<Item>();

            foreach (SearchResult<Item> result in results.Results)
            {
                SearchResultsItems.Add(result.Document);
            }

            /*
            var displayItem = new Item();
            displayItem.Title = "Title1";
            displayItem.Price = 89.99;
            displayItem.ItemDescription = "This is the item description";
            SearchResultsItems.Add(displayItem);
            */
            return View(SearchResultsItems);
        }


        private static SearchServiceClient CreateSearchServiceClient(IConfigurationRoot configuration)
        {
            string searchServiceName = configuration["SearchServiceName"];
            string adminApiKey = configuration["SearchServiceAdminApiKey"];

            SearchServiceClient serviceClient = new SearchServiceClient(searchServiceName, new SearchCredentials(adminApiKey));
            return serviceClient;
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


    }
}
