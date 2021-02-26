using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace HttpGetAsyncFirst
{
    class Program
    {
        static async Task Main(string[] args)
        {
            using var client = new HttpClient();

            var uris = new[] 
            {
                @"http://lite.cnn.com/en/article/h_c9bf2c57a31d6f1c8d2f5f2d4d9b3f1f",
                @"http://lite.cnn.com/en/article/h_9cf7e8d30baf454d2c5885eb2729f507"
            };

            var tasks = new List<Task<string>>();

            foreach (var uri in uris)
            {
                tasks.Add(Task.Run(async () =>
                {
                    return await client.GetStringAsync(uri);
                }));
            }

            var continuation = Task.WhenAll(tasks);

            var directoryPath = @"E:\DirectoryHttpGetAsyncFirst";
            if (!Directory.Exists(directoryPath))
                Directory.CreateDirectory(directoryPath);

            foreach (var result in continuation.Result)
            {
                var fileName = Path.GetRandomFileName();
                var filePath = Path.Combine(directoryPath, fileName);

                File.WriteAllText(filePath, result);

                var fileInfo = new FileInfo(filePath);
                Console.WriteLine(fileInfo.Length + " bytes");
            }
        }
    }
}
