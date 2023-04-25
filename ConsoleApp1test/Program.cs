using System;
using System.Net.Http;
using System.Threading.Tasks;
using Serilog;
using Serilog.Sinks.File;

namespace PaytureTest
{
    public class Program
    {
        public static async Task Main()
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.File("log.txt")
                .CreateLogger();

            var url = "https://sandbox3.payture.com/api/Pay";
            var reader = new ConsoleReader();
            Console.WriteLine("Enter the payment details");
            
            var _amount = reader.ReadString("Amount");
            var guid = Guid.NewGuid();
            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Post, url)

            {
                Content = new FormUrlEncodedContent(new Dictionary<string, string>
                {
                    { "Key", "Merchant" },
                    { "Amount", $"{_amount}"},
                    { "OrderId", $"{guid}"},
                    { "PayInfo", $@"PAN={reader.ReadString("PAN")};
                                   EMonth={reader.ReadString("EMonth")};
                                   EYear={reader.ReadString("EYear")};
                                   CardHolder={reader.ReadString("CardHolder")};
                                   SecureCode={reader.ReadString("SecureCode")};
                                   OrderId={guid};
                                   Amount={_amount}"},
                    { "CustomFields", @"IP=230.137.123.5;
                                        Product=Ticket"}
                })
            };
            Log.Information($"Adding payment: Key = Merchant, Amount = {_amount}, orderId = {guid}");


            try
               {
                 var Message = $"Sending request : OrderId = {guid}/Amount = {_amount} on URL: {url}";
                 Log.Information(Message);
                 Console.WriteLine(Message);
                 
                 var response = await client.SendAsync(request);
                 Message = "Request Succeed";
                 Log.Information(Message);
                 Console.WriteLine(Message);


                 Message = "Getting Response";

                 Log.Information(Message);
                 Console.WriteLine(Message);

                 
                 var responseContent = await response.Content.ReadAsStringAsync();

                 Message = $"Get Server response succeed: {responseContent}";

                 Log.Information(Message);
                 Console.WriteLine(Message);
                }
            catch (Exception ex)
                {
                 Log.Error($"Exception : {ex.Message}");
                 Console.WriteLine($"Exception : {ex.Message}");
                }

            Log.CloseAndFlush();
        }
    }
}