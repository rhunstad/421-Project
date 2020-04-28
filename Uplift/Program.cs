using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Azure.Search;
using Microsoft.Azure.Search.Models;
using Uplift.Models;

namespace Uplift
{
    public class Program
    {
        public static void Main(string[] args)
        {
            createSearchService(args);

            CreateHostBuilder(args).Build().Run();
            
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });



        static void createSearchService(string[] args)
        {
            IConfigurationBuilder builder = new ConfigurationBuilder().AddJsonFile("azure-search.json");
            IConfigurationRoot configuration = builder.Build();

            SearchServiceClient serviceClient = CreateSearchServiceClient(configuration);

            string indexName = configuration["SearchIndexName"];


            // I COMMENTED THE FOLLOWING 4 LINES OUT:

            //System.Diagnostics.Debug.WriteLine("Deleting index...\n");
            //DeleteIndexIfExists(indexName, serviceClient);

            //System.Diagnostics.Debug.WriteLine("Creating index...\n");
            //CreateIndex(indexName, serviceClient);

            ISearchIndexClient indexClient = serviceClient.Indexes.GetClient(indexName);
            System.Diagnostics.Debug.WriteLine("Uploading documents...\n");
            // UploadDocuments(indexClient);

            // INSERT THE SNIPPET IN ScaffoldingReadMe.txt HERE: 

            System.Diagnostics.Debug.WriteLine("Searching index for jacket...\n");
            RunQueries(indexClient);

            Console.WriteLine("Creating data source...");

            System.Diagnostics.Debug.WriteLine("Process complete \n");
        }

        // Create the search service client
        private static SearchServiceClient CreateSearchServiceClient(IConfigurationRoot configuration)
        {
            string searchServiceName = configuration["SearchServiceName"];
            string adminApiKey = configuration["SearchServiceAdminApiKey"];

            SearchServiceClient serviceClient = new SearchServiceClient(searchServiceName, new SearchCredentials(adminApiKey));
            return serviceClient;
        }

        // Delete an existing index to reuse its name
        private static void DeleteIndexIfExists(string indexName, SearchServiceClient serviceClient)
        {
            if (serviceClient.Indexes.Exists(indexName))
            {
                serviceClient.Indexes.Delete(indexName);
            }
        }

        // Create an index whose fields correspond to the properties of the Hotel class.
        // The Address property of Hotel will be modeled as a complex field.
        // The properties of the Address class in turn correspond to sub-fields of the Address complex field.
        // The fields of the index are defined by calling the FieldBuilder.BuildForType() method.
        private static void CreateIndex(string indexName, SearchServiceClient serviceClient)
        {
            var definition = new Microsoft.Azure.Search.Models.Index()
            {
                Name = indexName,
                Fields = FieldBuilder.BuildForType<Item>()
            };


            serviceClient.Indexes.Create(definition);
        }

        private static void WriteDocuments(DocumentSearchResult<Item> searchResults)
        {

            System.Diagnostics.Debug.WriteLine("Writing results...");

            foreach (SearchResult<Item> result in searchResults.Results)
            {
                System.Diagnostics.Debug.WriteLine(result.Document.ItemID);
            }
        }

        private static void RunQueries(ISearchIndexClient indexClient)
        {


            SearchParameters parameters;
            DocumentSearchResult<Item> results;


            // Query 1 
            Console.WriteLine("Query 1: Search for term 'jacket' with no result trimming");
            parameters = new SearchParameters()
            {
                OrderBy = new[] { "Price" },
            };
            results = indexClient.Documents.Search<Item>("jacket", parameters);
            WriteDocuments(results);




            //ADDITIONAL QUERIES FROM THE MICROSOFT TUTORIAL:
            /*
             *
             * 
            
            // Query 2
            System.Diagnostics.Debug.WriteLine("Query 2: Search on the term 'product', with trimming");
            System.Diagnostics.Debug.WriteLine("Returning only these fields: 'ItemID' :\n");
            parameters =
                new SearchParameters()
                {
                    Select = new[] { "ItemID" },
                    OrderBy = new[] { "Rating desc" },
                };
            results = indexClient.Documents.Search<Item>("product", parameters);
            WriteDocuments(results);


            // Query 3
            
            Console.WriteLine("Query 3: Search for the terms 'restaurant' and 'wifi'");
            Console.WriteLine("Return only these fields: HotelName, Description, and Tags:\n");
            parameters =
                new SearchParameters()
                {
                    Select = new[] { "HotelName", "Description", "Tags" }
                };
            results = indexClient.Documents.Search<Hotel>("restaurant, wifi", parameters);
            WriteDocuments(results);

            // Query 4 -filtered query
            Console.WriteLine("Query 4: Filter on ratings greater than 4");
            Console.WriteLine("Returning only these fields: HotelName, Rating:\n");
            parameters =
                new SearchParameters()
                {
                    Filter = "Rating gt 4",
                    Select = new[] { "HotelName", "Rating" }
                };
            results = indexClient.Documents.Search<Hotel>("*", parameters);
            WriteDocuments(results);

            // Query 5 - top 2 results
            Console.WriteLine("Query 5: Search on term 'boutique'");
            Console.WriteLine("Sort by rating in descending order, taking the top two results");
            Console.WriteLine("Returning only these fields: HotelId, HotelName, Category, Rating:\n");
            parameters =
                new SearchParameters()
                {
                    OrderBy = new[] { "Rating desc" },
                    Select = new[] { "HotelId", "HotelName", "Category", "Rating" },
                    Top = 2
                };
            results = indexClient.Documents.Search<Hotel>("boutique", parameters);
            WriteDocuments(results);
            */
        }
    }
}    

