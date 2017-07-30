// ***********************************************************************
// Assembly         : KryAIO
// Author           : kryptodev
// Created          : 07-28-2017
//
// Last Modified By : kryptodev
// Last Modified On : 07-28-2017
// ***********************************************************************
// <copyright file="Delegates.cs" company="kryptodev.com">
//     Copyright © Kryptodev 2017
// </copyright>
// <summary></summary>
// ***********************************************************************

using Aimtec;
using Aimtec.SDK.Orbwalking;

namespace KryAIO.Champions.Champion
{
    /// <summary>
    /// Class Champion.
    /// </summary>
    /// <seealso cref="System.IDisposable" />
    public abstract partial class Champion
    {
        /// <summary>
        /// Delegate GameOnStartHandler
        /// </summary>
        protected delegate void GameOnStartHandler();

        /// <summary>
        /// Delegate GameOnUpdateHandler
        /// </summary>
        protected delegate void GameOnUpdateHandler();

        /// <summary>
        /// Delegate GameOnEndHandler
        /// </summary>
        protected delegate void GameOnEndHandler();

        /// <summary>
        /// Delegate GameObjectOnCreateHandler
        /// </summary>
        /// <param name="sender">The sender.</param>
        protected delegate void GameObjectOnCreateHandler(GameObject sender);

        /// <summary>
        /// Delegate OrbwalkerPostAttackHandler
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="args">The <see cref="OrbwalkingEventArgs" /> instance containing the event data.</param>
        protected delegate void OrbwalkerPostAttackHandler(object sender, OrbwalkingEventArgs args);

        /// <summary>
        /// Delegate RenderOnPresentHandler
        /// </summary>
        protected delegate void RenderOnPresentHandler();

        /// <summary>
        /// The game on start handler delegate
        /// </summary>
        protected GameOnStartHandler GameOnStartHandlerDelegate;

        /// <summary>
        /// The game on update handler delegate
        /// </summary>
        protected GameOnUpdateHandler GameOnUpdateHandlerDelegate;

        /// <summary>
        /// The game on end handler delegate
        /// </summary>
        protected GameOnEndHandler GameOnEndHandlerDelegate;

        /// <summary>
        /// The game object on create handler delegate
        /// </summary>
        protected GameObjectOnCreateHandler GameObjectOnCreateHandlerDelegate;

        /// <summary>
        /// The orbwalker post attack handler delegate
        /// </summary>
        protected OrbwalkerPostAttackHandler OrbwalkerPostAttackHandlerDelegate;

        /// <summary>
        /// The render on present handler delegate
        /// </summary>
        protected RenderOnPresentHandler RenderOnPresentHandlerDelegate;
    }
}