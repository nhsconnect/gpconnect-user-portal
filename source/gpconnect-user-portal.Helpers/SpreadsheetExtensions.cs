using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Spreadsheet;

namespace gpconnect_user_portal.Helpers
{
    public static class SpreadsheetExtensions
    {
        public static EnumValue<CellValues> GetCellDataType(this string cellValue)
        {
            if (int.TryParse(cellValue, out _))
            {
                return CellValues.Number;
            }
            return CellValues.String;
        }
    }
}
