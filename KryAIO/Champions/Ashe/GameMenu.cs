﻿// ***********************************************************************
// Assembly         : KryAIO
// Author           : kryptodev
// Created          : 07-21-2017
//
// Last Modified By : kryptodev
// Last Modified On : 07-22-2017
// ***********************************************************************
// <copyright file="GameMenu.cs" company="kryptodev.com">
//     Copyright © Kryptodev 2017
// </copyright>
// <summary></summary>
// ***********************************************************************

using Aimtec.SDK.Menu;
using Aimtec.SDK.Menu.Components;

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
        /// Initializes the menu.
        /// </summary>
        private void InitializeMenu()
        {
            Orbwalker.Attach(Menu);
            var drawMenu = new Menu("Drawings", "Drawings");
            {
                drawMenu.Add(new MenuBool("DrawW", "Draw W"));
            }
            Menu.Add(drawMenu);
            Menu.Attach();
        }
    }
}