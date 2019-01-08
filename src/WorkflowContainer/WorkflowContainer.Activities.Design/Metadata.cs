using System.Activities.Presentation.Metadata;
using System.ComponentModel;
using WorkflowContainer.Activities.Design.Properties;

namespace WorkflowContainer.Activities.Design
{
    public class Metadata : IRegisterMetadata
    {
        public void Register()
        {
            CategoryAttribute myLocalCategoryAttribute =
                new CategoryAttribute($"{Strings.CategoryCustom}.{Strings.CategoryMy}");

            AttributeTableBuilder attributeTableBuilder = new AttributeTableBuilder();

            // Register Designers, display name, category etc.
            //per activity design
            attributeTableBuilder.AddCustomAttributes(typeof(RequestHumanInput<>), new DesignerAttribute(typeof(RequestHumanInputDesigner)));
            attributeTableBuilder.AddCustomAttributes(typeof(RequestHumanInput<>), new DisplayNameAttribute(Strings.RequestHumanInputDisplayName));
            attributeTableBuilder.AddCustomAttributes(typeof(RequestHumanInput<>), myLocalCategoryAttribute);

            // Apply the metadata
            MetadataStore.AddAttributeTable(attributeTableBuilder.CreateTable());
        }
    }
}
