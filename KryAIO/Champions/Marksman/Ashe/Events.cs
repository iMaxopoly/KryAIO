// ***********************************************************************
// Assembly         : KryAIO
// Author           : kryptodev
// Created          : 07-28-2017
//
// Last Modified By : kryptodev
// Last Modified On : 07-28-2017
// ***********************************************************************
// <copyright file="Events.cs" company="kryptodev.com">
//     Copyright © Kryptodev 2017
// </copyright>
// <summary></summary>
// ***********************************************************************

using Aimtec;
using Aimtec.SDK.Orbwalking;
using Aimtec.SDK.TargetSelector;
using System;
using System.Drawing;
using System.Threading.Tasks;
using Aimtec.SDK.Util.Cache;
using KryAIO.Logger;

namespace KryAIO.Champions.Marksman.Ashe
{
    /// <summary>
    /// Class Ashe.
    /// </summary>
    /// <seealso cref="KryAIO.Champions.Champion.Champion" />
    /// <seealso cref="KryAIO.IChampion" />
    partial class Ashe
    {
        /// <summary>
        /// Initializes the events.
        /// </summary>
        public void InitializeEvents()
        {
            GameOnUpdateHandlerDelegate += OnGameOnUpdateHandler;

            GameObjectOnCreateHandlerDelegate += OnGameObjectOnCreateHandler;

            OrbwalkerPostAttackHandlerDelegate += OnOrbwalkerPostAttackHandler;

            RenderOnPresentHandlerDelegate += OnRenderOnPresentHandler;
        }

        /// <summary>
        /// Called when [game on update handler].
        /// </summary>
        private void OnGameOnUpdateHandler()
        {
            var processFarmMechanicsTask = Task.Factory.StartNew(ProcessFarmMechanics);

            var processInstagibMechanicsTask = Task.Factory.StartNew(ProcessInstagibMechanics);

            var processTrickTrapMechanicsTask = Task.Factory.StartNew(ProcessTrickTrapMechanics);

            var processKeyPressMechanicsTask = Task.Factory.StartNew(() =>
            {
                var target = TargetSelector.GetTarget(LocalHeroTrueRange);
                if (target == null) return;

                Logger.Log($"Network ID:{target.NetworkId}| Name: {target.Name} ", LogType.Log, EventType.OnGameOnUpdateEvent);
                switch (Orbwalker.Mode)
                {
                    default:
                        throw new ArgumentOutOfRangeException($"Unknown {Orbwalker.Mode}");

                    case OrbwalkingMode.Combo:
                    case OrbwalkingMode.Custom:
                        ProcessComboMechanics(target);
                        break;

                    case OrbwalkingMode.Freeze:
                    case OrbwalkingMode.Lasthit:
                    case OrbwalkingMode.Mixed:
                        ProcessHarassMechanics(target);
                        break;

                    case OrbwalkingMode.Laneclear:
                        break;

                    case OrbwalkingMode.None:
                        break;
                }
            });

            processInstagibMechanicsTask.Wait();
            processTrickTrapMechanicsTask.Wait();
            processKeyPressMechanicsTask.Wait();
            processFarmMechanicsTask.Wait();
        }

        /// <summary>
        /// Called when [game object on create handler].
        /// </summary>
        /// <param name="sender">The sender.</param>
        private void OnGameObjectOnCreateHandler(GameObject sender)
        {
            // Safely cast a GameObject to MissileClient.
            var missileClient = sender as MissileClient;

            // If it's not of type MissileClient we don't care.
            if (missileClient == null)
                return;

            // If you need to check data of a spell that you didn't cast remove this conditional or inverse it.
            if (!missileClient.SpellCaster.IsMe)
                return;

            // Use any avaialble logging option you have.
            Console.WriteLine(missileClient.SpellData.Name);
        }

        /// <summary>
        /// Handles the <see cref="E:OrbwalkerPostAttackHandler" /> event.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="args">The <see cref="OrbwalkingEventArgs"/> instance containing the event data.</param>
        private void OnOrbwalkerPostAttackHandler(object sender, OrbwalkingEventArgs args)
        {
            var jungleTarget = args.Target as Obj_AI_Minion;
            if (jungleTarget == null || jungleTarget.Team != GameObjectTeam.Neutral ||
                jungleTarget.Name == "WardCorpse" || jungleTarget.Name == "Barrel" ||
                jungleTarget.Name.Contains("SRU_Plant_")) return;

            CastSpellW(jungleTarget, SpellPriority.Combo);
            CastSpellQ(jungleTarget, SpellPriority.Combo);
        }

        /// <summary>
        /// Called when [render on present handler].
        /// </summary>
        private void OnRenderOnPresentHandler()
        {
            if (WSpell.Ready && Menu["Drawings"]["DrawW"].Enabled)
            {
                Render.Circle(LocalHero.Position, WSpell.Range, 30, Color.Aqua);
            }
        }
    }
}