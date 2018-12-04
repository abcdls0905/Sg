using Entitas;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System.Text;
using Entitas.CodeGeneration.Attributes;

namespace Game
{
    [Game, Unique]
    public class HUDComponent : IComponent
    {
        public ReactiveProperty<string> debugInfo = new ReactiveProperty<string>();

        public float freqTime;
        public int fps;
        public int lastfps;
        public int clientFreq;
        public int clientIndex;
        public int serverFreq;
        public int serverIndex;
        public Vector3 serverPos;
        public int lastSendBytes;
        public int lastRecvBytes;
        public float SendRate;
        public float RecvRate;

        public bool openDebug;
        public bool shadowDebug;
        public bool showDamage;

        public void Reset()
        {
            serverPos = Vector3.zero;
            RecvRate = 0;
            SendRate = 0;
            fps = 0;
            lastfps = 0;
            lastSendBytes = 0;
            lastRecvBytes = 0;
            clientIndex = 0;
            serverIndex = 0;
            clientFreq = 0;
            serverFreq = 0;
            freqTime = 0;
            shadowDebug = false;
            showDamage = true;
        }
    }
}