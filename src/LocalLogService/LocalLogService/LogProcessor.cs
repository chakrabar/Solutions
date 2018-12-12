using LocalLogService.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace LocalLogService
{
    public class LogProcessor
    {
        IConfiguration _configuration;

        public LogProcessor(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<IEnumerable<Log>> GetLogsAsync()
        {
            var logFilePath = GetLatestLogFilePath();
            var logLines = await File.ReadAllLinesAsync(logFilePath);
            var dataLines = logLines.Skip(1); //skip header
            var logs = new List<Log>();
            foreach (var line in dataLines)
            {
                logs.Add(GetLogFromCsvLine(line));
            }
            return logs;
        }

        public async Task<Log> GetLogAsync(int id)
        {
            var logFilePath = GetLatestLogFilePath();
            var logLines = await File.ReadAllLinesAsync(logFilePath);
            var dataLines = logLines.Skip(1); //skip header
            foreach (var line in dataLines)
            {
                var log = GetLogFromCsvLine(line);
                if (log.Id == id)
                    return log;
            }
            return null;
        }

        public async Task<int> WriteToLogAsync(string title, string message)
        {
            var latestLogFile = GetLatestLogFilePath();
            var lastLog = File.ReadLines(latestLogFile) //not thread safe
                .Last();
            var lastLogId = int.Parse(lastLog.Split(',').First());
            var newId = ++lastLogId;

            var newLog = new Log
            {
                Id = newId,
                TimeStamp = DateTime.UtcNow,
                Title = title,
                Message = message
            };
            await LogAsync(newLog, latestLogFile);
            return newLog.Id;
        }

        private async Task LogAsync(Log log, string logFile)
        {
            var logline = $"{log.Id},{log.TimeStamp.ToString("yyyy-MM-ddTHH:mm:ss")}"
                + $",{log.Title},{log.Message}";
            await File.AppendAllTextAsync(logFile, $"{logline}{Environment.NewLine}");
        }

        private string GetLatestLogFilePath()
        {
            var logDirectory = _configuration.GetValue<string>("RobotLogPath");

            var logFiles = Directory.GetFiles(logDirectory);
            return logFiles
                .OrderByDescending(f => GetDateFromFileName(f))
                .First();

            int GetDateFromFileName(string filename)
            {
                filename = Path.GetFileName(filename);
                var date = filename
                    .Replace("log_", string.Empty)
                    .Replace(".log", string.Empty);
                return int.Parse(date);
            }
        }

        private Log GetLogFromCsvLine(string line)
        {
            var dataPieces = line.Split(','); //gross csv reading
            var log = new Log
            {
                Id = int.Parse(dataPieces[0]),
                TimeStamp = DateTime.Parse(dataPieces[1]),
                Title = dataPieces[2],
                Message = dataPieces[3]
            };
            return log;
        }
    }
}
