using HtmlAgilityPack;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Net;
using System.Text;
using System.IO;

public class Program
{

    public static string GetResponse(string url)
    {
        try
        {
            var response = CallUrl(url).Result;
            return response;
        } catch {
            return "That town wasn't found on Wikipedia";
        }


    }
    public static async Task<string> CallUrl(string fullUrl)
    {
        HttpClient client = new HttpClient();
        var response = await client.GetStringAsync(fullUrl);
        return response;
    }

    public static string ParseResponse(string html)
    {
        HtmlDocument htmlDoc = new HtmlDocument();
        htmlDoc.LoadHtml(html);

        var tempNode = htmlDoc.DocumentNode.SelectSingleNode("//td[contains(@class, 'infobox-data')][1]");

        var result = tempNode.InnerText;
        result = result.Substring(0, result.IndexOf('&'));
        return result;
    }

    public static void Main(string[] args)
    {
        Console.Write("Input the town to lookup: ");
        var input = Console.ReadLine();
        var result = ParseResponse(GetResponse("https://en.wikipedia.org/wiki/" + input));
        Console.WriteLine("The population in {0} is currently: {1}", input, result);
    }
}

