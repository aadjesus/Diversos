//-----------------------------------------------------------------------------
// File: D3DUtil.cs
//
// Desc: Shortcut functions for using DX objects
//
// Copyright (c) Microsoft Corporation. All rights reserved
//-----------------------------------------------------------------------------
using System;
using System.Runtime.InteropServices;
using System.IO;
using System.Windows.Forms;
using System.Drawing;
using Microsoft.DirectX;
using Microsoft.DirectX.Direct3D;
using Direct3D = Microsoft.DirectX.Direct3D;




/// <summary>
/// Various helper functions for graphics samples
/// </summary>
public class GraphicsUtility
{
	/// <summary>
	/// Private Constructor 
	/// </summary>
	private GraphicsUtility()
	{
	}



	/// <summary>
	/// Initializes a Material structure, setting the diffuse and ambient
	/// colors. It does not set emissive or specular colors.
	/// </summary>
	/// <param name="c">The ambient and diffuse color</param>
	/// <returns>A defined material</returns>
	public static Direct3D.Material InitMaterial(System.Drawing.Color c)
	{
		Material mtrl = new Material();
		mtrl.Ambient = mtrl.Diffuse = c;
		return mtrl;
	}




	/// <summary>
	/// Initializes a light, setting the light position. The
	/// diffuse color is set to white; specular and ambient are left as black.
	/// </summary>
	/// <param name="light">Which light to initialize</param>
	/// <param name="ltType">The type</param>
	public static void InitLight(Light light, LightType ltType, float x, float y, float z)
	{
		light.Type = ltType;
		light.Diffuse = System.Drawing.Color.White;
		light.Position = new Vector3(x, y, z);
		light.Direction = Vector3.Normalize(light.Position);
		light.Range = 1000.0f;
	}




	/// <summary>
	/// Helper function to create a texture. It checks the root path first,
	/// then tries the DXSDK media path (as specified in the system registry).
	/// </summary>
	public static Texture CreateTexture(Device device, string textureFilename, Format format)
	{
		// Get the path to the texture
		string path = DXUtil.FindMediaFile(null, textureFilename);

		// Create the texture using D3DX
		return TextureLoader.FromFile(device, path, D3DX.Default, D3DX.Default, D3DX.Default, 0, format,
			Pool.Managed, Filter.Triangle | Filter.Mirror,
			Filter.Triangle | Filter.Mirror, 0);
	}




	/// <summary>
	/// Helper function to create a texture. It checks the root path first,
	/// then tries the DXSDK media path (as specified in the system registry).
	/// </summary>
	public static Texture CreateTexture(Device device, string textureFilename)
	{
		return GraphicsUtility.CreateTexture(device, textureFilename, Format.Unknown);
	}





	/// <summary>
	/// Returns a view matrix for rendering to a face of a cubemap.
	/// </summary>
	public static Matrix GetCubeMapViewMatrix(CubeMapFace face)
	{
		Vector3 vEyePt = new Vector3(0.0f, 0.0f, 0.0f);
		Vector3 vLookDir = new Vector3();
		Vector3 vUpDir = new Vector3();

		switch (face)
		{
			case CubeMapFace.PositiveX:
				vLookDir = new Vector3(1.0f, 0.0f, 0.0f);
				vUpDir = new Vector3(0.0f, 1.0f, 0.0f);
				break;
			case CubeMapFace.NegativeX:
				vLookDir = new Vector3(-1.0f, 0.0f, 0.0f);
				vUpDir = new Vector3(0.0f, 1.0f, 0.0f);
				break;
			case CubeMapFace.PositiveY:
				vLookDir = new Vector3(0.0f, 1.0f, 0.0f);
				vUpDir = new Vector3(0.0f, 0.0f, -1.0f);
				break;
			case CubeMapFace.NegativeY:
				vLookDir = new Vector3(0.0f, -1.0f, 0.0f);
				vUpDir = new Vector3(0.0f, 0.0f, 1.0f);
				break;
			case CubeMapFace.PositiveZ:
				vLookDir = new Vector3(0.0f, 0.0f, 1.0f);
				vUpDir = new Vector3(0.0f, 1.0f, 0.0f);
				break;
			case CubeMapFace.NegativeZ:
				vLookDir = new Vector3(0.0f, 0.0f, -1.0f);
				vUpDir = new Vector3(0.0f, 1.0f, 0.0f);
				break;
		}

		// Set the view transform for this cubemap surface
		Matrix matView = Matrix.LookAtLH(vEyePt, vLookDir, vUpDir);
		return matView;
	}




