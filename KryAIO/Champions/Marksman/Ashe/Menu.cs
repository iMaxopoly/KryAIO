// ***********************************************************************
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

namespace KryAIO.Champions.Marksman.Ashe
{
    /// <summary>
    /// Class Ashe.
    /// </summary>
    /// <seealso cref="Champion" />
    /// <seealso cref="KryAIO.IChampion" />
    public partial class Ashe
    {
        /// <summary>
        /// Initializes the menu.
        /// </summary>
        private void InitializeMenu()
        {
            Orbwalker.Attach(Menu);

            var drawMenu = new Menu("Drawings", "Drawings")
            {
                new MenuBool("DrawW", "Draw W"),
                new MenuBool("DrawDamage", "Draw W+Auto Damage")
            };
            Menu.Add(drawMenu);


            Menu.Attach();
        }
    }
}