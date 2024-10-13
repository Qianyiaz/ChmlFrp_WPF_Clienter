using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq; // 需要安装 Newtonsoft.Json
using System.Diagnostics;

class Program
{
    static async Task Main()
    {
        // 获取当前工作目录
        string currentDirectory = Directory.GetCurrentDirectory();

        // GitHub API URL
        string apiUrl = "https://api.github.com/repos/Qianyiaz/ChmlFrpLauncher/releases/latest";

        using (HttpClient httpClient = new HttpClient())
        {
            // 添加 User-Agent 头部
            httpClient.DefaultRequestHeaders.Add("User-Agent", "YourAppName");

            try
            {
                var response = await httpClient.GetStringAsync(apiUrl);
                var json = JObject.Parse(response);
                var tagName = json["tag_name"].ToString();

                string versionString = tagName.StartsWith("v") ? tagName.Substring(1) : tagName;

                Version latestVersion = new Version(tagName);
                Version targetVersion = new Version("1.8.6");

                if (latestVersion > targetVersion)
                {
                    Console.WriteLine($"有新版本可用: {tagName}");

                    var assets = json["assets"];
                    if (assets != null && assets.HasValues)
                    {
                        foreach (var asset in assets)
                        {
                            string downloadUrl = asset["browser_download_url"].ToString();
                            string downloadFileName = asset["name"].ToString();
                            string fullPath = Path.Combine(currentDirectory, downloadFileName);

                            // 下载文件
                            using (WebClient webClient = new WebClient())
                            {
                                await webClient.DownloadFileTaskAsync(downloadUrl, fullPath);
                                Console.WriteLine($"文件已下载至: {fullPath}");
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("没有找到可用的发布资源.");
                    }
                }
            }
            catch (HttpRequestException httpRequestException)
            {
                Console.WriteLine($"HTTP 请求错误: {httpRequestException.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"下载文件时出错: {ex.Message}");
            }
            start:
            Thread.Sleep(2000);
            Console.Clear();

            Console.WriteLine("{1}启动frpc");

            string input = Console.ReadLine();
            int number;



            if (int.TryParse(input, out number)) // 尝试将输入转换为整数
            {
                if (number != 1)
                {
                    Console.WriteLine("输入无效，请输入一个有效的数字。");
      
                    goto start;
                }
                Console.WriteLine($"您输入的数字是: {number}");

                
                if (number == 1)
                {
                    Thread.Sleep(2000);
                    string command = "frpc"; // 你可以替换为任何命令

                    // 创建一个新的进程
                    Process process = new Process();
                    process.StartInfo.FileName = "cmd.exe"; // 指定程序
                    process.StartInfo.Arguments = "/c " + command; // /c 后跟命令
                    process.StartInfo.UseShellExecute = false; // 不使用操作系统外壳启动
                    process.StartInfo.RedirectStandardOutput = true; // 重定向标准输出
                    process.StartInfo.CreateNoWindow = true; // 创建窗口

                    try
                    {
                        // 启动新进程
                        process.Start();

                        // 可选：读取并输出命令输出
                        string output = process.StandardOutput.ReadToEnd();
                        Console.WriteLine(output);

                        // 等待进程结束
                        process.WaitForExit();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"发生错误: {ex.Message}");
                    }
                }
            }
            else
            {
                Console.WriteLine("输入无效，请输入一个有效的数字。");
            }

            goto start;
        }
    }
}
