using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using NaughtyAttributes;
using UnityEngine;
using Object = UnityEngine.Object;

public class GameRoot : MonoSingleton<GameRoot>
{
    [ValidateInput("ValidateGameSystems", "Game Systems must implement IInit, IExecute or ICleanUp interfaces")]
    [Expandable]
    public List<Object> GameSystems;

#if UNITY_EDITOR
    public List<Object> EditorOnlySystems;
#endif

    private Initializer Initializer;
    private Executor Executor;
    private FixedExecutor FixedExecutor;
    private Cleaner Cleaner;
    
    public static MonoBehaviour CoroutineRunner => Instance;
    
    private void Start()
    {
        SetupSystems();
        Initializer.Init();
    }

    private void SetupSystems()
    {
        Initializer = new Initializer();
        Executor = new Executor();
        FixedExecutor = new FixedExecutor();
        Cleaner = new Cleaner();
        
        ExtractSystemInterfaces(GameSystems);
#if UNITY_EDITOR
        ExtractSystemInterfaces(EditorOnlySystems);
#endif
    }

    private void ExtractSystemInterfaces(IEnumerable<Object> systems)
    {
        foreach (var system in systems)
        {
            if (system is IExecute exec)
            {
                Executor.AddExecutable(exec);
            }

            if (system is IFixedExecute fexec)
            {
                FixedExecutor.AddExecutable(fexec);
            }

            if (system is IInit init)
            {
                Initializer.AddInitializable(init);
            }

            if (system is ICleanUp clean)
            {
                Cleaner.AddCleanable(clean);
            }
        }
    }

    private void Update()
    {
        Executor?.Execute();
    }

    private void FixedUpdate()
    {
        FixedExecutor?.Execute();
    }

    private void OnDisable()
    {
        Cleaner?.CleanUp();
    }

    public T GetSystem<T>() where T : ScriptableObject
    {
        foreach (var gameSystem in GameSystems)
        {
            if(gameSystem is T system)
                return system;
        }

        return null;
    }
    
    //Validation
    private bool ValidateGameSystems(List<Object> obj)
    {
        if(obj == null || obj.Count == 0)
            return true;
        foreach (var system in obj)
        {
            if (system is not (IInit or IExecute or ICleanUp))
            {
                return false;
            }
        }

        return true;
    }

    #if UNITY_EDITOR
    [Button()]
    private void FindAllSystems()
    {
        var systems = new List<Object>();
        var guids = UnityEditor.AssetDatabase.FindAssets($"t:{nameof(ScriptableObject)}");
        foreach (var guid in guids)
        {
            var path = UnityEditor.AssetDatabase.GUIDToAssetPath(guid);
            var obj = UnityEditor.AssetDatabase.LoadAssetAtPath<ScriptableObject>(path);
            if(obj is IInit || obj is IExecute || obj is ICleanUp)
                systems.Add(obj);
        }
    }
    #endif
}