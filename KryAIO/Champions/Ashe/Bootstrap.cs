// ***********************************************************************
// Assembly         : KryAIO
// Author           : kryptodev
// Created          : 07-21-2017
//
// Last Modified By : kryptodev
// Last Modified On : 07-22-2017
// ***********************************************************************
// <copyright file="Bootstrap.cs" company="kryptodev.com">
//     Copyright © Kryptodev 2017
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace KryAIO.Champions.Ashe
{
    /// <summary>
    /// Class Ashe.
    /// </summary>
    /// <seealso cref="KryAIO.Champions.Champion" />
    /// <seealso cref="KryAIO.IChampion" />
    partial class Ashe
    {
        /// <summary>
        /// Bootstraps this instance.
        /// </summary>
        public void Bootstrap()
        {
            // Initialize Spells
            InitializeSpells();

            // Initialize Menu
            InitializeMenu();

            // Initialize Game Events
            InitializeEvents();
        }
    }
}