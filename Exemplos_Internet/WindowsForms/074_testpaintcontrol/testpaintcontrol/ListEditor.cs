using System;
using System.Collections.Generic;
using System.Text;

using DevExpress.XtraEditors;
using System.ComponentModel;

namespace testpaintcontrol
{
    public class ListEditor : PopupContainerEdit
    {
        //Override the Properties property
        //Simply type-cast the object to the custom repository item type
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public new RepositoryItemListEditor Properties
        {
            get
            {
                return base.Properties as RepositoryItemListEditor;
            }
        }

        /// <summary>
        /// The unique Editor name
        /// </summary>
        public override string EditorTypeName
        {
            get
            {
                return RepositoryItemListEditor.ControlName;
            }
        }

        /// <summary>Default constructor.</summary>
        /// <param name="field"></param>
        public ListEditor()
            : base()
        {

        }

        /// <summary>Static constructor : registers the editor with the DevExpress repository.
        /// </summary>
        static ListEditor()
        {
            RepositoryItemListEditor.RegisterListEditor();
        }

    }
}
