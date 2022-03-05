// -----------------------------------------------------------------------
// <copyright file="Suicide.cs" company="Build">
// Copyright (c) Build. All rights reserved.
// Licensed under the CC BY-SA 3.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace AssistedSuicide.Commands
{
    using System;
    using CommandSystem;
    using Exiled.API.Features;

    /// <inheritdoc />
    public class Suicide : ICommand
    {
        /// <inheritdoc />
        public string Command { get; set; } = "suicide";

        /// <inheritdoc />
        public string[] Aliases { get; set; } = null;

        /// <inheritdoc />
        public string Description { get; set; } = "Allows a player to kill themselves on command";

        /// <summary>
        /// Gets or sets the displayed reason when a player is killed.
        /// </summary>
        public string DeathReason { get; set; } = "Assisted Suicide";

        /// <summary>
        /// Gets or sets the response to send to a player that attempts to execute the command as a spectator.
        /// </summary>
        public string AlreadySpectator { get; set; } = "You are already dead.";

        /// <summary>
        /// Gets or sets the response to send to a player that successfully executes the command.
        /// </summary>
        public string Success { get; set; } = "Done.";

        /// <inheritdoc />
        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            Player player = Player.Get(sender);
            if (player is null)
            {
                response = "This command must be executed at the game level.";
                return false;
            }

            if (player.IsDead)
            {
                response = AlreadySpectator;
                return false;
            }

            player.Kill(DeathReason);
            response = Success;
            return true;
        }
    }
}