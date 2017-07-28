// ***********************************************************************
// Assembly         : KryAIO
// Author           : kryptodev
// Created          : 07-22-2017
//
// Last Modified By : kryptodev
// Last Modified On : 07-28-2017
// ***********************************************************************
// <copyright file="Champion.cs" company="kryptodev.com">
//     Copyright © Kryptodev 2017
// </copyright>
// <summary></summary>
// ***********************************************************************

using Aimtec;
using Aimtec.SDK.Menu;
using Aimtec.SDK.Orbwalking;
using KryAIO.Logger;
using System;

namespace KryAIO.Champions.Champion
{
    /// <summary>
    /// Class Champion.
    /// </summary>
    /// <seealso cref="System.IDisposable" />
    public abstract partial class Champion : IDisposable
    {
        /// <summary>
        /// The logger
        /// </summary>
        protected readonly ILogger Logger;

        /// <summary>
        /// The local hero
        /// </summary>
        protected readonly Obj_AI_Hero LocalHero;

        /// <summary>
        /// The orbwalker
        /// </summary>
        protected readonly IOrbwalker Orbwalker;

        /// <summary>
        /// The krywalker
        /// </summary>
        protected readonly Krywalk Krywalker;

        /// <summary>
        /// The menu
        /// </summary>
        protected readonly Menu Menu;

        /// <summary>
        /// The list of jungle monsters
        /// </summary>
        protected readonly string[] JungleMonsters =
        {
            "SRU_Dragon_Air", "SRU_Dragon_Fire", "SRU_Dragon_Water",
            "SRU_Dragon_Earth", "SRU_Dragon_Elder", "SRU_Baron",
            "SRU_RiftHerald", "SRU_Red", "SRU_Blue", "SRU_Gromp",
            "Sru_Crab", "SRU_Krug", "SRU_Razorbeak", "SRU_Murkwolf"
        };

        /// <summary>
        /// Initializes a new instance of the <see cref="Champion"/> class.
        /// </summary>
        protected Champion()
        {
            Logger = new ConsoleLogger();

            LocalHero = ObjectManager.GetLocalPlayer();
            Orbwalker = new Orbwalker();
            Krywalker = new Krywalk();
            Menu = new Menu(null, null, true);

            Game.OnStart += OnGameOnStart;
            Game.OnUpdate += OnGameOnUpdate;
            Game.OnEnd += OnGameOnEnd;
            GameObject.OnCreate += OnGameObjectOnCreate;
            Orbwalker.PostAttack += OnOrbwalkerOnPostAttack;
            Render.OnPresent += OnRenderOnPresent;
        }
    }
}