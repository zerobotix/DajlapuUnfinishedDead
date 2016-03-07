using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace DajLapu.Web.Helpers
{
    public static class EnumHelper
    {
        public static string GenerateJavaScriptVariable(IList<Type> enumTypes)
        {
            var sb = new StringBuilder("var Enums = Object.freeze({ ");
            foreach (var type in enumTypes)
            {
                sb.Append(type.Name + ": { ");
                foreach (var value in type.GetEnumValues())
                {
                    sb.AppendFormat("{0}: {1}, ", Enum.GetName(type, value), (int)value);
                }
                sb.Append("}, ");
            }
            sb.Append("});");
            var result = sb.ToString();

            return result;
        }
    }
}