using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Framework.MP3PlayerModule.Views
{
    public interface IMP3PlayerView
    {
        void RefreshListView();
        void SetTrackListBinding(DataTable table);
    }
}
