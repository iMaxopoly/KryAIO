// ***********************************************************************
// Assembly         : KryAIO
// Author           : kryptodev
// Created          : 07-28-2017
//
// Last Modified By : kryptodev
// Last Modified On : 07-28-2017
// ***********************************************************************
// <copyright file="ConsoleLogger.cs" company="kryptodev.com">
//     Copyright © Kryptodev 2017
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;

namespace KryAIO.Logger
{
    /// <summary>
    /// Class ConsoleLogger.
    /// </summary>
    /// <seealso cref="KryAIO.ILogger" />
    public class ConsoleLogger : ILogger
    {
        /// <summary>
        /// The synchronize lock
        /// </summary>
        private readonly object _syncLock;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConsoleLogger"/> class.
        /// </summary>
        public ConsoleLogger()
        {
            _syncLock = new object();
        }

        /// <summary>
        /// Logs the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="logType">Type of the log.</param>
        /// <param name="eventType">Type of the event.</param>
        public void Log(string message, LogType logType, EventType eventType)
        {
            lock (_syncLock)
            {
                Console.WriteLine($"{logType} | {eventType} | {DateTime.UtcNow}: {message}");
            }
        }
    }
}