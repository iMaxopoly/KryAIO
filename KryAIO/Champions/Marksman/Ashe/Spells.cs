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
        /// Initializes the spells.
        /// </summary>
        private void InitializeSpells()
        {
            QSpell = new Spell(SpellSlot.Q);
            WSpell = new Spell(SpellSlot.W, LocalHero.BoundingRadius + 1200F);
            ESpell = new Spell(SpellSlot.E, 2000F);
            RSpell = new Spell(SpellSlot.R, 2000F);

            WSpell.SetSkillshot(0.25f, 1.125F, 1500F, true, SkillshotType.Cone, false, HitChance.Medium);
            ESpell.SetSkillshot(0.25f, 130f, 1600f, false, SkillshotType.Line);
            RSpell.SetSkillshot(0.25f, 130f, 1600f, false, SkillshotType.Line);
        }
    }
}