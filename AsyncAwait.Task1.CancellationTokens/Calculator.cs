using System;
using System.Threading;
using System.Threading.Tasks;

namespace AsyncAwait.Task1.CancellationTokens;

internal static class Calculator
{
    // todo: change this method to support cancellation token
    public static long Calculate(int n /*, CancellationToken token*/)
    {
        long sum = 0;

        for (var i = 0; i < n; i++)
        {
            // i + 1 is to allow 2147483647 (Max(Int32)) 
            sum = sum + (i + 1);
            Thread.Sleep(10);
        }

        return sum;
    }


    /// <summary>
    /// Calculates 0 to n. If the token is cancelled, throws an exception
    /// </summary>
    /// <param name="n"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    public static Task<long> CalculateAsync(int n, CancellationToken token)
    {
        return Task.Run(() =>
        {
            long sum = 0;

            for (var i = 0; i < n; i++)
            {
                token.ThrowIfCancellationRequested(); 
                sum = sum + (i + 1);
                Thread.Sleep(10);
                Console.WriteLine($"Calculating now... The sum is {sum}");
            }

            return sum;
        }, token);
    }
}
