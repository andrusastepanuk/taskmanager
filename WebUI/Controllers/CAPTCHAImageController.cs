using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebUI.Infrastructure;
using System.Text;
using System.Drawing;

namespace WebUI.Controllers
{
    public class CAPTCHAImageController : Controller
    {
        public CAPTCHAImageActionResult Index()
        {
            string randomText = SelectRandomWord(6);
            HttpContext.Session["RandomText"] = randomText;
            return new CAPTCHAImageActionResult() { BackGroundColor = Color.LightGray, RandomTextColor = Color.Black, RandomWord = randomText };

        }
        private string SelectRandomWord(int numberOfChars)
        {
            if (numberOfChars > 36)
            {
                throw new InvalidOperationException("Random Word Charecters can not be greater than 36.");
            }
            // Creating an array of 26 characters  and 0-9 numbers
            char[] columns = new char[36];

            for (int charPos = 65; charPos < 65 + 26; charPos++)
                columns[charPos - 65] = (char)charPos;

            for (int intPos = 48; intPos <= 57; intPos++)
                columns[26 + (intPos - 48)] = (char)intPos;

            StringBuilder randomBuilder = new StringBuilder();
            // pick charecters from random position
            Random randomSeed = new Random();
            for (int incr = 0; incr < numberOfChars; incr++)
            {
                randomBuilder.Append(columns[randomSeed.Next(36)].ToString());

            }

            return randomBuilder.ToString();
        }    

    }
}
