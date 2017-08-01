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

            var manaMenu = new Menu("ManaManagement", "Mana Management")
            {
                new MenuSeperator("Q-Related", "Q-Related"),
                new MenuSliderBool("QHarass", "Q Harass", true, 70, 0, 100),
                new MenuSliderBool("QFarm", "Q Farm", true, 80, 0, 100),

                new MenuSeperator("W-Related", "W-Related"),
                new MenuSliderBool("WHarass", "W Harass", true, 12, 0, 100),
                new MenuBool("WFarm>55", "W Farm >  55%"),
                new MenuBool("WFarm>30<=55", "W Farm >  30% && <= 55%"),
                new MenuBool("WFarm<=30", "W Farm <= 30%"),
            };

            var hitChanceMenu = new Menu("HitChance", "Hit Chance Related")
            {
                new MenuSlider("WFarm>55", "W Farm >  55%", 0, 0, 4),
                new MenuSlider("WFarm>30<=55", "W Farm >  30% & <= 55%", 0, 0, 4),
                new MenuSlider("WFarm<=30", "W Farm <= 30%", 0, 0, 4),
                new MenuSeperator("HitChangeSliderUserInfo", "W Farming Hitchance Info:"),
                new MenuSeperator("0", "0 = >=Low,  1 = >=Medium"),
                new MenuSeperator("1", "2 = >=High, 3 = >=Very High"),
                new MenuSeperator("2", "4 = ==Immobile")
            };

            var logicMenu = new Menu("LogicRelated", "Logic Related")
            {
                new MenuBool("WFarmEnemyNear", "W Farm If Enemy Near"),
                new MenuBool("REnemyUnderTower", "R Enemy Under Same Ally Tower"),
                new MenuSliderBool("REnemyIfDanger", "R Enemy If In Danger @ Health%", true, 33,  1, 100)
            };

            var drawMenu = new Menu("Drawings", "Drawings")
            {
                new MenuBool("DrawW", "Draw W")
            };

            Menu.Add(manaMenu);
            Menu.Add(hitChanceMenu);
            Menu.Add(logicMenu);
            Menu.Add(drawMenu);
            Menu.Add(CreditsMenu);

            Menu.Attach();
        }
    }
}