using Entitas;
using Pb;
using Game;
using UnityEngine;
using System;
using System.Collections.Generic;
using Google.Protobuf.Collections;

namespace Game
{
    // 具体处理网络消息
    public class FrameSystem : IExecuteSystem, IFixedCleanupSystem, IFixedExecuteSystem
    {
        public FrameSystem()
        {
        }
        public void ExecuteCommand()
        {
            FrameComponent frameComp = Contexts.Instance.game.frame;
            FramePackage framePkg;
            if (!frameComp.frameDic.TryGetValue(frameComp.frameIndex, out framePkg))
                return;
            for (int i = 0; i < framePkg.commands.Count; ++i)
            {
                var command = framePkg.commands[i];
                command.ExecCommand();
            }
            frameComp.frameDic.Remove(frameComp.frameIndex);
        }

        public void FixedExecute()
        {
            FrameComponent frameComp = Contexts.Instance.game.frame;
            frameComp.frameIndex++;
            ExecuteCommand();
        }

        public void Execute()
        {
        }

        public void FixedCleanup()
        {

        }
    }
}