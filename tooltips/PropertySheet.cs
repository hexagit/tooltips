using SharpShell.Attributes;
using SharpShell.SharpPropertySheet;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;

namespace tooltips
{
	[ComVisible(true)]
	[COMServerAssociation(AssociationType.AllFiles)]
	public class PropertySheet : SharpPropertySheet
	{
		protected override bool CanShowSheet()
		{
			return (SelectedItemPaths.Count() == 1);
		}

		protected override IEnumerable<SharpPropertyPage> CreatePages()
		{
			var page = new MessageMetaPage();
			return new[] { page };
		}
	}
}
