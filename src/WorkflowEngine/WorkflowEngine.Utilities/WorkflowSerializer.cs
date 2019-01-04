using System.Activities;
using System.Activities.XamlIntegration;
using System.IO;
using System.Text;
using System.Xaml;
using System.Xml;

namespace WorkflowEngine.Utilities
{
    public static class WorkflowSerializer
    {
        private static ActivityBuilder GetAsActivityBuilder(Activity activity)
        {
            return new ActivityBuilder
            {
                Name = nameof(activity),
                Implementation = activity
            };
        }

        public static string ToXamlString2(this ActivityBuilder ab)
        {
            StringBuilder xaml = new StringBuilder();
            using (XmlWriter xmlWriter = XmlWriter.Create(xaml, new XmlWriterSettings { Indent = true, OmitXmlDeclaration = true }))
            using (XamlWriter xamlWriter = new XamlXmlWriter(xmlWriter, new XamlSchemaContext()))
            using (XamlWriter xamlServicesWriter = ActivityXamlServices.CreateBuilderWriter(xamlWriter))
            {
                XamlServices.Save(xamlServicesWriter, ab);
            }
            return xaml.ToString();
        }

        public static string ToXamlString2<T>(this ActivityBuilder<T> ab)
        {
            StringBuilder xaml = new StringBuilder();
            using (XmlWriter xmlWriter = XmlWriter.Create(xaml, new XmlWriterSettings { Indent = true, OmitXmlDeclaration = true }))
            using (XamlWriter xamlWriter = new XamlXmlWriter(xmlWriter, new XamlSchemaContext()))
            using (XamlWriter xamlServicesWriter = ActivityXamlServices.CreateBuilderWriter(xamlWriter))
            {
                XamlServices.Save(xamlServicesWriter, ab);
            }
            return xaml.ToString();
        }

        public static string ToXamlString2(this Activity activity)
        {
            StringBuilder xaml = new StringBuilder();
            using (XmlWriter xmlWriter = XmlWriter.Create(xaml, new XmlWriterSettings { Indent = true, OmitXmlDeclaration = true }))
            using (XamlWriter xamlWriter = new XamlXmlWriter(xmlWriter, new XamlSchemaContext()))
            using (XamlWriter xamlServicesWriter = ActivityXamlServices.CreateBuilderWriter(xamlWriter))
            {
                ActivityBuilder ab = GetAsActivityBuilder(activity);
                XamlServices.Save(xamlServicesWriter, ab);
            }
            return xaml.ToString();
        }

        public static void WriteToXamlFile3(this Activity activity, string filepath)
        {
            XamlServices.Save(filepath, activity);
        }

        public static void WriteToXamlFile4(this Activity activity, string filepath)
        {
            // Serialize the workflow to XAML and save it to a file.  
            StreamWriter sw = File.CreateText(filepath);
            XamlWriter xw2 = ActivityXamlServices.CreateBuilderWriter(new XamlXmlWriter(sw, new XamlSchemaContext()));
            XamlServices.Save(xw2, activity);
            sw.Close();
        }

        #region options

        public static string ToXamlString(this ActivityBuilder ab)
        {
            // Serialize the workflow to XAML and store it in a string.  
            StringBuilder sb = new StringBuilder();
            using (StringWriter tw = new StringWriter(sb))
            using(XamlWriter xw = ActivityXamlServices.CreateBuilderWriter(new XamlXmlWriter(tw, new XamlSchemaContext())))
            {                
                XamlServices.Save(xw, ab);
                string serializedAB = sb.ToString();
                return serializedAB;
            }
        }

        public static string ToXamlString(this Activity activity)
        {
            ActivityBuilder activityBuilder = GetAsActivityBuilder(activity);
            return activityBuilder.ToXamlString();
        }

        public static void WriteToXamlFile(this ActivityBuilder ab, string filepath)
        {
            // Serialize the workflow to XAML and save it to a file.  
            using (StreamWriter sw = File.CreateText(filepath))
            using (XamlWriter xw = ActivityXamlServices.CreateBuilderWriter(new XamlXmlWriter(sw, new XamlSchemaContext())))
            {
                XamlServices.Save(xw, ab);
            }
        }

        public static void WriteToXamlFile(this Activity activity, string filepath)
        {
            ActivityBuilder activityBuilder = GetAsActivityBuilder(activity);
            activityBuilder.WriteToXamlFile(filepath);
        }

        public static void WriteToXamlFile2(this ActivityBuilder ab, string filepath)
        {
            var xaml = ab.ToXamlString2();
            File.WriteAllText(filepath, xaml);
        }

        public static void WriteToXamlFile2<T>(this ActivityBuilder<T> ab, string filepath)
        {
            var xaml = ab.ToXamlString2();
            File.WriteAllText(filepath, xaml);
        }

        public static void WriteToXamlFile2(this Activity activity, string filepath)
        {
            var xaml = activity.ToXamlString2();
            File.WriteAllText(filepath, xaml);
        }

        public static string ToXaml(this Activity activity)
        {
            StringBuilder xaml = new StringBuilder();
            using (XmlWriter xmlWriter = XmlWriter.Create(xaml, new XmlWriterSettings { Indent = true, OmitXmlDeclaration = true}))
            using (XamlWriter xamlWriter = new XamlXmlWriter(xmlWriter, new XamlSchemaContext()))
            using (XamlWriter xamlServicesWriter = ActivityXamlServices.CreateBuilderWriter(xamlWriter))
            {
                ActivityBuilder activityBuilder = GetAsActivityBuilder(activity);
                XamlServices.Save(xamlServicesWriter, activityBuilder);
            }
            return xaml.ToString();
        }

        #endregion
    }
}
