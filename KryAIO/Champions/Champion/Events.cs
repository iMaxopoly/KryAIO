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
using Aimtec.SDK.Extensions;
using Aimtec.SDK.Orbwalking;
using KryAIO.Logger;

namespace KryAIO.Champions.Champion
{
    /// <summary>
    /// Class Champion.
    /// </summary>
    /// <seealso cref="System.IDisposable" />
    public abstract partial class Champion
    {
        /// <summary>
        /// Called when [game on start].
        /// </summary>
        private void OnGameOnStart()
        {
            Logger.Log("Init", LogType.Log, EventType.OnGameOnStartEvent);
            GameOnStartHandlerDelegate();
        }

        /// <summary>
        /// Called when [game on update].
        /// </summary>
        private void OnGameOnUpdate()
        {
            if (LocalHero.IsDead || LocalHero.HasBuff("recall")) return;

            GameOnUpdateHandlerDelegate();
        }

        /// <summary>
        /// Called when [game on end].
        /// </summary>
        /// <param name="team">The team.</param>
        private void OnGameOnEnd(GameObjectTeam team)
        {
            Logger.Log("Init", LogType.Log, EventType.OnGameOnEndEvent);
            GameOnEndHandlerDelegate();
        }

        /// <summary>
        /// Called when [game object on create].
        /// </summary>
        /// <param name="sender">The sender.</param>
        private void OnGameObjectOnCreate(GameObject sender)
        {
            GameObjectOnCreateHandlerDelegate(sender);
        }

        /// <summary>
        /// Handles the <see cref="E:OrbwalkerOnPostAttack" /> event.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="OrbwalkingEventArgs"/> instance containing the event data.</param>
        private void OnOrbwalkerOnPostAttack(object sender, OrbwalkingEventArgs e)
        {
            OrbwalkerPostAttackHandlerDelegate(sender, e);
        }

        /// <summary>
        /// Called when [render on present].
        /// </summary>
        private void OnRenderOnPresent()
        {
            if (LocalHero.IsDead) return;

            RenderOnPresentHandlerDelegate();
        }
    }
}