// ***********************************************************************
// Assembly         : KryAIO
// Author           : kryptodev
// Created          : 07-26-2017
//
// Last Modified By : kryptodev
// Last Modified On : 07-26-2017
// ***********************************************************************
// <copyright file="Krywalk.cs" company="kryptodev.com">
//     Copyright © Kryptodev 2017
// </copyright>
// <summary>Simple Orbwalker | Credits: Manciuszz and eXtragoZ</summary>
// ***********************************************************************

using Aimtec;
using Aimtec.SDK.Extensions;
using System;

namespace KryAIO
{
    /// <summary>
    /// Class Krywalk.
    /// </summary>
    public class Krywalk
    {
        /// <summary>
        /// The last wind up time
        /// </summary>
        private float _lastWindUpTime;

        /// <summary>
        /// The last attack cd
        /// </summary>
        private float _lastAttackCooldown;

        /// <summary>
        /// The last attack
        /// </summary>
        private float _lastAttack;

        /// <summary>
        /// Gets the hero true range.
        /// </summary>
        /// <value>The hero true range.</value>
        public float HeroTrueRange { get; }

        /// <summary>
        /// The local hero
        /// </summary>
        private readonly Obj_AI_Hero _localHero;

        /// <summary>
        /// Initializes a new instance of the <see cref="Krywalk"/> class.
        /// </summary>
        public Krywalk()
        {
            _localHero = ObjectManager.GetLocalPlayer();
            HeroTrueRange = _localHero.BoundingRadius + _localHero.AttackRange;

            _lastWindUpTime = 0;
            _lastAttackCooldown = 0;
            _lastAttack = Game.TickCount;

            Obj_AI_Base.OnProcessAutoAttack += ObjAiBaseOnProcessAutoAttack;
            Obj_AI_Base.OnProcessSpellCast += OnObjAiBaseOnProcessSpellCast;
        }

        private void OnObjAiBaseOnProcessSpellCast(GameObject sender, Obj_AI_BaseMissileClientDataEventArgs args)
        {
            if (_localHero.IsDead) return;
            if (!sender.IsMe) return;

            if (args.SpellSlot == SpellSlot.W)
            {
                _lastAttack = Game.TickCount - Game.Ping / 2 - _lastAttackCooldown;
            }
        }

        private void ObjAiBaseOnProcessAutoAttack(GameObject sender,
            Obj_AI_BaseMissileClientDataEventArgs objAiBaseMissileClientDataEventArgs)
        {
            if (_localHero.IsDead) return;
            if (!sender.IsMe) return;

            _lastAttack = Game.TickCount - Game.Ping / 2;
            _lastWindUpTime = _localHero.AttackDelay * 1000;
            _lastAttackCooldown = _localHero.AttackCastDelay * 1000;
        }

        /// <summary>
        /// Orbwalks the specified target hero.
        /// </summary>
        /// <param name="targetHero">The target hero.</param>
        public void Orbwalk(GameObject targetHero)
        {
            if (targetHero == null)
            {
                Console.WriteLine("Target is null but called");
                return;
            }

            Console.WriteLine("HeroCanMove. {0} > {1} {2}-{3}", Game.TickCount + Game.Ping / 2, _lastAttack + _lastWindUpTime + 90,
                _lastAttack, _lastWindUpTime);
            Console.WriteLine("TimeToShoot {0} > {1} {2}-{3}", Game.TickCount + Game.Ping / 2 , _lastAttack + _lastAttackCooldown,
                _lastAttack, _lastAttackCooldown);


            if (TimeToShoot())
            {
                _localHero.IssueOrder(OrderType.AttackUnit, targetHero);
            }
            else if (HeroCanMove())
            {
                MoveToCursor();
            }
        }

        /// <summary>
        /// Moves to cursor.
        /// </summary>
        private void MoveToCursor()
        {
            var moveToPos = _localHero.Position + (Game.CursorPos - _localHero.Position).Normalized() * 300;
            _localHero.IssueOrder(OrderType.MoveTo, moveToPos);
        }

        /// <summary>
        /// Heroes the can move.
        /// </summary>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        private bool HeroCanMove()
        {
            return Game.TickCount + Game.Ping / 2 > _lastAttack + _lastWindUpTime + 90;
        }

        /// <summary>
        /// Times to shoot.
        /// </summary>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        private bool TimeToShoot()
        {
            return Game.TickCount + Game.Ping / 2 > _lastAttack + _lastWindUpTime;
        }
    }
}