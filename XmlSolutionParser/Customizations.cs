using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Alex.Net.Crm.SolutionCompare.Parser.Objects;
using System.IO;

namespace Alex.Net.Crm.SolutionCompare.Parser
{
    public class Customizations
    {

        public List<Entity> Entities { get; private set; }

        public static Customizations Create(XDocument customizationsDocument, int defaultLanguageCode)
        {
            Customizations customizations = new Customizations();
            customizations.Entities = ParseEntities(customizationsDocument.Element("Entities"), defaultLanguageCode);
            return customizations;
        }

        private static List<Entity> ParseEntities(XElement entitiesElement, int defaultLanguageCode)
        {
            List<Entity> entityList = new List<Entity>();
            foreach (XElement entityElement in entitiesElement.Elements("Entity"))
            {
                Entity e = new Entity();
                e.LogicalName = entityElement.Element("Name").Value;
                e.ObjectTypeCode = int.Parse(entityElement.Element("ObjectTypeCode").Value);

                if (entityElement.Element("EntityInfo") != null)
                {
                    ParseEntityInfo(ref e, entityElement.Element("EntityInfo"), defaultLanguageCode);
                }
                
            }
            return entityList;
        }

        private static void ParseEntityInfo(ref Entity e, XElement entityInfoElement, int defaultLanguageCode)
        {
            XElement entityInfo = entityInfoElement.Element("entity");

            e.Name = Util.ParseLocalizedLabelElement(entityInfo.Element("LocalizedNames"), defaultLanguageCode);
            e.CollectionName = Util.ParseLocalizedLabelElement(entityInfo.Element("LocalizedCollectionNames"), defaultLanguageCode);
            e.Description = Util.ParseLocalizedLabelElement(entityInfo.Element("Descriptions"), defaultLanguageCode);
        }

        private Customizations()
        {
            
        }
    }
}
