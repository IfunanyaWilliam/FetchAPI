using System.Net.Http;
using Newtonsoft.Json.Linq;
using ConsoleTables;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace FetchAPI
{
    public class Program
    {
        static async Task Main(string[] args)
        {
           await GetAPI();
        }

        public static async Task GetAPI()
        {
            string API_URL = "https://jsonmock.hackerrank.com/api/article_users/search";
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    using HttpResponseMessage responseMsg = await client.GetAsync(API_URL);
                    using HttpContent content = responseMsg.Content;

                    var stringData = await content.ReadAsStringAsync();
                    if(stringData != null)
                    {
                        var ObjectToString = JObject.Parse(stringData);
                        var pageData = new FetchPage()
                        {
                            Page = (int)ObjectToString["page"],
                            PerPage = (int)ObjectToString["per_page"],
                            Total = (int)ObjectToString["total"],
                            TotalPage = (int) ObjectToString["total_pages"]
                        };
                        foreach(var data in ObjectToString["data"])
                        {
                            pageData.Data.Add(new Data()
                            {
                                Id              = (int) data["id"],
                                UserName        = (string) data["username"],
                                About           = Truncate(((string)data["about"])),
                                Submitted       = (int) data["submitted"],
                                UpdatedAt       = (string) data["updated_at"],
                                SubmissionCount = (int) data["submission_count"],
                                CommentCount    = (int)data["comment_count"],
                                CtreatedAt      = UnixTimeConvert((long) data["created_at"])
                            });
                        }

                        Console.WriteLine($"\n");
                        var table = new ConsoleTable("Id", "About", "Username", "Comment Count", "Submission",
                                                    "Submission Count", "Updated At", "Created At");
                        pageData.Data.ForEach(data =>

                            table.AddRow(data.Id,data.About, data.UserName, data.CommentCount, data.Submitted,
                                            data.SubmissionCount, data.UpdatedAt, data.CtreatedAt));
                        table.Write();
                        Console.ReadKey();
                        Console.WriteLine();
                    }

                    
                   
                       

                }
            }
            catch (Exception exception)
            {
                Console.WriteLine("There was an error during file fetch ");
                Console.WriteLine(exception);
            }
        }

        public static string Truncate(string str)
        {
            return str.Length > 40 ? str.Substring(0, 30) + "..." : str + "...";
        }

        public static string UnixTimeConvert(long time)
        {
            DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            dateTime = dateTime.AddMilliseconds(time).ToLocalTime();
            return dateTime.ToShortDateString();
        }
    }


}
