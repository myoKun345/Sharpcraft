using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

using OpenTK;

namespace MyoKun.SharpCraft.Core
{
    public abstract class Primitive
    {
        private Vector3[] vertices, normals;
        private Vector2[] texcoords;
        private int[] indices;
        private int[] colors;

        public Vector3[] Vertices
        {
            get { return vertices; }
            set
            {
                vertices = value;
            }
        }

        public Vector3[] Normals
        {
            get { return normals; }
            set
            {
                normals = value;
            }
        }

        public Vector2[] TexCoords
        {
            get { return texcoords; }
            set
            {
                texcoords = value;
            }
        }

        public int[] Indices
        {
            get { return indices; }
            set
            {
                indices = value;
            }
        }

        public int[] Colors
        {
            get { return colors; }
            set
            {
                colors = value;
            }
        }
    }

    public class Cube : Primitive
    {
        public Cube()
        {
            Vertices = new Vector3[]
            {
                new Vector3(-1.0f, -1.0f,  1.0f),
                new Vector3( 1.0f, -1.0f,  1.0f),
                new Vector3( 1.0f,  1.0f,  1.0f),
                new Vector3(-1.0f,  1.0f,  1.0f),
                new Vector3(-1.0f, -1.0f, -1.0f),
                new Vector3( 1.0f, -1.0f, -1.0f), 
                new Vector3( 1.0f,  1.0f, -1.0f),
                new Vector3(-1.0f,  1.0f, -1.0f)
            };

            Indices = new int[]
            {
                // front face
                0, 1, 2, 2, 3, 0,
                // top face
                3, 2, 6, 6, 7, 3,
                // back face
                7, 6, 5, 5, 4, 7,
                // left face
                4, 0, 3, 3, 7, 4,
                // bottom face
                0, 1, 5, 5, 4, 0,
                // right face
                1, 5, 6, 6, 2, 1,
            };

            Normals = new Vector3[]
            {
                new Vector3(-1.0f, -1.0f,  1.0f),
                new Vector3( 1.0f, -1.0f,  1.0f),
                new Vector3( 1.0f,  1.0f,  1.0f),
                new Vector3(-1.0f,  1.0f,  1.0f),
                new Vector3(-1.0f, -1.0f, -1.0f),
                new Vector3( 1.0f, -1.0f, -1.0f),
                new Vector3( 1.0f,  1.0f, -1.0f),
                new Vector3(-1.0f,  1.0f, -1.0f),
            };

            Colors = new int[]
            {
                Utilities.ColorToRgba32(Color.Black),
                Utilities.ColorToRgba32(Color.Black),
                Utilities.ColorToRgba32(Color.White),
                Utilities.ColorToRgba32(Color.White),
                Utilities.ColorToRgba32(Color.Black),
                Utilities.ColorToRgba32(Color.Black),
                Utilities.ColorToRgba32(Color.White),
                Utilities.ColorToRgba32(Color.White),
            };
        }
    }
}
