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
	public partial class FormLoadMesh : Form
	{
		string mFileName;
		AutoMesh mMesh;
		Quaternion mOrientation = Quaternion.Identity;
		bool mMeshLoaded;
		bool mMouseDown;
		Vector3 mMouseDownPos;
		Quaternion mMouseDownOrientation;
	
		public FormLoadMesh()
		{
			InitializeComponent();
		}
		
		/// <summary>
		/// Call to create, show, then dispose this form.
		/// Returns TRUE if user loads the Mesh.
		/// </summary>
		static public bool Show(Form parent, string fileName)
		{
			FormLoadMesh loadMeshForm = new FormLoadMesh();
			bool meshLoaded = loadMeshForm.ShowForm(parent, fileName);
			
			// Todo: capture mMesh and mOrientation (if meshLoaded)
			if (meshLoaded)
				MessageBox.Show(parent, "Load function not implemented");

			// Dispose the mesh after using it
			if (loadMeshForm.mMesh != null)
				loadMeshForm.mMesh.Dispose();
			
			loadMeshForm.Dispose();
			return meshLoaded;
		}
		
		/// <summary>
		/// Display this form
		/// </summary>
		bool ShowForm(Form parent, string fileName)
		{
			mFileName = fileName;
			mMeshLoaded = false;
			this.ShowDialog(parent);
			return mMeshLoaded;
		}

		/// <summary>
		/// User accepts this mesh
		/// </summary>
		private void buttonAccept_Click(object sender, EventArgs e)
		{
			Hide();
			mMeshLoaded = true;
		}

		/// <summary>
		/// User cancels loading the mesh
		/// </summary>
		private void buttonCancel_Click(object sender, EventArgs e)
		{
			Hide();
			if (mMesh != null)
				mMesh.Dispose();
		}

		/// <summary>
		/// Setup the form by loading the mesh
		/// </summary>
		private void FormViewMesh_Shown(object sender, EventArgs e)
		{
			// ToDo: Show a message form while loading			
			AutoMesh autoMesh = null;

			// Load the mesh (or fail and exit)
			try
			{
				autoMesh = AutoMesh.LoadFromXFile(mFileName, MeshFlags.SystemMemory, d3dModel);									
			}
			catch 
			{
				// Dispose whatever we have
				try { autoMesh.Dispose(); } catch { }
				autoMesh = null;
				
				MessageBox.Show(this, "Error loading mesh");
				Hide();
				return;
			}
			
			// Detect unsupported texture formats
			VertexFormats format = autoMesh.M.VertexFormat;
			if ( (format & VertexFormats.TextureCountMask) != VertexFormats.Texture0
					&& (format & VertexFormats.Texture1) != VertexFormats.Texture1 )
			{
				MessageBox.Show(this, "Multiple textures not supported");
				autoMesh.Dispose();
				Hide();
				return;
			}

			// Unsupported vertex formats
			VertexFormats unsupported = VertexFormats.Specular
										| VertexFormats.LastBetaD3DColor
										| VertexFormats.LastBetaUByte4
										| VertexFormats.PointSize
										| VertexFormats.Transformed;
			if ( (int)(format & unsupported) != 0)
			{
				MessageBox.Show(this, "Unsupported vertex format");
				autoMesh.Dispose();
				Hide();
				return;
			}
			
			// No position (sometimes this happens)
			if ( (format & VertexFormats.Position) != VertexFormats.Position )
			{
				MessageBox.Show(this, "Unsupported vertex format (no position detected!)");
				autoMesh.Dispose();
				Hide();
				return;
			}
			
			// Clone to new format, using 32 bit pixel format.
			try
			{
				mMesh = autoMesh.Clone(d3dModel, MeshFlags.Managed, autoMesh.M.VertexFormat,
										Format.A8R8G8B8, Usage.AutoGenerateMipMap, Pool.Managed);
			}
			catch
			{
				MessageBox.Show(this, "Error cloning mesh");
				try { autoMesh.Dispose(); } catch  { }
				try { mMesh.Dispose(); } catch { }
				Hide();
				return;
			}
			
			autoMesh.Dispose();
			autoMesh = null;
			
			// Display mesh info
			DisplayMeshInfo();						
		}

		/// <summary>
		/// Show vertex info to user
		/// </summary>
		void DisplayMeshInfo()
		{
			// Set basic mesh info
			labelPictureName.Text = System.IO.Path.GetFileName(mFileName);
			labelPictureDirectory.Text = System.IO.Path.GetFullPath(mFileName);
			
			// Count number of textures
			int textureCount = 0;
			int texturesMemory = 0;
			for (int j = 0;  j < mMesh.Textures.Length;  j++)
			{
				// Ignore null textures
				if (mMesh.Textures[j] == null)
					continue;
					
				// Ignore already counted textures
				bool alreadyHaveTexture = false;
				for (int k = 0;  k < j;  k++)
					if (mMesh.Textures[j] == mMesh.Textures[k])
						alreadyHaveTexture = true;
				if (alreadyHaveTexture)
					continue;
				
				// Count new texture (sum memory usage)
				textureCount++;
				SurfaceDescription desc = mMesh.Textures[j].T.GetLevelDescription(0);
				texturesMemory += desc.Height * desc.Width * 4;
			}
			// Approximate memory for mip maps
			texturesMemory += texturesMemory/4 + texturesMemory /16;
			
			string textureString = "0";
			if (textureCount != 0)
				textureString = textureCount.ToString() + " (" + texturesMemory/1000 + " Kb video memory)";
			
			// Display mesh info
			float radius;
			Vector3 center;
			radius = mMesh.BoundingSphereMin(out center);
			labelPictureSize.Text = "Faces: " + mMesh.M.NumberFaces
									+ ", Vertices: " + mMesh.M.NumberVertices
									+ ", Textures: " + textureString;

			// Vertex format
			labelFormat.Text = "";
			labelLighting.Text = "";
			if ((mMesh.M.VertexFormat & VertexFormats.Position) != VertexFormats.None)
				labelFormat.Text += "Position";
			if ((mMesh.M.VertexFormat & VertexFormats.Normal) != VertexFormats.None)
				labelFormat.Text += ", Normal";
			if ((mMesh.M.VertexFormat & VertexFormats.Diffuse) != VertexFormats.None)
				labelFormat.Text += ", Diffuse";
			if ((mMesh.M.VertexFormat & VertexFormats.Texture1) != VertexFormats.None)
				labelFormat.Text += ", Textured";
			
			// Mesh has normals?
			if ( (int)(mMesh.M.VertexFormat & VertexFormats.Normal) == 0 )
			{
				buttonAddNormals.Visible = true;
				labelLighting.Text = "Normals not includued, lighting/color disabled";
			}
			else
			{
				buttonAddNormals.Visible = false;
				labelLighting.Text = "";
			}
		}

		/// <summary>
		/// User wants to add normals to the mesh
		/// </summary>
		private void buttonAddNormals_Click(object sender, EventArgs e)
		{
			AutoMesh mesh = mMesh.Clone(d3dModel, MeshFlags.Managed, 
										mMesh.M.VertexFormat | VertexFormats.Normal,
										Format.A8R8G8B8, Usage.AutoGenerateMipMap, Pool.Managed);
			mesh.M.ComputeNormals();
			mMesh.Dispose();
			mMesh = mesh;
			
			DisplayMeshInfo();
		}

		/// <summary>
		/// Called each frame to render the mesh
		/// </summary>
		private void d3dModel_DxRender3d(Direct3d d3d, Device dx)
		{
			if (mMesh == null)
				return;
				
			// Setup the lights
			dx.Lights[0].Enabled = true;
			dx.Lights[0].Type = LightType.Directional;
			dx.Lights[0].Direction = new Vector3(0, 0, 1);
			dx.Lights[0].Diffuse = Color.White;
			dx.Lights[0].Position = new Vector3(0, 0, 0);
			dx.RenderState.NormalizeNormals = true;	

			// Lighting only when there are normals
			dx.RenderState.Lighting = (int)(mMesh.M.VertexFormat & VertexFormats.Normal) != 0;

			// Setup camera		
			dx.Transform.Projection = Matrix.PerspectiveFovLH(
								(float)Math.PI / 4, 640f / 480f, 50.0f, 2000.0f);
			dx.Transform.View = Matrix.LookAtLH(new Vector3(0, 0, -220),
						new Vector3(0.0f, 0.0f, 0.0f), new Vector3(0.0f, 1.0f, 0.0f));
			
			// Use mip map
			dx.SamplerState[0].MipFilter = TextureFilter.Linear;
			dx.SamplerState[0].MinFilter = TextureFilter.Linear;
			dx.SamplerState[0].MagFilter = TextureFilter.Linear;
			
			// Adjust mesh to fit view port	
			Vector3 center;
			float radius = mMesh.BoundingSphereMin(out center);			
			dx.Transform.World = Matrix.Translation(-1*center)
								* Matrix.Scaling(1/radius, 1/radius, 1/radius)
								* Matrix.Scaling(90, 90, 90)
								* Matrix.RotationQuaternion(mOrientation);
			
			// Draw mesh using mesh specified materials
			mMesh.Draw(true);
			
			// For an interesting effect, comment above line and uncomment following lines
			//dx.Material = GraphicsUtility.InitMaterial(Color.Red);
			//mMesh.Draw(true);
		}
		
		/// <summary>
		/// Convert screen (X, Y) to sphere (X, Y, Z)
		/// </summary>
		Vector3 MouseSphere(Vector2 p)
		{
			// Scale to screen
			float x = p.X;
			float y = p.Y;
			float z = x*x + y*y;

			if (z > 1.0f)
			{
				float scale = 1.0f/(float)Math.Sqrt(z);
				x *= scale;
				y *= scale;
				z = 0;
			}
			else
				z = (float)Math.Sqrt(1.0f - z);
			
			return new Vector3(x, y, -z);
		}
		
		/// <summary>
		/// Calculate a rotatation based on mouse movement
		/// </summary>
		Quaternion ArcBall(Vector3 from, Vector3 to)
		{
            float dot = Vector3.Dot(from, to);
            Vector3 part = Vector3.Cross(from, to);
            return new Quaternion(part.X, part.Y, part.Z, dot);
		}

		/// <summary>
		/// User clicks mesh (to grab it)
		/// </summary>
		private void d3dModel_MouseDown(object sender, MouseEventArgs e)
		{
			mMouseDown = true;
			mMouseDownPos = MouseSphere(d3dModel.DxMouseLastScreenPosition);
			mMouseDownOrientation = mOrientation;
		}

		/// <summary>
		/// User moves mesh
		/// </summary>
		private void d3dModel_MouseMove(object sender, MouseEventArgs e)
		{
			if (mMouseDown)
			{
				mOrientation = mMouseDownOrientation * ArcBall(mMouseDownPos, 
									MouseSphere(d3dModel.DxMouseLastScreenPosition));
			}
		}

		/// <summary>
		/// User releases mesh
		/// </summary>
		private void d3dModel_MouseUp(object sender, MouseEventArgs e)
		{
			mMouseDown = false;
		}

		private void FormLoadMesh_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.F1)
				d3dModel.DxFullScreen = !d3dModel.DxFullScreen;
		}
	}
}