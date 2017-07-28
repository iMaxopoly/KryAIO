// ***********************************************************************
// Assembly         : KryAIO
// Author           : kryptodev
// Created          : 07-28-2017
//
// Last Modified By : kryptodev
// Last Modified On : 07-28-2017
// ***********************************************************************
// <copyright file="EventType.cs" company="kryptodev.com">
//     Copyright © Kryptodev 2017
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace KryAIO.Logger
{
    /// <summary>
    /// Enum EventType
    /// </summary>
    public enum EventType
    {
        /// <summary>
        /// The on game on start event
        /// </summary>
        OnGameOnStartEvent,

        /// <summary>
        /// The on game on update event
        /// </summary>
        OnGameOnUpdateEvent,

        /// <summary>
        /// The on game on end event
        /// </summary>
        OnGameOnEndEvent,

        /// <summary>
        /// The on game object on create event
        /// </summary>
        OnGameObjectOnCreateEvent,

        /// <summary>
        /// The on game object on destroy event
        /// </summary>
        OnGameObjectOnDestroyEvent,

        /// <summary>
        /// The on orbwalker on pre attack event
        /// </summary>
        OnOrbwalkerOnPreAttackEvent,

        /// <summary>
        /// The on orbwalker on post attack event
        /// </summary>
        OnOrbwalkerOnPostAttackEvent,

        /// <summary>
        /// The on render on present event
        /// </summary>
        OnRenderOnPresentEvent
    }
}