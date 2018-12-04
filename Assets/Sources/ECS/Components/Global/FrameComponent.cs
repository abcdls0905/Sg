using Entitas;
using Entitas.CodeGeneration.Attributes;
using System.Collections.Generic;
using UnityEngine;
namespace Game
{
    [Game]
    [Unique]
    public class FrameComponent : IComponent
    {
        public const int FRAME_DELAY_COUNT = 20;
        public const int DEVIATION_COUNT = 10;

        public int frameRate = GameEnvSetting.frameRate;
        public float frameTime = GameEnvSetting.frameTime;
        public float minFrameTime = 0.035f;
        public float maxFrameTime = 0.1f;

        public List<Pb.B2C_Frame> recvFrames = new List<Pb.B2C_Frame>();
        public List<Pb.C2B_PlayerCommand> sendCommands = new List<Pb.C2B_PlayerCommand>();
        public Dictionary<ulong, uint> targetFrame = new Dictionary<ulong, uint>();

        //todo
        public float gameUpdateTime;
        public float lerpTime;

        public int timeScale;
        public float lerpPercent;
        public int frameIndex;
        public int serverFrameIndex;
        public int startFrameTime;
        private int curFrameDelay;
        private int avgFrameDelay;

        public Dictionary<int, FramePackage> frameDic = new Dictionary<int, FramePackage>();
        public Dictionary<int, bool> hasFrameDic = new Dictionary<int, bool>();

        public void Reset()
        {
            recvFrames.Clear();
            sendCommands.Clear();
            targetFrame.Clear();
            lerpPercent = 0;
            frameIndex = 1;
            serverFrameIndex = 1;
            startFrameTime = 0;
            curFrameDelay = 0;
            avgFrameDelay = 0;
            timeScale = 1;
            startFrameTime = (int)(Util.GetRealTime() * 1000);

            frameDic.Clear();
            hasFrameDic.Clear();
        }

        public void AddFrame(int frameCnt, FramePackage frame)
        {
            if (frame.commands.Count > 0)
                frameDic.Add(frameCnt, frame);

            hasFrameDic[frameCnt] = true;
            while (hasFrameDic.ContainsKey(serverFrameIndex))
                serverFrameIndex++;
        }

        public float GetTickTime()
        {
            return frameTime / timeScale;
        }

        void SetFrameStartTime()
        {
            startFrameTime = (int)(Util.GetRealTime() * 1000);
        }

        public void UpdateServerFrame()
        {
            serverFrameIndex = frameIndex + 1;
            hasFrameDic[serverFrameIndex] = true;
        }

        public int CalculateJitterDelay(int delayTime)
        {
            curFrameDelay = delayTime <= 0 ? 0 : delayTime;
            if (avgFrameDelay <= 0)
                avgFrameDelay = curFrameDelay;
            else
                avgFrameDelay = (29 * avgFrameDelay + curFrameDelay) / 30;
            return avgFrameDelay;
        }

        public void AddLocalCommand(FramePackage frameData)
        {
            if (frameData.commands.Count == 0)
                return;
            int nextFrame = frameIndex + 1;
            FramePackage frame;
            if (!frameDic.TryGetValue(nextFrame, out frame))
            {
                frameDic.Add(nextFrame, frameData);
                return;
            }
            frame.commands.AddRange(frameData.commands);
        }

        public int GetSvrTime()
        {
            return (int)(frameIndex * frameTime * 1000);
        }
    }
}