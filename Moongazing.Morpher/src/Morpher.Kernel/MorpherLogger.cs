using System;

namespace Morpher.Kernel
{
    /// <summary>
    /// Centralized logger for mapping errors and diagnostics.
    /// </summary>
    public static class MorpherLogger
    {
        /// <summary>
        /// Logs an error message with exception details.
        /// </summary>
        /// <param name="message">Error message to log.</param>
        /// <param name="exception">Exception details to include in the log.</param>
        public static void LogError(string message, Exception exception)
        {
            Console.WriteLine($"[Morpher Error] {message}: {exception.Message}");
        }
    }
}
