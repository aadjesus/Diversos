/*
Copyright (C) 2005 by Jeremy Spiller.  All rights reserved.

Redistribution and use in source and binary forms, with or without 
modification, are permitted provided that the following conditions are met:

   1. Redistributions of source code must retain the above copyright 
      notice, this list of conditions and the following disclaimer.
   2. Redistributions in binary form must reproduce the above copyright 
      notice, this list of conditions and the following disclaimer in 
      the documentation and/or other materials provided with the distribution.
 
THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS"
AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE 
IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE 
ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT OWNER OR CONTRIBUTORS BE 
LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR 
CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF
SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS 
INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN 
CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) 
ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE 
POSSIBILITY OF SUCH DAMAGE.
*/

using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using Microsoft.DirectX;
using Microsoft.DirectX.Direct3D;
using Gosub;

namespace DxControl
{
	public partial class SampleForm : Form
	{
		
		AutoMesh		 mJeremyMesh;
		Vector3			 mJeremyMeshCenter;
		float			 mJeremyMeshRadius;
		AutoMesh		 mCubeMesh;
		AutoTexture		 mGosubTexture;
		AutoVertexBuffer mSquareVB;
		Color32			 mBackgroundColor = Color.DarkBlue;

	
		public SampleForm()
		{
			InitializeComponent();
		}
	
		/// <summary>
		/// Generate an alpha map for the texture.  min/max color and alpha parameters are 0..255.
		/// Typically min/max alpha will be (0, 255), and min/max color will be a small range
		/// like (16, 32) to create a soft edge.
		/// </summary>
		public void GenerateAlphaMap(Texture texture, int minAlpha, int maxAlpha, int minColor, int maxColor)
		{
			// Generate an alphamap
			int width = texture.GetLevelDescription(0).Width;
			int height = texture.GetLevelDescription(0).Height;
			int pitch;
			Color32 []bm = (Color32[])texture.LockRectangle(typeof(Color32), 
															0, 0, out pitch, width*height);
			
			// Multiply by 3 because alpha calculation is R+G+B
			minColor *= 3;
			maxColor *= 3;
			
			for (int i = 0;  i < bm.Length;  i++)
			{
				Color32 color = bm[i];
				
				int colorSum = color.iR + color.iG + color.iB; // alpha = color*3
				
				// Calculate alpha
				int alpha;
				if (colorSum >= maxColor)
					alpha = maxAlpha;
				else if (colorSum <= minColor)
					alpha = minAlpha;
				else
				{
					// Convert colorSum from (minColor..maxColor) to (minApha..maxAlpha)
					alpha = (colorSum-minColor)*(maxAlpha-minAlpha)/(maxColor-minColor)  + minAlpha;
				}
				
				bm[i] = new Color32(alpha, color);
			}
			texture.UnlockRectangle(0);	
		}	
		

