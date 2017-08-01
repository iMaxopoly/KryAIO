// ***********************************************************************
// Assembly         : KryAIO
// Author           : kryptodev
// Created          : 08-01-2017
//
// Last Modified By : kryptodev
// Last Modified On : 08-01-2017
// ***********************************************************************
// <copyright file="NamedLocker.cs" company="kryptodev.com">
//     Copyright © Kryptodev 2017
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using System.Collections.Concurrent;

namespace KryAIO
{
    /// <summary>
    /// Class NamedLocker.
    /// Thanks to the interesting article at johnculviner.com
    /// </summary>
    public class NamedLocker
    {
        /// <summary>
        /// The lock dictionary
        /// </summary>
        private readonly ConcurrentDictionary<int, object> _lockDictionary = new ConcurrentDictionary<int, object>();

        /// <summary>
        /// Run a short lock inline using a lambda
        /// </summary>
        /// <typeparam name="TResult">The type of the t result.</typeparam>
        /// <param name="id">The identifier.</param>
        /// <param name="body">The body.</param>
        /// <returns>TResult.</returns>
        public TResult RunWithLock<TResult>(int id, Func<TResult> body)
        {
            lock (_lockDictionary.GetOrAdd(id, s => new object()))
            {
                return body();
            }
        }

        /// <summary>
        /// Run a short lock inline using a lambda
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="body">The body.</param>
        public void RunWithLock(int id, Action body)
        {
            lock (_lockDictionary.GetOrAdd(id, s => new object()))
            {
                body();
            }
        }
    }
}