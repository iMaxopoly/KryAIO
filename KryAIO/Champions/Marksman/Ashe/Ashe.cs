// ***********************************************************************
// Assembly         : KryAIO
// Author           : kryptodev
// Created          : 07-21-2017
//
// Last Modified By : kryptodev
// Last Modified On : 07-22-2017
// ***********************************************************************
// <copyright file="Ashe.cs" company="kryptodev.com">
//     Copyright © Kryptodev 2017
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace KryAIO.Champions.Marksman.Ashe
{
    /// <summary>
    /// Class Ashe.
    /// </summary>
    /// <seealso cref="Champion" />
    /// <seealso cref="KryAIO.IChampion" />
    public sealed partial class Ashe : Champion.Champion, IChampion
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Marksman.Ashe.Ashe" /> class.
        /// </summary>
        /// <param name="projectMeta">The project meta.</param>
        public Ashe(string projectMeta)
        {
            // Sets Up Project Name
            Menu.InternalName = projectMeta;
            Menu.DisplayName = projectMeta;
        }
    }
}