using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FlipTile3D
{
    /// <summary>
    /// Simple generic tuple implementation
    /// for 3d Material and BlogUrl for a given
    /// Disciple
    /// </summary>
    public struct Tuple<T1,T2>
    {
        #region Data
        private readonly T1 material;
        private readonly T2 blogUrl;
        #endregion

        #region Public Properties
        public T1 Material
        {
            get { return material; }
        }

        public T2 BlogUrl
        {
            get { return blogUrl; }
        }
        #endregion

        #region Ctor
        public Tuple(T1 m, T2 b)
        {
            material = m;
            blogUrl = b;
        }
        #endregion
    }
}
