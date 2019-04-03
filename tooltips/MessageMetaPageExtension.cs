using SharpShell.SharpPropertySheet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tooltips
{
	public partial class MessageMetaPage
	{
		private string FileName { get; set; }
		protected override void OnPropertyPageInitialised(SharpPropertySheet parent)
		{
			FileName = parent.SelectedItemPaths.First();
			var editor = new MetaEditor();
			try
			{
				textBox.Text = editor.GetInformation(FileName);
			}
			catch
			{
			}
		}
		private void UpdateMetaData()
		{
			var editor = new MetaEditor();
			if (textBox.Text.Length == 0)
			{
				// if the box is empty, will remove the information from meta data.
				try
				{
					editor.RemoveInformation(FileName);
				}
				catch { }
			}
			else
			{
				try
				{
					editor.SetInformation(FileName, textBox.Text);
				}
				catch { }
			}
		}

		protected override void OnPropertySheetApply()
		{
			UpdateMetaData();
		}
		protected override void OnPropertySheetOK()
		{
			UpdateMetaData();
		}
	}
}
