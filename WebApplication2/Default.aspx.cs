using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication2
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var asm = System.Reflection.Assembly.GetExecutingAssembly();
            var version = asm.GetName().Version;

            int days = version.Build;
            int seconds = version.Revision * 2;

            DateTime baseDate = new DateTime(2000, 1, 1);
            DateTime buildDate = baseDate.AddDays(days);
            DateTime buildDateTime = buildDate.AddSeconds(seconds);

            StringBuilder sb = new StringBuilder();
            sb.Append("ビルド日時：");
            sb.Append(buildDateTime.ToString("yyyy'/'MM'/'dd' 'HH':'mm':'ss"));

            Label1.Text = sb.ToString();

        }
    }
}