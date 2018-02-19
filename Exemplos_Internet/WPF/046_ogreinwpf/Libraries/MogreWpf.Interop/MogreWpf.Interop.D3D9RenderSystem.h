// Mogre.Wpf.Interop.h

#pragma once

using namespace System;
using namespace Mogre;

namespace MogreWpf
{
	namespace Interop 
	{
		public ref class D3D9RenderSystem
		{
		public:
			static bool IsDeviceLost(RenderWindow^ renderWindow)
			{
				Ogre::D3D9RenderWindow *rw = dynamic_cast<Ogre::D3D9RenderWindow *>(renderWindow->NativePtr);
				if (!rw)
					throw gcnew ArgumentException("Not a D3D9RenderWindow", "renderWindow");

				return !SUCCEEDED(rw->getD3DDevice()->TestCooperativeLevel());
			}

			/*
			static void RestoreDevice(RenderSystem^ renderSystem)
			{
				Ogre::D3D9RenderSystem *rs = dynamic_cast<Ogre::D3D9RenderSystem *>(renderSystem->NativePtr);
				if (!rs)
					throw gcnew ArgumentException("Not a D3D9RenderSystem", "renderSystem");

				//rs->restoreLostDevice();
			}
			*/
		};
	}
}
