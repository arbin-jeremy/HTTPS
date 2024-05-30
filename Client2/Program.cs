using System;
using System.IO;
using System.Net.Http;
using System.Diagnostics;
using System.Threading.Tasks;

class Program
{
    static async Task Main(string[] args)
    {
        if (args.Length < 2)
        {
            Console.WriteLine("Usage: Client2 <numfiles> <serverUrl>");
            Console.WriteLine("Press [Enter] to exit the program.");
            Console.ReadLine();
            return;
        }
        string serverUrl = $"{args[1]}download?recipient=client2";
        string savePath =Path.GetTempPath(); // Update the save path

        int numFiles = int.Parse(args[0]); // Number of files to download

        if (numFiles <= 0)
        {
            Console.WriteLine("Invalid number of files");
            return;
        }
        int fileSize = 0;

        Stopwatch stopwatch = new Stopwatch();

        using (HttpClient client = new HttpClient())
        {
            for (int i = 0; i < numFiles; i++)
            {
                while (true)
                {
                    var response = await client.GetAsync(serverUrl);
                    if (response.IsSuccessStatusCode)
                    {
                        if (stopwatch.IsRunning == false)
                        {
                            stopwatch.Start();
                        }
                        byte[] fileBytes = await response.Content.ReadAsByteArrayAsync();
                        fileSize = fileBytes.Length;
                        string filePath = Path.Combine(savePath, $"downloaded{i}.bin");
                        File.WriteAllBytes(filePath, fileBytes);
                        Console.WriteLine("File downloaded and saved to " + savePath);
                        break; // Exit the loop
                    }
                    else
                    {
                        await Task.Delay(100); // Retry after 100 millisecond
                    }

                }
            }
        }
        stopwatch.Stop();
        string endTime = DateTime.UtcNow.ToString("HH:mm:ss.fff");
        

        long totalBytes = numFiles * fileSize;
        double totalMB = totalBytes / (1024.0 * 1024.0);
        double transferRate = totalMB / stopwatch.Elapsed.TotalSeconds;
        Console.WriteLine($"Total transfer rate: {transferRate} MB/s");
        
        Console.WriteLine($"Files downloaded: {numFiles} files");
        Console.WriteLine($"File size: {fileSize} bytes");
        Console.WriteLine(); 

        //File.AppendAllText("log.txt", $"Total transfer rate: {transferRate} MB/s\n");
        Console.WriteLine($"Client2 finished at {endTime}");
        Console.WriteLine($"Time taken for download: {stopwatch.Elapsed.TotalSeconds} seconds");


        //calculatetotaltime(endTime);

        await Task.Delay(1000); // Delay for 1 second
        serverUrl = $"{args[1]}timestamp?recipient=client2";
        using (HttpClient client = new HttpClient())
        {
            using (var content = new StringContent(endTime))
            {
                var response = await client.PostAsync(serverUrl, content);
                string result = await response.Content.ReadAsStringAsync();
                Console.WriteLine(result);
            }

        }
        Console.WriteLine("Press [Enter] to exit the program.");
        Console.ReadLine();
    }

    private static void calculatetotaltime(string endTime)
    {
        // Calculate total time taken
        try
        {
            string lastline = File.ReadLines("log.txt").AsParallel().Last();
            if (lastline.Contains("Client1 started at"))
            {
                DateTime start = DateTime.Parse(lastline.Replace("Client1 started at ", ""));
                DateTime end = DateTime.Parse(endTime);
                TimeSpan totaltime = end - start;
                Console.WriteLine($"Total time taken: {totaltime.TotalSeconds} seconds");
                File.AppendAllText("log.txt", $"Client2 finished at {endTime}\n");
                File.AppendAllText("log.txt", $"Total time taken: {totaltime.TotalSeconds} seconds\n");
            }
        }
        catch (Exception e)
        {
            Console.WriteLine("Error: " + e.Message);
        }
    }
}