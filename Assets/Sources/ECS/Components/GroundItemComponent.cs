using System;
using System.Collections.Generic;
using Entitas;


namespace Game
{
    public enum GroundItemState
    {
        None,
        Normal,
        Revive,
    }

    public class GroundItemComponent : IComponent
    {
        public int itemId;
        public GroundItemState state;
        public void Reset()
        {
            state = GroundItemState.None;
            itemId = 0;
        }
    }
}
