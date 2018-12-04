using Entitas;
using Google.Protobuf;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public enum UpdateViewRadius
    {
        None,
        Alway,
        Once,
    }
    class CameraViewSystem : IExecuteSystem, IInitializeSystem, ITearDownSystem
    {
        public void Initialize()
        {

        }
        
        public void TearDown()
        {
        }

        public void Execute()
        {
            RefreshViewRadius();
        }

        void RefreshViewRadius()
        {

        }
    }
}
