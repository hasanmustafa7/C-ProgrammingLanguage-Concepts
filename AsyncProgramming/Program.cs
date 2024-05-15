using System;
using System.Threading;
using System.Threading.Tasks;
namespace AsyncProgram
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
          
            using (var cancellationTokenSource = new CancellationTokenSource())
            {
                CancellationToken cancellationToken = cancellationTokenSource.Token;

                Task task =Task.Run(() => PerformAsyncOperation(cancellationToken));

                Console.WriteLine("Press any key to cancel the task");
                Console.ReadLine();

                cancellationTokenSource.Cancel();
                await task; // helps out to observe the result of exception raised
            }


            static async Task PerformAsyncOperation(CancellationToken cancellationToken)
            {
                try
                {
                    while (!cancellationToken.IsCancellationRequested)
                    {
                        // Simulate time consuming Taks
                        Console.WriteLine("Working...");
                        await Task.Delay(1000); // Simulate Time consuming Task
                        cancellationToken.ThrowIfCancellationRequested();
                    }
                }
                catch (OperationCanceledException)
                {
                    // Handle cancellation if needed
                    Console.WriteLine("Operation cancelled.");
                   
                }
            }
        }
    }
}