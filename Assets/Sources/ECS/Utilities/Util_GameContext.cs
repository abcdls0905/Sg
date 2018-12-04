
using System;
using UnityEngine;
using System.Collections.Generic;

namespace Game
{
    public static partial class Util
    {
        public static void PushCommand<T>(ref T command, ulong id = 0) where T : ICommand
        {
            GameContext game = Contexts.Instance.game;
            if (id == 0)
                id = Contexts.Instance.game.gameMaster.entity.iD.value;
            bool single = BattleManager.Instance.isSingle;
            if (single)
            {
                Delegate handler = null;
                if (!game.command.commands.TryGetValue(typeof(T), out handler))
                    return;
                if (handler == null || !(handler is CommandHandler<T>))
                    return;
                FramePackage framePkg = new FramePackage();
                FrameCommand<T> frameCommand = new FrameCommand<T>(handler as CommandHandler<T>, command, id);
                framePkg.commands.Add(frameCommand);
                game.frame.AddLocalCommand(framePkg);
            }
            else
            {
                //send command
            }
        }

        public static void ListenCommand<T>(CommandHandler<T> handler) where T : ICommand
        {
            GameContext game = Contexts.Instance.game;
            Delegate _delegate = null;
            Type t = typeof(T);
            if (!game.command.commands.TryGetValue(typeof(T), out _delegate) || _delegate == null || _delegate is CommandHandler<T>)
                game.command.commands[typeof(T)] = Delegate.Combine(_delegate, handler);
        }
    }
}