using UnityEngine;

namespace Features.BoardUnit
{
    public class BoardUnitAsset : ScriptableObject
    {
        [SerializeField]
        private GameObject Prefab;
        
        public GameObject CreateInScene()
        { 
            return Instantiate(Prefab);
        }
    }
}