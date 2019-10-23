// -----------------------------------------------------------------------
//  <copyright file="RollingFileLoggerProvider.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2017 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor></last-editor>
//  <last-date>2017-09-17 21:19</last-date>
// -----------------------------------------------------------------------

using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

using OSharp.Logging.RollingFile.Internal;

namespace OSharp.Logging.RollingFile
{
    /// <summary>
    /// An <see cref="ILoggerProvider" /> that writes logs to a file
    /// </summary>
    ///power by https://github.com/andrewlock/NetEscapades.Extensions.Logging
    [ProviderAlias("File")]
    public class FileLoggerProvider : BatchingLoggerProvider
    {
        private readonly string _fileName;
        private readonly int? _maxFileSize;
        private readonly int? _maxRetainedFiles;
        private readonly string _path;

        /// <summary>
        /// Creates an instance of the <see cref="FileLoggerProvider" />
        /// </summary>
        /// <param name="options">The options object controlling the logger</param>
        public FileLoggerProvider(IOptions<FileLoggerOptions> options)
            : base(options)
        {
            var loggerOptions = options.Value;
            this._path = loggerOptions.LogDirectory;
            this._fileName = loggerOptions.FileName;
            this._maxFileSize = loggerOptions.FileSizeLimit;
            this._maxRetainedFiles = loggerOptions.RetainedFileCountLimit;
        }

        /// <inheritdoc />
        protected override async Task WriteMessagesAsync(IEnumerable<LogMessageEntry> messages, CancellationToken cancellationToken)
        {
            Directory.CreateDirectory(this._path);

            foreach (var group in messages.GroupBy(this.GetGrouping))
            {
                var fullName = this.GetFullName(group.Key);
                var fileInfo = new FileInfo(fullName);
                if (this._maxFileSize > 0 && fileInfo.Exists && fileInfo.Length > this._maxFileSize)
                {
                    return;
                }

                using (var streamWriter = File.AppendText(fullName))
                {
                    foreach (var item in group)
                    {
                        await streamWriter.WriteAsync(item.Message);
                    }
                }
            }

            this.RollFiles();
        }

        private string GetFullName((int Year, int Month, int Day) group)
        {
            return Path.Combine(this._path, $"{this._fileName}{group.Year:0000}{group.Month:00}{group.Day:00}.txt");
        }

        private (int Year, int Month, int Day) GetGrouping(LogMessageEntry message)
        {
            return (message.Timestamp.Year, message.Timestamp.Month, message.Timestamp.Day);
        }

        /// <summary>
        /// Deletes old log files, keeping a number of files defined by <see cref="FileLoggerOptions.RetainedFileCountLimit" />
        /// </summary>
        protected void RollFiles()
        {
            if (this._maxRetainedFiles > 0)
            {
                var files = new DirectoryInfo(this._path)
                    .GetFiles(this._fileName + "*")
                    .OrderByDescending(f => f.Name)
                    .Skip(this._maxRetainedFiles.Value);

                foreach (var item in files)
                {
                    item.Delete();
                }
            }
        }
    }
}