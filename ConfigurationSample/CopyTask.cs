using System;
using System.Configuration;
using System.IO;
using Taskmatics.Scheduler.Core;

namespace ConfigurationSample
{
    [InputParameters(typeof(CopyTaskParameters))]
    public class CopyTask : TaskBase
    {
        protected override void Execute()
        {
            var copyParameters = (CopyTaskParameters)Context.Parameters;
            var maxFileSizeMegabytes = int.Parse(ConfigurationManager.AppSettings["maxFileSizeMegabytes"]);
            var maxFileSizeBytes = maxFileSizeMegabytes * 1024 * 1024;

            if (!Directory.Exists(copyParameters.SourceFolderPath))
            {
                Context.Logger.Log("Could not find the source path '{0}'.", copyParameters.SourceFolderPath);
                return;
            }

            var copyCount = 0;
            foreach (var sourceFilePath in Directory.GetFiles(copyParameters.SourceFolderPath, "*.*", SearchOption.AllDirectories))
            {
                var fileInfo = new FileInfo(sourceFilePath);
                var destinationFilePath = Path.Combine(copyParameters.DestinationFolderPath, sourceFilePath.Substring(copyParameters.SourceFolderPath.Length).TrimStart('\\'));
                var destinationFileFolderPath = Path.GetDirectoryName(destinationFilePath);

                if (fileInfo.Length > maxFileSizeBytes)
                {
                    Context.Logger.Log("Cannot copy file '{0}' because its size ({1}MB) exceeds the maximum allowed file size of {2}MB.",
                        sourceFilePath,
                        fileInfo.Length / 1024.0 / 1024.0,
                        maxFileSizeMegabytes);
                    continue;
                }

                if (!Directory.Exists(destinationFileFolderPath))
                    Directory.CreateDirectory(destinationFileFolderPath);

                Context.Logger.Log("Copying '{0}' to '{1}'.", sourceFilePath, destinationFilePath);
                fileInfo.CopyTo(destinationFilePath, true);
                copyCount++;
            }

            Context.Logger.Log("Copy completed. Copied {0} files from '{1}' to '{2}'.", copyCount, copyParameters.SourceFolderPath, copyParameters.DestinationFolderPath);
        }
    }
}
