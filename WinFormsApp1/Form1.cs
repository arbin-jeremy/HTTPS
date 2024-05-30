using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.ComponentModel;
using static System.Runtime.InteropServices.JavaScript.JSType;
namespace WinFormsApp1
{

    public partial class Form1 : Form
    {
        private Task serverTask;
        private HttpListener listener;
        static Dictionary<string, List<string>> fileStore = new Dictionary<string, List<string>>();
        static Dictionary<string, string> timestampStore = new Dictionary<string, string>(); 
        private int numFiles = 1000;
        private int fileSize = 1024;
        private string serverUrl = "http://localhost:5000/";
        public Form1()
        {
            InitializeComponent();
            textBoxFileSize.Text = "1";
            textBoxNumFiles.Text = "1000";
            textBoxServerUrl.Text = "http://localhost:5000/"; 

        }

        private void serverbtn_Click(object sender, EventArgs e)
        {
            //default values
            serverTask = Task.Run(() => StartServer());
            serverbtn.Enabled = false;
        }
        private void StartServer()
        {
            string prefix = serverUrl;
            listener = new HttpListener();
            listener.Prefixes.Add(prefix);
            listener.Start();
            MessageBox.Show("Server started");
            // string consoleappPath = @"C:\Users\Zhiyuan.Y\testing\HTTPS.test\ConsoleApp1\bin\Debug\net7.0\ConsoleApp1.exe";

            // Process.Start(consoleappPath); // Start the console app
            while (true)
            {
                var context = listener.GetContext();
                Task.Run(() => HandleRequest(context));
            }
        }
        private async Task HandleRequest(HttpListenerContext context)
        {
            var request = context.Request;
            var response = context.Response;

            if (request.HttpMethod == "POST" && request.Url.AbsolutePath == "/upload")
            {
                string recipient = request.QueryString["recipient"];
                if (string.IsNullOrEmpty(recipient))
                {
                    response.StatusCode = 400; // Bad Request
                    await response.OutputStream.WriteAsync(System.Text.Encoding.UTF8.GetBytes("Recipient not specified"));
                    response.Close();
                    return;
                }

                using (var ms = new MemoryStream())
                {
                    await request.InputStream.CopyToAsync(ms);
                    var fileData = ms.ToArray();
                    string filePath = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());
                    File.WriteAllBytes(filePath, fileData);

                    if (!fileStore.ContainsKey(recipient))
                    {
                        fileStore[recipient] = new List<string>();
                    }
                    fileStore[recipient].Add(filePath);
                }

                response.StatusCode = 200; // OK
                await response.OutputStream.WriteAsync(System.Text.Encoding.UTF8.GetBytes("File uploaded"));
                response.Close();
            }
            // handle timestamp request from clients 
            else if (request.HttpMethod == "POST" && request.Url.AbsolutePath == "/timestamp")
            {
                string recipient = request.QueryString["recipient"];
                using (StreamReader reader = new StreamReader(request.InputStream, request.ContentEncoding))
                {
                    string timestamp = await reader.ReadToEndAsync();
                    timestampStore[recipient] = timestamp;
                    
                    if (recipient == "client1")
                    {
                        Invoke((Action)(() =>
                        {
                            textBoxTimeStamp1.Text = timestamp;
                        }));
                    }
                    else 
                    {
                        Invoke((Action)(() =>
                        {
                            textBoxTimeStamp2.Text = timestamp;
                        }));
                        if (timestampStore.Count == 2)
                        {
                            TimeSpan ts = DateTime.Parse(timestamp) - DateTime.Parse(timestampStore["client1"]);
                            Invoke((Action)(() =>
                            {
                                textBoxTotalTime.Text = ts.TotalSeconds.ToString();
                            }));
                        }
                       
                    }
                }
                response.StatusCode = 200; // OK
                await response.OutputStream.WriteAsync(System.Text.Encoding.UTF8.GetBytes("Timestamp uploaded"));
                response.Close();

            }
            else if (request.HttpMethod == "GET" && request.Url.AbsolutePath == "/download")
            {
                string recipient = request.QueryString["recipient"];
                if (string.IsNullOrEmpty(recipient) || !fileStore.ContainsKey(recipient) || fileStore[recipient].Count == 0)
                {
                    response.StatusCode = 404; // Not Found
                    await response.OutputStream.WriteAsync(System.Text.Encoding.UTF8.GetBytes("No files available for recipient"));
                    response.Close();
                    return;
                }

                string filePath = fileStore[recipient][0];
                fileStore[recipient].RemoveAt(0);

                byte[] fileBytes = File.ReadAllBytes(filePath);
                response.ContentType = "application/octet-stream";
                response.ContentLength64 = fileBytes.Length;
                await response.OutputStream.WriteAsync(fileBytes, 0, fileBytes.Length);
                response.Close();



                File.Delete(filePath); // Clean up

            }
            else
            {
                response.StatusCode = 404; // Not Found
                await response.OutputStream.WriteAsync(System.Text.Encoding.UTF8.GetBytes("Invalid endpoint"));
                response.Close();
            }
        }

        private void client1btn_Click(object sender, EventArgs e)
        {
            string client1path = $@"{AppDomain.CurrentDomain.BaseDirectory}..\..\..\..\ConsoleApp1\bin\Debug\net7.0\Client1.exe";
            Task.Run(() => System.Diagnostics.Process.Start(client1path, $"{numFiles} {fileSize} {serverUrl}")); // Start the client1 app
        }

        private void client2btn_Click(object sender, EventArgs e)
        {
            string client2path = $@"{AppDomain.CurrentDomain.BaseDirectory}..\..\..\..\Client2\bin\Debug\net7.0\Client2.exe";
            Task.Run(() => System.Diagnostics.Process.Start(client2path, $"{numFiles} {serverUrl}")); // Start the client2 app
        }

        private void textBoxFileSize_TextChanged(object sender, EventArgs e)
        {
            fileSize = int.Parse(textBoxFileSize.Text) * 1024;
        }

        private void textBoxNumFiles_TextChanged(object sender, EventArgs e)
        {
            numFiles = int.Parse(textBoxNumFiles.Text); // Number of files to download
        }

        private void textBoxServerUrl_TextChanged(object sender, EventArgs e)
        {
            serverUrl = textBoxServerUrl.Text;
        }

       
    }
}
