using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Drawing;
using System.Drawing.Imaging;

namespace WebUI.Infrastructure
{
    public class CAPTCHAImageActionResult:ActionResult
    {
        public Color BackGroundColor { get; set; }
        public Color RandomTextColor { get; set; }
        public string RandomWord { get; set; }

        public override void ExecuteResult(ControllerContext context)
        {

            RenderCAPTCHAImage(context);
        }

        private void RenderCAPTCHAImage(ControllerContext context)
        {
            Bitmap objBMP = new System.Drawing.Bitmap(150, 60);
            Graphics objGraphics = System.Drawing.Graphics.FromImage(objBMP);

            objGraphics.Clear(BackGroundColor);


            // Instantiate object of brush with black color
            SolidBrush objBrush = new SolidBrush(RandomTextColor);

            Font objFont = null;
            int a;
            String myFont, str;

            //Creating an array for most readable yet cryptic fonts for OCR's
            // This is entirely up to developer's discretion
            String[] crypticFonts = new String[11];

            crypticFonts[0] = "Arial";
            crypticFonts[1] = "Verdana";
            crypticFonts[2] = "Comic Sans MS";
            crypticFonts[3] = "Impact";
            crypticFonts[4] = "Haettenschweiler";
            crypticFonts[5] = "Lucida Sans Unicode";
            crypticFonts[6] = "Garamond";
            crypticFonts[7] = "Courier New";
            crypticFonts[8] = "Book Antiqua";
            crypticFonts[9] = "Arial Narrow";
            crypticFonts[10] = "Estrangelo Edessa";

            //Loop to write the characters on image  
            // with different fonts.
            for (a = 0; a <= RandomWord.Length - 1; a++)
            {
                myFont = crypticFonts[new Random().Next(a)];
                objFont = new Font(myFont, 18, FontStyle.Bold | FontStyle.Italic | FontStyle.Strikeout);
                str = RandomWord.Substring(a, 1);
                objGraphics.DrawString(str, objFont, objBrush, a * 20, 20);
                objGraphics.Flush();
            }
            context.HttpContext.Response.ContentType = "image/GF";
            objBMP.Save(context.HttpContext.Response.OutputStream, ImageFormat.Gif);
            objFont.Dispose();
            objGraphics.Dispose();
            objBMP.Dispose();

        }
    }
}