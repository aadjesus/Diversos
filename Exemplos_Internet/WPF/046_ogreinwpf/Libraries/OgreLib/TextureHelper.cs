using System;
using Mogre;

namespace OgreLib
{
    public static class TextureHelper
    {
        unsafe public static void CalculateAplha(Texture texture)
        {
            if (texture == null) return;

            if (texture.Format == PixelFormat.PF_X8R8G8B8) return;
            if (texture.Format != PixelFormat.PF_A8R8G8B8) throw new ArgumentException("Currently only ARGB textures allowed.");

            HardwareBuffer buffer = texture.GetBuffer();
            var data = (uint*)buffer.Lock(HardwareBuffer.LockOptions.HBL_DISCARD);
            try
            {
                var size = sizeof(uint);
                var width = texture.Width;
                var height = texture.Height;
                var pitch = (width / size) * size + ((width % size) > 0 ? size : 0);

                for (var y = 0; y < height; ++y)
                {
                    for (var x = 0; x < width; ++x)
                    {
                        var pixel = data[pitch * y + x];
                        var r = (byte)((pixel & 0x00FF0000) >> 16);
                        var g = (byte)((pixel & 0x0000FF00) >> 8);
                        var b = (byte)(pixel & 0x000000FF);
                        var a = (r + g + b) / 3;

                        pixel = (uint)((a << 24) + (r << 16) + (g << 8) + b);
                        data[pitch * y + x] = pixel;
                    }
                }
            }
            finally
            {
                buffer.Unlock();
            }
        }

        public static void CalculateAlphas(TexturePtr texturePtr)
        {
            if (texturePtr == null) return;

            CalculateAplha(texturePtr.Target);
        }

        public static void CalculateAllAlphas(MaterialPtr material)
        {
            if (material == null) return;

            foreach (var technique in material.GetTechniqueIterator())
            {
                foreach (var pass in technique.GetPassIterator())
                {
                    foreach (var textureUnit in pass.GetTextureUnitStateIterator())
                    {
                        if (string.IsNullOrEmpty(textureUnit.TextureName)) return;

                        if (TextureManager.Singleton.GetByName(textureUnit.TextureName) == null)
                        {
                            var texture = TextureManager.Singleton.Load(textureUnit.TextureName, material.Group,
                                                                        TextureType.TEX_TYPE_2D,
                                                                        textureUnit.NumMipmaps, 1, false,
                                                                        PixelFormat.PF_A8R8G8B8);
                            CalculateAplha(texture);
                        }
                    }
                }
            }
        }
        public static void CalculateAllAlphas(ParticleSystem particalSystem)
        {
            CalculateAllAlphas(MaterialManager.Singleton.GetByName(particalSystem.MaterialName));
        }
    }
}
