using Entitas;
using UnityEngine;

namespace Game
{
    [Game]
    public class AnimationComponent : IComponent
    {
        public Animator animator;
        public void Reset()
        {
            animator = null;
        }
    }
}