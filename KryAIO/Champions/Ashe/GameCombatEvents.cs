// ***********************************************************************
// Assembly         : KryAIO
// Author           : kryptodev
// Created          : 07-22-2017
//
// Last Modified By : kryptodev
// Last Modified On : 07-22-2017
// ***********************************************************************
// <copyright file="GameCombatEvents.cs" company="kryptodev.com">
//     Copyright © Kryptodev 2017
// </copyright>
// <summary></summary>
// ***********************************************************************

using Aimtec;
using Aimtec.SDK.Extensions;
using Aimtec.SDK.Orbwalking;
using System;

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
        /// Initializes the events.
        /// </summary>
        private void InitializeEvents()
        {
            Game.OnStart += OnGameOnStart;
            Game.OnUpdate += OnGameOnUpdate;
            Game.OnEnd += OnGameOnEnd;

            Render.OnPresent += OnPresent;
        }

        /// <summary>
        /// Called when [game on start].
        /// </summary>
        protected virtual void OnGameOnStart()
        {
            Console.WriteLine("HelloWorld");
        }

        /// <summary>
        /// Called when [game on update].
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        protected virtual void OnGameOnUpdate()
        {
            if (LocalHero.IsDead || LocalHero.IsRecalling()) return;

            ProcessInstagibMechanics().Wait();

            switch (Orbwalker.Mode)
            {
                default:
                    throw new ArgumentOutOfRangeException(Orbwalker.Mode.ToString());

                case OrbwalkingMode.None:
                    break;

                case OrbwalkingMode.Combo:
                    ProcessComboMechanics();
                    break;

                case OrbwalkingMode.Mixed:
                    ProcessHarassMechanics();
                    break;

                case OrbwalkingMode.Laneclear:
                    break;

                case OrbwalkingMode.Lasthit:
                    break;

                case OrbwalkingMode.Freeze:
                    break;

                case OrbwalkingMode.Custom:
                    break;
            }
        }

        /// <summary>
        /// Called when [game on end].
        /// </summary>
        /// <param name="team">The team.</param>
        protected virtual void OnGameOnEnd(GameObjectTeam team)
        {
            Console.WriteLine("ByeWorld");
        }
    }
}