	/// <summary>
	/// Returns a quaternion for the rotation implied by the window's cursor position
	/// </summary>
	public static Quaternion GetRotationFromCursor(System.Windows.Forms.Form control, float fTrackBallRadius)
	{
		System.Drawing.Point pt = System.Windows.Forms.Cursor.Position;
		System.Drawing.Rectangle rc = control.ClientRectangle;
		pt = control.PointToClient(pt);
		float xpos = (((2.0f * pt.X) / (rc.Right - rc.Left)) - 1);
		float ypos = (((2.0f * pt.Y) / (rc.Bottom - rc.Top)) - 1);
		float sz;

		if (xpos == 0.0f && ypos == 0.0f)
			return new Quaternion(0.0f, 0.0f, 0.0f, 1.0f);

		float d2 = (float)Math.Sqrt(xpos * xpos + ypos * ypos);

		if (d2 < fTrackBallRadius * 0.70710678118654752440) // Inside sphere
			sz = (float)Math.Sqrt(fTrackBallRadius * fTrackBallRadius - d2 * d2);
		else                                                 // On hyperbola
			sz = (fTrackBallRadius * fTrackBallRadius) / (2.0f * d2);

		// Get two points on trackball's sphere
		Vector3 p1 = new Vector3(xpos, ypos, sz);
		Vector3 p2 = new Vector3(0.0f, 0.0f, fTrackBallRadius);

		// Get axis of rotation, which is cross product of p1 and p2
		Vector3 axis = Vector3.Cross(p1, p2);

		// Calculate angle for the rotation about that axis
		float t = Vector3.Length(Vector3.Subtract(p2, p1)) / (2.0f * fTrackBallRadius);
		if (t > +1.0f) t = +1.0f;
		if (t < -1.0f) t = -1.0f;
		float fAngle = (float)(2.0f * Math.Asin(t));

		// Convert axis to quaternion
		return Quaternion.RotationAxis(axis, fAngle);
	}




	/// <summary>
	/// Returns a quaternion for the rotation implied by the window's cursor position
	/// </summary>
	public static Quaternion GetRotationFromCursor(System.Windows.Forms.Form control)
	{
		return GetRotationFromCursor(control, 1.0f);
	}




	/// <summary>
	/// Axis to axis quaternion double angle (no normalization)
	/// Takes two points on unit sphere an angle THETA apart, returns
	/// quaternion that represents a rotation around cross product by 2*THETA.
	/// </summary>
	public static Quaternion D3DXQuaternionUnitAxisToUnitAxis2(Vector3 fromVector, Vector3 toVector)
	{
		Vector3 axis = Vector3.Cross(fromVector, toVector);    // proportional to sin(theta)
		return new Quaternion(axis.X, axis.Y, axis.Z, Vector3.Dot(fromVector, toVector));
	}




	/// <summary>
	/// Axis to axis quaternion 
	/// Takes two points on unit sphere an angle THETA apart, returns
	/// quaternion that represents a rotation around cross product by theta.
	/// </summary>
	public static Quaternion D3DXQuaternionAxisToAxis(Vector3 fromVector, Vector3 toVector)
	{
		Vector3 vA = Vector3.Normalize(fromVector), vB = Vector3.Normalize(toVector);
		Vector3 vHalf = Vector3.Add(vA, vB);
		vHalf = Vector3.Normalize(vHalf);
		return GraphicsUtility.D3DXQuaternionUnitAxisToUnitAxis2(vA, vHalf);
	}




	/// <summary>
	/// Gets the number of ColorChanelBits from a format
	/// </summary>
	static public int GetColorChannelBits(Format format)
	{
		switch (format)
		{
			case Format.R8G8B8:
				return 8;
			case Format.A8R8G8B8:
				return 8;
			case Format.X8R8G8B8:
				return 8;
			case Format.R5G6B5:
				return 5;
			case Format.X1R5G5B5:
				return 5;
			case Format.A1R5G5B5:
				return 5;
			case Format.A4R4G4B4:
				return 4;
			case Format.R3G3B2:
				return 2;
			case Format.A8R3G3B2:
				return 2;
			case Format.X4R4G4B4:
				return 4;
			case Format.A2B10G10R10:
				return 10;
			case Format.A2R10G10B10:
				return 10;
			default:
				return 0;
		}
	}




