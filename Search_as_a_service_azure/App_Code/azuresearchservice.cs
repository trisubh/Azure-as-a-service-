using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Threading;
using Microsoft.Azure.Search;
using Microsoft.Azure.Search.Models;
using Microsoft.Spatial;
using Search_as_a_service_azure.Models;



namespace Search_as_a_service_azure.App_Code
{
    public class azuresearchservice
    {

       public  List<Hotel> AllHotels;
       SearchServiceClient serviceClient;

        public azuresearchservice()
        {

            string searchServiceName = "searchme";

            string apiKey = "1BB5B636FC15489314EA859AE989CD49";

            serviceClient = new SearchServiceClient(searchServiceName, new SearchCredentials(apiKey));

            DeleteHotelsIndexIfExists(serviceClient);

            CreateHotelsIndex(serviceClient);

            SearchIndexClient indexClient = serviceClient.Indexes.GetClient("hotels");

            UploadDocuments(indexClient);

        }

        public SearchIndexClient getIndexClient()
        {
          return  serviceClient.Indexes.GetClient("hotels");
        }

        public  void DeleteHotelsIndexIfExists(SearchServiceClient serviceClient)
        {
            if(serviceClient.Indexers.Exists("hotels"))
            {
                serviceClient.Indexes.Delete("hotels");
            }
        }

        public  void CreateHotelsIndex(SearchServiceClient serviceClient)
        {
            var definition = new Index()
            {
                Name = "hotels",
                Fields = new[]
                {
                    new Field ("hotelId",DataType.String){IsKey =true},
                    new Field ("hotelName",DataType.String){IsSearchable =true,IsFilterable=true},
                    new Field ("baseRate",DataType.String){IsFilterable=true,IsSearchable =true},
                    new Field("category",DataType.String){IsSearchable=true,IsFilterable=true,IsSortable=true,IsFacetable=true},
                    new Field ("tags",DataType.Collection(DataType.String)){IsSearchable=true,IsFilterable=true},
                    new Field("parkingIncluded",DataType.Boolean){IsFilterable=true,IsFacetable=true},
                    new Field("lastRenovationDate",DataType.DateTimeOffset){IsFilterable=true,IsSortable =true,IsFacetable=true},
                    new Field("rating",DataType.Int32){IsFilterable=true,IsSortable=true,IsFacetable=true},
                    new Field("location",DataType.GeographyPoint){IsFilterable=true,IsSortable=true}
                }
            };
         
        //    serviceClient.Indexes.Create(definition);
        }

        public  List<Hotel> allHotels()
        {
            return AllHotels;
        }
       

        public  void UploadDocuments(SearchIndexClient indexClient) 
        {
            var documents = new Hotel[]
             {
                 new Hotel()
                 {
                     hotelId="1058-441",
                     hotelName="Fancy Stay",
                     baseRate="199.0",
                     category="Luxary",
                     tags=new[]{"pool","view","concierge"},
                     parkingIncluded=false,
                     lastRenovationDate=new DateTimeOffset(2010, 6, 27, 0, 0, 0, TimeSpan.Zero),
                     rating=5,
                     location=GeographyPoint.Create(47.678581, -122.131577)
                 },

                 new Hotel()
                 {
                     hotelId="666-437",
                     hotelName="Roach Motel",
                     baseRate="79.99",
                     category="Budget",
                     tags=new[]{"motel","budget"},
                     parkingIncluded=true,
                     lastRenovationDate=new DateTimeOffset(1082, 4, 28, 0, 0, 0, TimeSpan.Zero),
                     rating=1,
                     location=GeographyPoint.Create(49.678581, -122.131577)
                 },

                  new Hotel()
                 {
                     hotelId="970-501",
                     hotelName="Econo-Stay",
                     baseRate="129.99",
                     category="Budget",
                     tags=new[]{"pool","budget"},
                     parkingIncluded=true,
                     lastRenovationDate=new DateTimeOffset(1995, 7, 1, 0, 0, 0, TimeSpan.Zero),
                     rating=4,
                     location=GeographyPoint.Create(46.678581, -122.131577)
                 },

                  new Hotel()
                 {
                     hotelId="956-532",
                     hotelName="Express Rooms",
                     baseRate="129.99",
                     category="Budget",
                     tags=new[]{"wifi","budget"},
                     parkingIncluded=true,
                     lastRenovationDate=new DateTimeOffset(1995, 7, 1, 0, 0, 0, TimeSpan.Zero),
                     rating=4,
                     location=GeographyPoint.Create(48.678581, -122.131577)
                 },


                 new Hotel()
                 {
                     hotelId="566-518",
                     hotelName="Surprisingly Expensive Suites",
                     baseRate="279.99",
                     category="Luxury",
                     parkingIncluded=false,
                  
                 },
             };


            try
            {
                var batch = IndexBatch.Upload(documents);
                indexClient.Documents.Index(batch);
                AllHotels = new List<Hotel>();
                foreach(var item in documents)
                {
                    AllHotels.Add(item);
                }
            }

            catch(IndexBatchException e)
            {
                Console.WriteLine("Failed to index some of the documents: {0}", String.Join(",", e.IndexingResults.Where(r => !r.Succeeded).Select(r => r.Key)));
            }

            Thread.Sleep(2000);
        }

        public  List<Hotel> SearchDocuments(SearchIndexClient indexClient,string searchText,string filter=null)
        {
            List<Hotel> ls = new List<Hotel>();
            var sp = new SearchParameters();

            if(!string.IsNullOrEmpty(filter))
            {
                sp.Filter = filter;
            }

            DocumentSearchResult<Hotel> response = indexClient.Documents.Search<Hotel>(searchText, sp);

            foreach(SearchResult<Hotel> result in response.Results)
            {
                ls.Add(result.Document);
            }

            return ls;
        }

    }
}