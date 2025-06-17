using RestSharp;

var client = new RestClient("https://httpbin.org");
var request = new RestRequest("get", Method.Get);
var response = client.Execute(request);
Console.WriteLine(response.Content);