		/// <summary>
		/// Occurs once after DirectX has been initialized for the first time.  
		/// Setup AutoMesh's, AutoVertexBuffer's, and AutoTexture's here.
		/// </summary>
		private void mD3d_DxLoaded(Gosub.Direct3d d3d, Microsoft.DirectX.Direct3D.Device dx)
		{
			// Build 3D text, then get its center
			mJeremyMesh = new AutoMesh(d3d, Mesh.TextFromFont(dx,
				new System.Drawing.Font("Arial", 18,
				System.Drawing.FontStyle.Bold)
				, "Jeremy Spiller", 0.001f, 1));
	
			// Find center of mesh
			mJeremyMeshRadius = mJeremyMesh.BoundingSphereMin(out mJeremyMeshCenter);

			// Make a cube mesh			
			mCubeMesh = new AutoMesh(d3d, Mesh.Box(dx, 1, 1, 1));
			
			// Read Gosub logo texture, and create an AutoTexture for it
			// NOTE: Textgure sizes must be a power of two
			string path = Path.GetDirectoryName(Application.ExecutablePath);
			Stream stream = File.Open(path + "\\GosubLogoStampFrame.png", FileMode.Open);
			Bitmap bitmap = (Bitmap)Bitmap.FromStream(stream);
			Texture texture = Texture.FromBitmap(d3d.Dx, bitmap, Usage.AutoGenerateMipMap, Pool.Managed);
			mGosubTexture = new AutoTexture(d3d, texture);
			stream.Close();
			bitmap.Dispose();
			GenerateAlphaMap(mGosubTexture.T, 0, 255, 16, 64);
			
			
			// Create an AutoVertexBuffer to display the Gosub logo (two triangles, one square)
			// NOTE: Texture is 256x128, so use Vector3(2, 1, 0) to maintain aspect ration
			Vector3 n = new Vector3(0, 0, -1);  // Normal
			VertexTypePNT p1 = new VertexTypePNT(new Vector3(-2,  1, 0), n, 0f, 0f);  // Upper left
			VertexTypePNT p2 = new VertexTypePNT(new Vector3( 2,  1, 0), n, 1f, 0f);  // Upper right
			VertexTypePNT p3 = new VertexTypePNT(new Vector3(-2, -1, 0), n, 0f, 1f);  // Lower left
			VertexTypePNT p4 = new VertexTypePNT(new Vector3( 2, -1, 0), n, 1f, 1f);  // Lower right
			int vertices = 6;  // Two triangles to make one square
			VertexTypePNT[] squareVertices = new VertexTypePNT[vertices];
			squareVertices[0] = p1;
			squareVertices[1] = p2;
			squareVertices[2] = p4;
			squareVertices[3] = p4;
			squareVertices[4] = p3;
			squareVertices[5] = p1;
			mSquareVB = new AutoVertexBuffer(d3d, typeof(VertexTypePNT), vertices,
									Usage.Dynamic, VertexTypePNT.Format,	Pool.Default);
			mSquareVB.VB.SetData(squareVertices, 0, LockFlags.None);
			
		}

