using System.Data;
using System.Text;
using System.Linq;

namespace gpconnect_user_portal.Helpers
{
    public static class HtmlExtensions
    {  
        public static string ExportDataTableToHTML(this DataTable dataTable)
        {
            var htmlBuilder = new StringBuilder();

            htmlBuilder.Append("<table border='1' cellpadding='10' cellspacing='0'>");
            htmlBuilder.Append("<thead>");
            htmlBuilder.Append("<tr>");
            foreach (DataColumn myColumn in dataTable.Columns)
            {
                htmlBuilder.Append("<th>");
                htmlBuilder.Append(myColumn.ColumnName);
                htmlBuilder.Append("</th>");

            }
            htmlBuilder.Append("</tr>");
            htmlBuilder.Append("</thead>");
            htmlBuilder.Append("<tbody>");

            foreach (DataRow myRow in dataTable.Rows)
            {
                htmlBuilder.Append("<tr>");
                foreach (DataColumn myColumn in dataTable.Columns)
                {
                    htmlBuilder.Append("<td>");
                    htmlBuilder.Append(myRow[myColumn.ColumnName].ToString());
                    htmlBuilder.Append("</td>");
                }
                htmlBuilder.Append("</tr>");
            }
            htmlBuilder.Append("</tbody>");
            htmlBuilder.Append("</table>");
            return htmlBuilder.ToString();
        }
    }
}
