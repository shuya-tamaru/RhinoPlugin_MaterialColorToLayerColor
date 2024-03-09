using Rhino;
using Rhino.Commands;
using Rhino.Geometry;
using Rhino.Input;
using Rhino.Input.Custom;
using System;
using System.Collections.Generic;

namespace MaterialColorToLayerColor
{
    public class MaterialColorToLayerColorCommand : Command
    {
        public MaterialColorToLayerColorCommand()
        {
            Instance = this;
        }

        public static MaterialColorToLayerColorCommand Instance { get; private set; }

        public override string EnglishName => "MaterialColorToLayerColor";


        protected override Result RunCommand(RhinoDoc doc, RunMode mode)
        {
            var layers = doc.Layers;
            foreach (var layer in layers)
            {
                int materialIndex = layer.RenderMaterialIndex;
                if (materialIndex != -1)
                {
                    var material = doc.Materials[materialIndex];
                    if (material != null)
                    {
                        var materialColor = material.DiffuseColor;
                        layer.Color = materialColor;
                        RhinoApp.WriteLine("レイヤー '{0}' の色を {1} に設定しました。", layer.Name, materialColor.ToString());
                    }
                }
                else
                {
                    RhinoApp.WriteLine("レイヤー '{0}' にはマテリアルが設定されていません。", layer.Name);
                }
            }
            doc.Views.Redraw();
            return Result.Success;
        }
    }
}
