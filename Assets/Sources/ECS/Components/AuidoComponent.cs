using Entitas;
using Entitas.CodeGeneration.Attributes;
using Game;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    [Game]
    public class AudioComponent : IComponent
    {
        public GameObject gameObject = null;

        public void Reset()
        {
//             if (gameObject != null)
//             {
//                 if (AkSoundEngine.IsGameObjectRegistered(gameObject))
//                     AkSoundEngine.UnregisterGameObj(gameObject);
//                 GameObjectPool.Instance.RecycleGameObject(gameObject);
//                 gameObject = null;
//             }
        }
    }
}