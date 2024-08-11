using System;
using UnityEngine;

namespace Features.Grid
{
    public class TileColorComponent : TileComponent
    {
        public MeshRenderer Renderer;
        public bool SetOnStart;
        public Color Color;

        private void Start()
        {
            if(SetOnStart) SetColor(Color);
        }

        public void SetColor(Color color)
        {
            Color = color;
            Renderer.material.color = color;
        }
    }
}