		// More accurate than Environment.TickCount for smoother motion
		[System.Security.SuppressUnmanagedCodeSecurity]
		[DllImport("winmm.dll")]
		private static extern int timeGetTime();

		
		/// <summary>
		/// Occurs when it is time to render 3d objects.  Place all 3d
		/// drawing code in this event.
		/// </summary>
		private void mD3d_DxRender3d(Gosub.Direct3d d3d, Microsoft.DirectX.Direct3D.Device dx)
		{
            // Setup the lights
            dx.Lights[0].Enabled = true;
            dx.Lights[0].Type = LightType.Directional;
            dx.Lights[0].Direction = new Vector3(0, 0, 1);
            dx.Lights[0].Diffuse = Color.White;
            dx.Lights[0].Position = new Vector3(0, 0, 0);
            dx.RenderState.Lighting = true;

            // Set viewer		
            dx.Transform.View = Matrix.LookAtLH(new Vector3(0, 0, -220),
                new Vector3(0.0f, 0.0f, 0.0f), new Vector3(0.0f, 1.0f, 0.0f));

            // Set projection matrix
            dx.Transform.Projection = Matrix.PerspectiveFovLH(
                (float)Math.PI / 4, 640f / 480f, 50.0f, 2000.0f);
            dx.RenderState.NormalizeNormals = true;
            
            // int ticks = Environment.TickCount; // Safe, less accurate
			int ticks = timeGetTime();  // Unsafe, more accurate

			// --------------------------------------------------------------
			// Draw background
			// --------------------------------------------------------------
			dx.Transform.World = Matrix.Scaling(200, 200, 200) * Matrix.Translation(0, 0, 100);
			dx.Material = GraphicsUtility.InitMaterial(mBackgroundColor);
			dx.VertexFormat = mSquareVB.VB.Description.VertexFormat;
			dx.SetStreamSource(0, mSquareVB.VB, 0);
			dx.DrawPrimitives(PrimitiveType.TriangleList, 0, mSquareVB.NumVertices/3);			
			
			// --------------------------------------------------------------
			// Draw Jeremy text model
			// --------------------------------------------------------------
			int timeMs = 4000;
			float circleTime = (float)(ticks % timeMs) / (float)timeMs * 2 * (float)Math.PI;
			float yaw = 0;
			float pitch = circleTime;
			float roll = 0;
			float x = 0;
			float y = 0;
			float z = 0;
		
			// Setup the world matrix to position (and scale and rotate) the model in 3d world space
			dx.Transform.World = Matrix.Translation(-1*mJeremyMeshCenter) //Center model
								 * Matrix.Scaling(35, 35, 5) // Make it bigger
								 * Matrix.RotationYawPitchRoll(yaw, pitch, roll)
								 * Matrix.Translation(x, y, z); // Then move it where you want
			dx.Material = GraphicsUtility.InitMaterial(Color.Blue);
			mJeremyMesh.M.DrawSubset(0);

			// --------------------------------------------------------------
			// Draw box
			// --------------------------------------------------------------
			timeMs = 5000;
			circleTime = (float)(ticks % timeMs) / (float)timeMs * 2 * (float)Math.PI;
			yaw = circleTime;
			pitch = circleTime*2;
			roll = circleTime*3;
			float radius = 50;
			x = radius * (float)Math.Cos(circleTime);
			y = radius * (float)Math.Sin(circleTime);
			z = 0;

			// Setup the world matrix to position (and scale and rotate) the model in 3d world space
			dx.Transform.World = Matrix.Translation(0, 0, 0) //Center model
								 * Matrix.Scaling(25, 25, 25) // Make it bigger
								 * Matrix.RotationYawPitchRoll(yaw, pitch, roll)
								 * Matrix.Translation(x, y, z); // Then move it where you want
			dx.Material = GraphicsUtility.InitMaterial(Color.Red);
			mCubeMesh.M.DrawSubset(0);

			// --------------------------------------------------------------
			// Draw gosub logo texture
			// --------------------------------------------------------------
			timeMs = 30000;
			circleTime = (float)(ticks % timeMs) / (float)timeMs * 2 * (float)Math.PI;
			// Setup to render texture
			dx.SetTexture(0, mGosubTexture.T);
            dx.Material = GraphicsUtility.InitMaterial(Color.White);
			dx.RenderState.AlphaBlendEnable = true;
			dx.RenderState.SourceBlend = Blend.SourceAlpha;
			dx.RenderState.DestinationBlend = Blend.InvSourceAlpha;
			dx.RenderState.ColorWriteEnable = ColorWriteEnable.RedGreenBlueAlpha;
			dx.RenderState.AlphaBlendOperation = BlendOperation.Add;
			dx.RenderState.ColorVertex = true;			
			dx.TextureState[0].AlphaOperation = TextureOperation.Modulate;
			

			// Setup the world matrix to position (and scale and rotate) the model in 3d world space
			dx.Transform.World = Matrix.Scaling(100, 100, 100) // Make it bigger
								 * Matrix.RotationYawPitchRoll(0, 0, circleTime)
								 * Matrix.Translation(0, 0, 50); // Then move it behind scene
			
			dx.VertexFormat = mSquareVB.VB.Description.VertexFormat;
			dx.SetStreamSource(0, mSquareVB.VB, 0);
			dx.DrawPrimitives(PrimitiveType.TriangleList, 0, mSquareVB.NumVertices/3);

			dx.RenderState.Lighting = true;
			dx.RenderState.AlphaBlendEnable = false;
			dx.SetTexture(0, null);
		}

