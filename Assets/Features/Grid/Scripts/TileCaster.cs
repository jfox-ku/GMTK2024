using UnityEngine;

namespace Features.Grid
{
    public class TileCaster
    {
        private Camera cam;
        
        public TileCaster()
        {
            cam = Camera.main;
        }
        
        public Tile Cast()
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, 100f, LayerMask.GetMask("TileLayer")))
            {
                Debug.Log(hit.point);
                var hitObject = hit.collider.gameObject;
                Debug.Log("Hit: " + hitObject.name);
                    
                Tile tile = hitObject.GetComponent<Tile>();
                if (tile != null)
                {
                    return tile;
                }
            }
            return null;
        }
    }
}