// ***********************************************************************
// Assembly         : KryAIO
// Author           : kryptodev
// Created          : 07-21-2017
//
// Last Modified By : kryptodev
// Last Modified On : 07-22-2017
// ***********************************************************************
// <copyright file="Program.cs" company="kryptodev.com">
//     Copyright © Kryptodev 2017
// </copyright>
// <summary></summary>
// ***********************************************************************

using Aimtec;
using Aimtec.SDK.Events;
using KryAIO.Champions.Marksman.Ashe;
using System;

namespace KryAIO
{
    /// <summary>
    /// Class Program.
    /// </summary>
    internal static class Program
    {
        /// <summary>
        /// The project name
        /// </summary>
        private const string ProjectName = "KryAIO";

        /// <summary>
        /// The project version
        /// </summary>
        private static readonly Version ProjectVersion = new Version(7, 15, 0, 0);

        /// <summary>
        /// Defines the entry point of the application.
        /// </summary>
        private static void Main()
        {
            GameEvents.GameStart += OnGameStart;
        }

        /// <summary>
        /// Called when [game start].
        /// </summary>
        private static void OnGameStart()
        {
            var playerChampionName = ObjectManager.GetLocalPlayer().ChampionName;
            switch (playerChampionName)
            {
                default:
                    Console.WriteLine($"{playerChampionName} is currently not supported");
                    break;

                case "Ashe":
                    try
                    {
                        IChampion championAshe = new Ashe($"{playerChampionName} | {ProjectName} - {ProjectVersion}");
                        championAshe.Bootstrap();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Some exception just got throw: {ex}");
                        Console.WriteLine($"Stacktrace: {ex.StackTrace}");
                    }
                    break;

                case "Annie":
                    break;
            }
        }
    }
}