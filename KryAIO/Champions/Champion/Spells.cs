// ***********************************************************************
// Assembly         : KryAIO
// Author           : kryptodev
// Created          : 07-28-2017
//
// Last Modified By : kryptodev
// Last Modified On : 07-31-2017
// ***********************************************************************
// <copyright file="Spells.cs" company="kryptodev.com">
//     Copyright © Kryptodev 2017
// </copyright>
// <summary></summary>
// ***********************************************************************

using Aimtec.SDK;

namespace KryAIO.Champions.Champion
{
    /// <summary>
    /// Class Champion.
    /// </summary>
    /// <seealso cref="System.IDisposable" />
    public abstract partial class Champion
    {
        /// <summary>
        /// Enum SpellPriority
        /// </summary>
        protected enum SpellPriority
        {
            /// <summary>
            /// The harass priority
            /// </summary>
            Harass,

            /// <summary>
            /// The farm priority
            /// </summary>
            Farm,
            /// <summary>
            /// The combo priority
            /// </summary>
            Combo,
            /// <summary>
            /// The force priority
            /// </summary>
            Force
        }

        /// <summary>
        /// The q spell
        /// </summary>
        protected Spell QSpell;

        /// <summary>
        /// The w spell
        /// </summary>
        protected Spell WSpell;

        /// <summary>
        /// The e spell
        /// </summary>
        protected Spell ESpell;

        /// <summary>
        /// The r spell
        /// </summary>
        protected Spell RSpell;
    }
}