	/// <summary>
	/// Gets the number of alpha channel bits 
	/// </summary>
	static public int GetAlphaChannelBits(Format format)
	{
		switch (format)
		{
			case Format.R8G8B8:
				return 0;
			case Format.A8R8G8B8:
				return 8;
			case Format.X8R8G8B8:
				return 0;
			case Format.R5G6B5:
				return 0;
			case Format.X1R5G5B5:
				return 0;
			case Format.A1R5G5B5:
				return 1;
			case Format.A4R4G4B4:
				return 4;
			case Format.R3G3B2:
				return 0;
			case Format.A8R3G3B2:
				return 8;
			case Format.X4R4G4B4:
				return 0;
			case Format.A2B10G10R10:
				return 2;
			case Format.A2R10G10B10:
				return 2;
			default:
				return 0;
		}
	}




	/// <summary>
	/// Gets the number of depth bits
	/// </summary>
	static public int GetDepthBits(DepthFormat format)
	{
		switch (format)
		{
			case DepthFormat.D16:
				return 16;
			case DepthFormat.D15S1:
				return 15;
			case DepthFormat.D24X8:
				return 24;
			case DepthFormat.D24S8:
				return 24;
			case DepthFormat.D24X4S4:
				return 24;
			case DepthFormat.D32:
				return 32;
			default:
				return 0;
		}
	}




	/// <summary>
	/// Gets the number of stencil bits
	/// </summary>
	static public int GetStencilBits(DepthFormat format)
	{
		switch (format)
		{
			case DepthFormat.D16:
				return 0;
			case DepthFormat.D15S1:
				return 1;
			case DepthFormat.D24X8:
				return 0;
			case DepthFormat.D24S8:
				return 8;
			case DepthFormat.D24X4S4:
				return 4;
			case DepthFormat.D32:
				return 0;
			default:
				return 0;
		}
	}




	/// <summary>
	/// Assembles and creates a file-based vertex shader
	/// </summary>
	public static VertexShader CreateVertexShader(Device device, string filename)
	{
		GraphicsStream code = null;
		string path = null;

		// Get the path to the vertex shader file
		path = DXUtil.FindMediaFile(null, filename);

		// Assemble the vertex shader file
		code = ShaderLoader.FromFile(path, null, 0);

		// Create the vertex shader
		return new VertexShader(device, code);
	}

}







/// <summary>
/// Generic utility functions for our samples
/// </summary>
public class DXUtil
{

	// Constants for SDK Path registry keys
	private const string sdkPath = "Software\\Microsoft\\DirectX SDK";
	private const string sdkKey = "DX9SDK Samples Path";

	private DXUtil() { /* Private Constructor */ }




	/// <summary>
	/// Returns the DirectX SDK media path
	/// </summary>
	public static string SdkMediaPath
	{
		get
		{
			Microsoft.Win32.RegistryKey rKey = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(sdkPath);
			string sReg = string.Empty;
			if (rKey != null)
			{
				sReg = (string)rKey.GetValue(sdkKey);
				rKey.Close();
			}
			if (sReg != null)
				sReg += @"\Media\";
			else
				return string.Empty;

			return sReg;
		}
	}






	/// <summary>
	/// Returns a valid path to a DXSDK media file
	/// </summary>
	/// <param name="path">Initial path to search</param>
	/// <param name="filename">Filename we're searching for</param>
	/// <returns>Full path to the file</returns>
	public static string FindMediaFile(string path, string filename)
	{
		// First try to load the file in the full path
		if (path != null)
		{
			if (File.Exists(AppendDirectorySeparator(path) + filename))
				return AppendDirectorySeparator(path) + filename;
		}

		// if not try to find the filename in the current folder.
		if (File.Exists(filename))
			return AppendDirectorySeparator(Directory.GetCurrentDirectory()) + filename;

		// last, check if the file exists in the media directory
		if (File.Exists(AppendDirectorySeparator(SdkMediaPath) + filename))
			return AppendDirectorySeparator(SdkMediaPath) + filename;

		throw new FileNotFoundException("Could not find this file.", filename);
	}




	/// <summary>
	/// Returns a valid string with a directory separator at the end.
	/// </summary>
	private static string AppendDirectorySeparator(string filename)
	{
		if (!filename.EndsWith(@"\"))
			return filename + @"\";

		return filename;
	}
}
