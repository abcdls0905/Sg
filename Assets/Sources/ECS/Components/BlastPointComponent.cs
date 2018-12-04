using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entitas;

namespace Game
{
    public enum BlastState
    {
        BlastClose,
        BlastOpen,
        BlastLock,
    }
    public class BlastPointComponent : IComponent
    {
        public int index;
        public int holdTeamId;
        public BlastState state;
        public void Reset()
        {
            index = 0;
            holdTeamId = -1;
            state = BlastState.BlastClose;
        }
    }
}
