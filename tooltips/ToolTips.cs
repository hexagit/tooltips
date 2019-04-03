using SharpShell.Attributes;
using SharpShell.SharpInfoTipHandler;
using System;
using System.Runtime.InteropServices;

namespace tooltips
{
	[ComVisible(true)]
	[COMServerAssociation(AssociationType.AllFiles)]
	public class ToolTips : SharpInfoTipHandler
	{
		private string FindTip()
		{
			var tipEditor = new MetaEditor();
			try
			{
				return tipEditor.GetInformation(SelectedItemPath);
			}
			catch
			{
				return string.Empty;
			}
		}
		protected override string GetInfo(RequestedInfoType infoType, bool singleLine)
		{
			switch (infoType)
			{
				case RequestedInfoType.InfoTip:
					return FindTip();
				default:
					return string.Empty;
			}
		}
	}
}
