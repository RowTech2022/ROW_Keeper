using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Keeper.Core
{
	public class Helper
	{
		public const string FileTempName = "/FilesTemp";
		public const string FilePublicPath = "/FileStorage";

		public static System.Drawing.Image ResizeImage(System.Drawing.Image imgToResize, Size size)
		{
			//Get the image current width  
			int sourceWidth = imgToResize.Width;
			//Get the image current height  
			int sourceHeight = imgToResize.Height;
			float nPercent = 0;
			float nPercentW = 0;
			float nPercentH = 0;
			//Calulate  width with new desired size  
			nPercentW = ((float)size.Width / (float)sourceWidth);
			//Calculate height with new desired size  
			nPercentH = ((float)size.Height / (float)sourceHeight);
			if (nPercentH < nPercentW)
				nPercent = nPercentH;
			else
				nPercent = nPercentW;
			//New Width  
			int destWidth = (int)(sourceWidth * nPercent);
			//New Height  
			int destHeight = (int)(sourceHeight * nPercent);
			Bitmap b = new Bitmap(destWidth, destHeight);
			Graphics g = Graphics.FromImage((System.Drawing.Image)b);
			g.InterpolationMode = InterpolationMode.HighQualityBicubic;
			// Draw image with new width and height  
			g.DrawImage(imgToResize, 0, 0, destWidth, destHeight);
			g.Dispose();
			return (System.Drawing.Image)b;
		}
	}
}
