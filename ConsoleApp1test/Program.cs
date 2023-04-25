using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace PaytureTest
{
    public class Program
    {
        public static async Task Main()
        {
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

            try
               {
                 Console.WriteLine($"Sending request : OrderId = {guid}/Amount = {_amount} on URL: {url}");
                 var response = await client.SendAsync(request);
                 Console.WriteLine("Request Succeed");

                 Console.WriteLine($"Getting Response");
                 var responseContent = await response.Content.ReadAsStringAsync();
                 Console.WriteLine($"Get Server response succeed: {responseContent}");
                }
            catch (Exception ex)
                {
                 Console.WriteLine($"Exception : {ex.Message}");
                }
        }
    }
}