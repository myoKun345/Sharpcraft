using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;

namespace MyoKun.SharpCraft.Core
{
    public class SharpcraftWindow : GameWindow
    {
        private Sharpcraft Game;
        private Primitive shape = new Cube();

        int vertex_buffer_object, color_buffer_object, element_buffer_object;

        public SharpcraftWindow(int width, int height, string title, Sharpcraft game)
            : base(width, height, new GraphicsMode(32, 0, 0, 4), title)
        {
            this.Game = game;
            Keyboard.KeyDown += HandleKey;
        }

        private void HandleKey(object sender, OpenTK.Input.KeyboardKeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                this.Game.StopGame(0x00);
            }

            if (e.Key == Key.F11)
            {
                if (this.WindowState == WindowState.Fullscreen)
                {
                    this.Game.Writer.Write("Switched back to normal window", "INFO");
                    this.WindowState = WindowState.Normal;
                }
                else
                {
                    this.Game.Writer.Write("Switched to fullscreen", "INFO");
                    this.WindowState = WindowState.Fullscreen;
                }
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            Version version = new Version(GL.GetString(StringName.Version).Substring(0, 3));
            Version target = new Version(2, 0);
            if (version < target)
            {
                throw new NotSupportedException(String.Format("OpenGL {0} is required (you only have {1}).", target, version));
            }

            GL.ClearColor(Color4.White);
            GL.Enable(EnableCap.DepthTest);

            CreateVBO();
        }

        protected void CreateVBO()
        {
            int size;

            GL.GenBuffers(1, out vertex_buffer_object);
            GL.GenBuffers(1, out color_buffer_object);
            GL.GenBuffers(1, out element_buffer_object);

            GL.BindBuffer(BufferTarget.ArrayBuffer, vertex_buffer_object);
            GL.BufferData(BufferTarget.ArrayBuffer, (IntPtr)(shape.Vertices.Length * 3 * sizeof(float)), shape.Vertices, BufferUsageHint.StaticDraw);
            GL.GetBufferParameter(BufferTarget.ArrayBuffer, BufferParameterName.BufferSize, out size);

            if (size != shape.Vertices.Length * 3 * sizeof(Single))
            {
                throw new ApplicationException(String.Format("Problem uploading vertex buffer to VBO. Tried to upload {0} bytes, uploaded {1}.", shape.Vertices.Length * 3 * sizeof(Single), size));
            }

            GL.BindBuffer(BufferTarget.ArrayBuffer, color_buffer_object);
            GL.BufferData(BufferTarget.ArrayBuffer, (IntPtr)(shape.Colors.Length * sizeof(int)), shape.Colors, BufferUsageHint.StaticDraw);
            GL.GetBufferParameter(BufferTarget.ArrayBuffer, BufferParameterName.BufferSize, out size);

            if (size != shape.Colors.Length * sizeof(int))
            {
                throw new ApplicationException(String.Format("Problem uploading color buffer to VBO. Tried to upload {0} bytes, uploaded {1}.", shape.Colors.Length * sizeof(int), size));
            }

            GL.BindBuffer(BufferTarget.ElementArrayBuffer, element_buffer_object);
            GL.BufferData(BufferTarget.ElementArrayBuffer, (IntPtr)(shape.Indices.Length * sizeof(Int32)), shape.Indices, BufferUsageHint.StaticDraw);
            GL.GetBufferParameter(BufferTarget.ElementArrayBuffer, BufferParameterName.BufferSize, out size);

            if (size != shape.Indices.Length * sizeof(int))
            {
                throw new ApplicationException(String.Format("Problem uploading index buffer to VBO. Tried to upload {0} bytes, uploaded {1}.", shape.Indices.Length * sizeof(int), size));
            }
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            GL.Clear(ClearBufferMask.ColorBufferBit |
                     ClearBufferMask.DepthBufferBit);

            Matrix4 lookat = Matrix4.LookAt(0, 5, 5, 0, 0, 0, 0, 1, 0);
            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadMatrix(ref lookat);

            GL.EnableClientState(ArrayCap.VertexArray);
            GL.EnableClientState(ArrayCap.ColorArray);

            GL.BindBuffer(BufferTarget.ArrayBuffer, vertex_buffer_object);
            GL.VertexPointer(3, VertexPointerType.Float, 0, IntPtr.Zero);
            GL.BindBuffer(BufferTarget.ArrayBuffer, color_buffer_object);
            GL.ColorPointer(4, ColorPointerType.UnsignedByte, 0, IntPtr.Zero);
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, element_buffer_object);

            GL.DrawElements(BeginMode.Triangles, shape.Indices.Length,
                DrawElementsType.UnsignedInt, IntPtr.Zero);

            GL.DrawArrays(BeginMode.Points, 0, shape.Vertices.Length);

            GL.DisableClientState(ArrayCap.VertexArray);
            GL.DisableClientState(ArrayCap.ColorArray);


            //int error = GL.GetError();
            //if (error != 0)
            //    Debug.Print(Glu.ErrorString(Glu.Enums.ErrorCode.INVALID_OPERATION));

            SwapBuffers();
        }

        protected override void OnResize(EventArgs e)
        {
            GL.Viewport(0, 0, Width, Height);

            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadIdentity();
            GL.Ortho(-1.0, 1.0, -1.0, 1.0, 0.0, 4.0);

            base.OnResize(e);
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            this.Game.StopGame(0x00);
        }
    }
}
