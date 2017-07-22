// ***********************************************************************
// Assembly         : KryAIO
// Author           : kryptodev
// Created          : 07-22-2017
//
// Last Modified By : kryptodev
// Last Modified On : 07-22-2017
// ***********************************************************************
// <copyright file="GameDrawEvents.cs" company="kryptodev.com">
//     Copyright © Kryptodev 2017
// </copyright>
// <summary></summary>
// ***********************************************************************

using Aimtec;
using System.Drawing;

namespace KryAIO.Champions.Ashe
{
    /// <summary>
    /// Class Ashe.
    /// </summary>
    /// <seealso cref="KryAIO.Champions.Champion" />
    /// <seealso cref="KryAIO.IChampion" />
    public partial class Ashe
    {
        /// <summary>
        /// Called when [present].
        /// </summary>
        protected virtual void OnPresent()
        {
            if (LocalHero.IsDead) return;

            if (_spellW.Ready && Menu["Drawings"]["DrawW"].Enabled)
            {
                Render.Circle(LocalHero.Position, _spellW.Range, 30, Color.Purple);
            }
        }
    }
}