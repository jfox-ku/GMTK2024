using System;
using System.Collections.Generic;
using DefaultNamespace;
using NaughtyAttributes;
using Object = UnityEngine.Object;

[Serializable]
public class Initializer
{
    public List<IInit> Initializables = new List<IInit>();
    
    [ShowNativeProperty]
    public List<Object> Inits => Initializables.ConvertAll(x => x as Object);

    public void AddInitializable(IInit initializable)
    {
        Initializables.Add(initializable);
    }
    
    public void RemoveInitializable(IInit initializable)
    {
        Initializables.Remove(initializable);
    }
    
    public void Init()
    {
        foreach (var obj in Initializables)
        {
            if (obj is IInit init)
            {
                init.Init();
            }
            else
            {
                throw new Exception("Object must implement IInit interface to be initialized");
            }
        }
    }
}