using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;

namespace DefaultNamespace
{
    [Serializable]
    public class GameState
    {
        public string Name = "GameState";
        public virtual IEnumerator Enter()
        {
            yield break;   
        }

        public virtual IEnumerator Exit()
        {
            yield break;   
        }
    }
}