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
using System.Dynamic;

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

        public IActionResult Index(string cat, string sorton, string id)
        {

            dynamic ViewModel = new ExpandoObject();

            IConfigurationBuilder builder = new ConfigurationBuilder().AddJsonFile("azure-search.json");
            IConfigurationRoot configuration = builder.Build();

            SearchServiceClient serviceClient = CreateSearchServiceClient(configuration);
            string indexName = configuration["SearchIndexName"];
            ISearchIndexClient indexClient = serviceClient.Indexes.GetClient(indexName);

            SearchParameters parameters;
            DocumentSearchResult<Item> results;

            parameters = new SearchParameters();

            if(sorton == "phl")
            {
                sorton = "Price desc";
            }
            else if (sorton == "plh"){
                sorton = "Price";
            }
            else if (sorton == "tza")
            {
                sorton = "Title desc";
            }
            else if(sorton == "taz")
            {
                sorton = "Title";
            }
            else
            {
                sorton = "r";
            }


            if ((sorton != "r") && (cat != "all"))
            {
                parameters = new SearchParameters()
                    {
                        OrderBy = new[] { sorton },
                        Filter = "ItemCategory eq '" + cat + "'"
                    };
            }
            else if ((sorton == "r") && (cat != "all"))
            {
                parameters = new SearchParameters()
                {
                    Filter = "ItemCategory eq '" + cat + "'"
                };
            }
            else if ((sorton != "r") && (cat == "all"))
            {
                parameters = new SearchParameters()
                {
                    OrderBy = new[] { sorton }
                };
            }


            results = indexClient.Documents.Search<Item>(id, parameters);
            List<Item> SearchResultsItems = new List<Item>();

            foreach (SearchResult<Item> result in results.Results)
            {
                SearchResultsItems.Add(result.Document);
            }

            ViewModel.SearchResultsItems = SearchResultsItems;
            ViewModel.urltag = id;
            ViewModel.cat = cat;

            return View(ViewModel);
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
