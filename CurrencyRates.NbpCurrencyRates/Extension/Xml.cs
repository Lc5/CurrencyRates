﻿using System.IO;
using System.Xml.Linq;
using System.Xml.Schema;

namespace CurrencyRates.NbpCurrencyRates.Extension
{
    static class Xml
    {
        public static void Validate(this XDocument xml, XmlSchema xmlSchema)
        {
            var schemas = new XmlSchemaSet();
            schemas.Add(xmlSchema);
            xml.Validate(schemas, null);
        }

        public static void Validate(this XDocument xml, string schema)
        {
            Validate(xml, BuildXmlSchema(schema));
        }

        static XmlSchema BuildXmlSchema(string schema)
        {
            return XmlSchema.Read(new StringReader(schema), null);
        }
    }
}
