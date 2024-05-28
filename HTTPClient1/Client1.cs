using System;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

class Client1
{
    static async Task Main(string[] args)
    {
        string serverUri = "http://localhost:8080/";
        HttpClient httpClient = new HttpClient();
        string senderId = "Client1";
        string recipientId = "Client2";

        // Define message sizes
        int messageSizeKB = 1024; // 1KB
        int messageSizeMB = 1024 * 1024; // 1MB
        string messageKB = GenerateMessage(messageSizeKB);
        string messageMB = GenerateMessage(messageSizeMB);

        // Measure time for sending 1000 1KB messages
        Stopwatch stopwatch = Stopwatch.StartNew();
        int rep = 1;
        for (int i = 0; i < rep; i++)
        {
            await SendMessage(httpClient, serverUri, senderId, recipientId, messageKB);
        }
        stopwatch.Stop();
        string time1k = $"Time taken to send {rep} 1KB messages: {stopwatch.ElapsedMilliseconds} ms";
        WriteMessageToFile(time1k);

        // Send the last message with an indicator
        await SendMessage(httpClient, serverUri, senderId, recipientId, "LAST_MESSAGE");

        // Measure time for sending 1000 1MB messages
        stopwatch.Restart();
        for (int i = 0; i < rep; i++)
        {
            await SendMessage(httpClient, serverUri, senderId, recipientId, messageMB);
        }
        stopwatch.Stop();
        string time1m = $"Time taken to send {rep} 1MB messages: {stopwatch.ElapsedMilliseconds} ms";
        WriteMessageToFile(time1m);

        httpClient.Dispose();
    }

    static async Task SendMessage(HttpClient httpClient, string serverUri, string senderId, string recipientId, string message)
    {
        try
        {
            var messageContent = new StringContent(message, Encoding.UTF8, "text/plain");
            messageContent.Headers.Add("SenderId", senderId); // Include sender ID in message request
            messageContent.Headers.Add("RecipientId", recipientId); // Include recipient ID in message request
            await httpClient.PostAsync(serverUri, messageContent);



        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine($"Error sending message: {ex.Message}");
        }
    }

    static string GenerateMessage(int sizeInBytes)
    {
        byte[] buffer = new byte[sizeInBytes];
        new Random().NextBytes(buffer);
        return Encoding.UTF8.GetString(buffer);
    }

    static void WriteMessageToFile(string message)
    {
        File.AppendAllText("log.txt", message + "\n");
        Console.WriteLine(message);
    }
}
