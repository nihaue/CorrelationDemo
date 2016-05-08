using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuickLearn.Demo.XmlUtility.Extensions
{
    public static class AsyncExtensions
    {
        public static Task ForEachAsync<T>(this IEnumerable<T> source,
            int maxDegreeOfParallelism, Func<T, Task> body)
        {
            return Task.WhenAll(
                from partition in Partitioner.Create(source).GetPartitions(maxDegreeOfParallelism)
                select Task.Run(async () =>
                                {   while (partition.MoveNext())
                                        await body(partition.Current);
                                }));
        }
    }
}
