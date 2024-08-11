using System;
using System.Collections.Generic;
using DefaultNamespace;
using NaughtyAttributes;

[Serializable]
public class Cleaner
{
    [ShowNonSerializedField]
    public List<ICleanUp> Cleanables = new List<ICleanUp>();
    
    public void AddCleanable(ICleanUp cleanable)
    {
        Cleanables.Add(cleanable);
    }
    
    public void RemoveCleanable(ICleanUp cleanable)
    {
        Cleanables.Remove(cleanable);
    }
    
    public void CleanUp()
    {
        for (var i = Cleanables.Count-1; i > 0; i--)
        {
            var cleanable = Cleanables[i];
            cleanable.CleanUp();
        }
        
    }
}