using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.SceneManagement;
[CreateAssetMenu(menuName = "Systems/EditorUtilitySystem")]
public class EditorUtilitySystem : ScriptableObject, IInit, IExecute
{
    public KeyCode ReloadSceneKey;

    public void Init()
    {
        
    }

    public void Execute()
    {
        if (Input.GetKeyDown(ReloadSceneKey))
        {
            SceneManager.LoadScene(0);
        }   
    }
}
