// ***********************************************************************
// Assembly         : KryAIO
// Author           : kryptodev
// Created          : 07-21-2017
//
// Last Modified By : kryptodev
// Last Modified On : 07-22-2017
// ***********************************************************************
// <copyright file="ChampionSpells.cs" company="kryptodev.com">
//     Copyright © Kryptodev 2017
// </copyright>
// <summary></summary>
// ***********************************************************************

using Aimtec;
using Aimtec.SDK.Prediction.Skillshots;
using Spell = Aimtec.SDK.Spell;

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
        /// The spell q
        /// </summary>
        private Spell _spellQ;

        /// <summary>
        /// The spell w
        /// </summary>
        private Spell _spellW;

        /// <summary>
        /// The spell e
        /// </summary>
        private Spell _spellE;

        /// <summary>
        /// The spell r
        /// </summary>
        private Spell _spellR;

        /// <summary>
        /// Initializes the spells.
        /// </summary>
        private void InitializeSpells()
        {
            _spellQ = new Spell(SpellSlot.Q);
            _spellW = new Spell(SpellSlot.W, LocalHero.BoundingRadius + 1200F);
            _spellE = new Spell(SpellSlot.E, 2000F);
            _spellR = new Spell(SpellSlot.R, 2000F);

            _spellW.SetSkillshot(0.25f, 1.125F, 1500F, true, SkillshotType.Cone, false, HitChance.Low);
            _spellE.SetSkillshot(0.25f, 130f, 1600f, false, SkillshotType.Line);
            _spellR.SetSkillshot(0.25f, 130f, 1600f, false, SkillshotType.Line, false, HitChance.VeryHigh);
        }
    }
}