using Taskmatics.Scheduler.Core;

namespace ConfigurationSample
{
    public class Program
    {
        static void Main(string[] args)
        {
            // This code is used when running the task locally for testing purposes. 
            var parameters = new CopyTaskParameters();
            parameters.SourceFolderPath = @"c:\temp";
            parameters.DestinationFolderPath = @"c:\windows\temp";
            
            var harness = new TaskHarness<CopyTask>(parameters);
            harness.Execute();
            harness.WaitForCompletion();
        }
    }
}
