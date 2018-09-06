using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Linq;
using System.Xml.Linq;

namespace PrivateLabelLite.Framework.Helper
{
    public static class XmlHelper
    {
        public static string AttributeValue(this XAttribute attrib)
        {
            return attrib == null ? "" : attrib.Value;
        }
        public static string ElementValue(this XElement ele)
        {
            return ele == null ? "" : ele.Value;
        }

        
    }
}
