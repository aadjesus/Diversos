using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CycleTracks
{
    public class Heart
    {
        public IEnumerator<int> HeartRate()
        {
            Random rnd = new Random(DateTime.Now.Millisecond);
            int lastRate = 120;
            int correction = 2;

            while (true)
            {
                yield return lastRate;
                lastRate += correction + rnd.Next(-5, +5);
                if (correction > 0 && lastRate > 150)
                    correction = -2;
                else if (correction < 0 && lastRate < 80)
                    correction = +2;
            }
        }
    }
}
