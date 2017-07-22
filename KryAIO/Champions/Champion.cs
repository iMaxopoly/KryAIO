// ***********************************************************************
// Assembly         : KryAIO
// Author           : kryptodev
// Created          : 07-22-2017
//
// Last Modified By : kryptodev
// Last Modified On : 07-22-2017
// ***********************************************************************
// <copyright file="Champion.cs" company="kryptodev.com">
//     Copyright © Kryptodev 2017
// </copyright>
// <summary></summary>
// ***********************************************************************

using Aimtec;
using Aimtec.SDK.Menu;
using Aimtec.SDK.Orbwalking;
using System;

namespace KryAIO.Champions
{
    /// <summary>
    /// Class Champion.
    /// </summary>
    /// <seealso cref="System.IDisposable" />
    public abstract class Champion : IDisposable
    {
        /// <summary>
        /// The local hero
        /// </summary>
        protected readonly Obj_AI_Hero LocalHero;

        /// <summary>
        /// The orbwalker
        /// </summary>
        protected readonly Orbwalker Orbwalker;

        /// <summary>
        /// The menu
        /// </summary>
        protected readonly Menu Menu;

        /// <summary>
        /// Initializes a new instance of the <see cref="Champion" /> class.
        /// </summary>
        protected Champion()
        {
            LocalHero = ObjectManager.GetLocalPlayer();
            Orbwalker = new Orbwalker();
            Menu = new Menu(null, null, true);
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (!disposing) return;

            Orbwalker.Dispose();
            Menu.Dispose();
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}