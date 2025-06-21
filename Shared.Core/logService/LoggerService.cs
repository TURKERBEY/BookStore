using Microsoft.AspNetCore.Diagnostics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Core.logService;
    public static class LogService
{
    private static string _logFolderPath { get; set; }
    private static string _logFilePath { get; set; }
    private static string _logFileName { get; set; }

    public static void Write(string msg)
    {
        _logFolderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "logs");
        DirectoryInfo directoryInfo = new DirectoryInfo(_logFolderPath);
        if (!directoryInfo.Exists)
        {
            directoryInfo.Create();
        }
        _logFileName = $"log-{DateTime.Now.Year}-{DateTime.Now.Month}-{DateTime.Now.Day}.txt";
        var logFilePath = Path.Combine(_logFolderPath, _logFileName);
        string path = Path.GetTempFileName(); // Bunun içeriğini kontrol et
        FileInfo fileInfo = new FileInfo(logFilePath);
        using (StreamWriter sw = fileInfo.Exists ? fileInfo.AppendText() : fileInfo.CreateText())
        {
            sw.WriteLine($"{DateTime.Now} - {msg}");
        }
    }

    public static void ExceptionWrite(Exception ex)
    {
        _logFolderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "logs");
        _logFileName = DateTime.Now.ToString();
        _logFileName = _logFileName.Replace(" ", "_");
        _logFileName = _logFileName.Replace(":", "-");
        _logFileName = _logFileName.Replace("/", "-");
        _logFileName += ".txt";

        _logFilePath = Path.Combine(_logFolderPath, _logFileName);

        DirectoryInfo directoryInfo = new DirectoryInfo(_logFolderPath);
        if (!directoryInfo.Exists)
        {
            directoryInfo.Create();
        }
        FileInfo fileInfo = new FileInfo(_logFilePath);
        var writer = fileInfo.CreateText();
        writer.WriteLine($"Hata Tarihi: {DateTime.Now}");
        writer.WriteLine($"Hata Message: {ex.Message}");
        writer.WriteLine($"Inner Exception: {ex.InnerException?.Message}");
        writer.WriteLine($"StackTrace: {ex.StackTrace}");
        writer.WriteLine("------------------------------------------------------");
        writer.Close();
    }
}