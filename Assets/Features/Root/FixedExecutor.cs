using System;
using System.Collections.Generic;
using DefaultNamespace;
using NaughtyAttributes;
using UnityEngine;

[Serializable]
public class FixedExecutor
{
    [ShowNonSerializedField]
    public List<IFixedExecute> Executables =  new List<IFixedExecute>();
    
    public void AddExecutable(IFixedExecute executable)
    {
        Executables.Add(executable);
    }
    
    public void RemoveExecutable(IFixedExecute executable)
    {
        Executables.Remove(executable);
    }
    
    public void Execute()
    {
#if UNITY_EDITOR
        if (!Application.isPlaying) return;
#endif
        
        if(Executables == null || Executables.Count == 0) return;
        for (var i = Executables.Count-1; i >= 0; i--)
        {
            var executable = Executables[i];
            executable.FixedExecute();
        }
    }
}