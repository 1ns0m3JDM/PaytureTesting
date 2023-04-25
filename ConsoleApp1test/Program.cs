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
            var guid = Guid.NewGuid();
            var amount = reader.ReadString("Amount");
            var pan = reader.ReadString("PAN");
            var eMonth = reader.ReadString("EMonth");
            var eYear = reader.ReadString("EYear");
            var cardHolder = reader.ReadString("CardHolder");
            var secureCode = reader.ReadString("SecureCode");

            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Post, url)

            {
                Content = new FormUrlEncodedContent(new Dictionary<string, string>
                {
                    { "Key", "Merchant" },
                    { "Amount", $"{amount}"},
                    { "OrderId", $"{guid}"},
                    { "PayInfo", $@"PAN={pan};
                                   EMonth={eMonth};
                                   EYear={eYear};
                                   CardHolder={cardHolder};
                                   SecureCode={secureCode};
                                   OrderId={guid};
                                   Amount={amount}"},
                    { "CustomFields", @"IP=230.137.123.5;
                                        Product=Ticket"}
                })
            };
            Log.Information($"Adding payment: Key = Merchant, Amount = {amount}, orderId = {guid}");


            try
            {
                var message = $"Sending request : OrderId = {guid}/Amount = {amount} on URL: {url}";
                Log.Information(message);
                Console.WriteLine(message);

                var response = await client.SendAsync(request);
                message = "Request Succeed";
                Log.Information(message);
                Console.WriteLine(message);


                message = "Getting Response";

                Log.Information(message);
                Console.WriteLine(message);


                var responseContent = await response.Content.ReadAsStringAsync();

                message = $"Get Server response succeed: {responseContent}";

                Log.Information(message);
                Console.WriteLine(message);
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