using System;
using System.Windows.Forms.Design;
using System.ComponentModel;
using System.Drawing.Design;
using System.Windows.Forms;
using System.Collections;
using System.ComponentModel.Design;

namespace CustomControls.CollectionEditor
{
	public class CustomCollectionEditor: System.Drawing.Design.UITypeEditor 
	{

		public delegate void CollectionChangedEventHandler(object sender, object instance, object value);
		public event CollectionChangedEventHandler CollectionChanged;

		private ITypeDescriptorContext _context;
		
		private IWindowsFormsEditorService edSvc = null;

		public CustomCollectionEditor()
		{}

		
		public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value) 
		{
			if (context != null	&& context.Instance != null	&& provider != null) 
			{
				object originalValue=value;
				_context=context;
				edSvc = (IWindowsFormsEditorService)provider.GetService(typeof(IWindowsFormsEditorService));

				if (edSvc != null) 
				{
					CustomCollectionEditorForm collEditorFrm = CreateForm();
					collEditorFrm.ItemAdded+= new CustomCollectionEditorForm.InstanceEventHandler(ItemAdded);
					collEditorFrm.ItemRemoved+= new  CustomCollectionEditorForm.InstanceEventHandler(ItemRemoved);
				
					collEditorFrm.Collection=(System.Collections.CollectionBase)value;
						
			
					context.OnComponentChanging();
					if(edSvc.ShowDialog(collEditorFrm)==DialogResult.OK)
					{
						OnCollectionChanged(context.Instance, value);
						context.OnComponentChanged();
					}					

				
					
				}
			}

			return value;
		}

		
		public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context) 
		{
			if (context != null && context.Instance != null) 
			{
				return UITypeEditorEditStyle.Modal;
			}
			return base.GetEditStyle(context);
		}

		
		private void ItemAdded(object sender, object item)
		{
			
			if(_context!=null && _context.Container!=null)
			{
				IComponent icomp=item as IComponent;
				if(icomp !=null )
				{	
					_context.Container.Add(icomp);									
				}
			}

		}

	
		private void ItemRemoved(object sender, object item)
		{			
			if(_context!=null && _context.Container!=null)
			{
				IComponent icomp=item as IComponent;
				if(icomp!=null)
				{						
					_context.Container.Remove(icomp);
				}
			}

		}

	
		protected virtual void OnCollectionChanged(object instance ,object value)
		{
			if(CollectionChanged !=null)
			{
				CollectionChanged(this, instance,value);
			}
		}
		

		protected virtual CustomCollectionEditorForm CreateForm()
		{
			return new CustomCollectionEditorForm();
		}

		
	}
}
