using Entitas;
using Entitas.CodeGeneration.Attributes;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

namespace Game
{
    public interface ICommand { }

    public delegate void CommandHandler<T>(ref T command, ulong id) where T : ICommand;

    public interface IFrameCommand
    {
        void ExecCommand();
    }

    public struct FrameCommand<T> : IFrameCommand where T : ICommand
    {
        public ulong entityId;
        public T command;
        public CommandHandler<T> callback;
        public FrameCommand(CommandHandler<T> _call, T _prm, ulong id = 0)
        {
            entityId = id;
            this.command = _prm;
            this.callback = _call;
        }
        public void ExecCommand()
        {
            if (this.callback != null)
            {
                this.callback(ref this.command, entityId);
            }
        }
    }

    public class FramePackage
    {
        public List<IFrameCommand> commands = new List<IFrameCommand>();
    }
    public struct MoveCommand : ICommand
    {
        public AkTurnDir eTurnDir;
        public bool isMoving;
    }

    public struct RotateCommand : ICommand
    {
        public AkTurnDir eTurnDir;
    }


    public struct AnimCommand : ICommand
    {
        public string name;
    }

    public struct RestartCommand : ICommand
    {

    }

    [Game]
    [Unique]
    public class CommandComponent : IComponent
    {
        public Dictionary<Type, Delegate> commands = new Dictionary<Type, Delegate>();
        public List<IFrameCommand> postCommands = new List<IFrameCommand>();

        public void Reset()
        {
            commands.Clear();
            postCommands.Clear();
        }
    }
}