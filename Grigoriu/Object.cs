﻿using OpenTK;
using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;

namespace Grigoriu
{
    class Object
    {
        private bool visibility;
        private Color color ;
        private readonly Randomizer rando;
        private List<Vector3> coordsList;
        private bool hasError;
        private const String FILENAME = "Coo/Coordonate.txt";
        private const int FACTOR_SCALARE_IMPORT = 4;

        public Object(Color col)
        {


            try
            {
                coordsList = LoadFromObjFile(FILENAME);

                if (coordsList.Count == 0)
                {
                    Console.WriteLine("Crearea obiectului a esuat: obiect negasit/coordonate lipsa!");
                    return;
                }
                visibility = true;
                color = col;
                hasError = false;
                Console.WriteLine(" \n Obiect 3D încarcat - " + coordsList.Count.ToString() + " vertexuri disponibile!");
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR: assets file <" + FILENAME + "> is missing!!!");
                hasError = true;
            }
        }
        public void ToggleVisibility()
        {            
                visibility = !visibility;            
        }
        public void ChColor()
        {
            color = rando.GetRandomColor();
            
        }
        public void DrawObj()
        {
            if (hasError == false && visibility == true)
            {
                
                GL.Begin(PrimitiveType.Quads);
                foreach (var vert in coordsList)
                {
                    GL.Color3(color);
                    GL.Vertex3(vert);
                
                }
                GL.End();
            }
        }

    
        private List<Vector3> LoadFromObjFile(string fname)
        {
            List<Vector3> vlc3 = new List<Vector3>();


            var lines = File.ReadLines(fname);
            foreach (var line in lines)
            {
                if (line.Trim().Length > 2)
                {
                    string ch1 = line.Trim().Substring(0, 1);
                    string ch2 = line.Trim().Substring(1, 1);
                    if (ch1 == "v" && ch2 == " ")
                    {

                        string[] block = line.Trim().Split(' ');
                        if (block.Length == 4)
                        {
                            // ATENTIE: Pericol!!!
                            float xval = float.Parse(block[1].Trim()) * FACTOR_SCALARE_IMPORT;
                            float yval = float.Parse(block[2].Trim()) * FACTOR_SCALARE_IMPORT;
                            float zval = float.Parse(block[3].Trim()) * FACTOR_SCALARE_IMPORT;

                            vlc3.Add(new Vector3((int)xval, (int)yval, (int)zval));

                        }
                    }
                }
            }

            return vlc3;
        }
    }
}
