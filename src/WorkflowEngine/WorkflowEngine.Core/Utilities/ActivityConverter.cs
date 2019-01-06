using Microsoft.VisualBasic.Activities;
using System.Activities;

namespace WorkflowEngine.Core.Utilities
{
    public static class ActivityConverter
    {
        public static ActivityBuilder ToBuilder(DynamicActivity dynamicActivity)
        {
            var activityBuilder = new ActivityBuilder
            {
                Implementation = dynamicActivity.Implementation != null ? dynamicActivity.Implementation() : null,
                Name = dynamicActivity.Name
            };

            foreach (var item in dynamicActivity.Attributes)
                activityBuilder.Attributes.Add(item);

            foreach (var item in dynamicActivity.Constraints)
                activityBuilder.Constraints.Add(item);

            foreach (var item in dynamicActivity.Properties)
            {
                var property = new DynamicActivityProperty
                {
                    Name = item.Name,
                    Type = item.Type,
                    Value = null
                };

                foreach (var attribute in item.Attributes)
                    property.Attributes.Add(attribute);

                activityBuilder.Properties.Add(property);
            }

            VisualBasic.SetSettings(activityBuilder, VisualBasic.GetSettings(dynamicActivity));

            return activityBuilder;
        }
    }
}