		/// <summary>
		/// Allow user to toggle full screen
		/// </summary>
		private void SampleForm_KeyDown(object sender, KeyEventArgs e)
		{
			// Toggle full screen
			if (e.KeyCode == Keys.F1)
				mD3d.DxFullScreen = !mD3d.DxFullScreen;
			
			// Exit full screen
			if (e.KeyCode == Keys.Escape)
				mD3d.DxFullScreen = false;
		}

		/// <summary>
		/// Ask the control to resize to fit the form (even when the form size changes)
		/// </summary>
		private void buttonAutoSize_Click(object sender, EventArgs e)
		{
			mD3d.DxAutoResize = true;
			mD3d.BringToFront();  // Make sure the control is on top of other stuff
		}

		/// <summary>
		/// Display the screen parameters form
		/// </summary>
		private void buttonScreenParams_Click(object sender, EventArgs e)
		{
			mD3d.DxShowSelectDeviceDialog(this);
		}
		
		/// <summary>
		/// Make a solid alpha map
		/// </summary>
		private void radioSolid_CheckedChanged(object sender, EventArgs e)
		{
			GenerateAlphaMap(mGosubTexture.T, 255, 255, 0, 0);
		}

		/// <summary>
		/// Alpha map (fades to clear)
		/// </summary>
		private void radioDarkFadesToClear_CheckedChanged(object sender, EventArgs e)
		{
			GenerateAlphaMap(mGosubTexture.T, 0, 255, 16, 64);
		}

		/// <summary>
		/// Alpha map (black is clear)
		/// </summary>
		private void radioBlackIsClear_CheckedChanged(object sender, EventArgs e)
		{
			GenerateAlphaMap(mGosubTexture.T, 0, 255, 0, 1);
		}

		/// <summary>
		/// Alpha map for circle
		/// </summary>
		private void radioFade_CheckedChanged(object sender, EventArgs e)
		{
			GenerateAlphaMap(mGosubTexture.T, 0, 255, 16, 64);  // Dark is clear
			
			// Generate an alphamap with a circle
			Texture texture = mGosubTexture.T;
			int width = texture.GetLevelDescription(0).Width;
			int height = texture.GetLevelDescription(0).Height;
			int pitch;
			Color32 []bm = (Color32[])texture.LockRectangle(typeof(Color32), 
															0, 0, out pitch, width*height);
			// Draw the circle (use distance formula)
			float scaleAlpha = 1.0f/height*2;
			for (int x = 0;  x < width;  x++)
				for (int y = 0;  y < height;  y++)
				{
					// Attenuate alpha by distance from center
					int dx = x-width/2;
					int dy = y-height/2;
					float unitDistance = (float)Math.Sqrt(dx*dx + dy*dy)*scaleAlpha;
					Color32 color = bm[y*width + x];
					float alpha = 1-unitDistance;  // Center is solid, edges clear
					alpha = Math.Min(alpha, 1);
					alpha = Math.Max(alpha, 0);
					bm[y*width + x] = new Color32( (int)(alpha * color.iA), color);
				}				
			texture.UnlockRectangle(0);	
		}

		/// <summary>
		/// User changes the background color
		/// </summary>
		private void pictureWhite_Click(object sender, EventArgs e)
		{
			mBackgroundColor = pictureWhite.BackColor;
		}

		/// <summary>
		/// User changes the background color
		/// </summary>
		private void pictureBlue_Click(object sender, EventArgs e)
		{
			mBackgroundColor = pictureBlue.BackColor;
		}
	
		/// <summary>
		/// User wants to load a mesh
		/// </summary>
		private void loadMesh_Click(object sender, EventArgs e)
		{
			// Show open dialog
			openFileDialog1.FileName = "";
			openFileDialog1.Title = "Load texture";
			openFileDialog1.Multiselect = false;
			openFileDialog1.Filter = "Mesh (*.x)|*.x";
			openFileDialog1.ShowDialog(this);
			string file = openFileDialog1.FileName;
			
			// User cancels
			if (file == "")
				return;
				
			FormLoadMesh.Show(this, file);
		}
	}
}