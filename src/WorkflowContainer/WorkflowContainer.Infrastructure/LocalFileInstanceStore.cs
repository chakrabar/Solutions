using System;
using System.IO;
using System.Xml;
using WorkflowContainer.Infrastructure.InstanceStoreBase;

namespace WorkflowContainer.Infrastructure
{
    public class LocalFileInstanceStore : CustomInstanceStoreBase
    {
        private const string InstanceLocationTemplate =
            @"C:\ArghyaC\repos\Solutions\src\WorkflowContainer\InstanceStore\{0}.xml";

        public LocalFileInstanceStore()
            : base(new Guid("24c35e4c-1a2c-4096-912c-23836d2e93ee")) //static instance owner
        { }

        public override XmlDocument Load(Guid instanceId)
        {
            string fileName = string.Format(InstanceLocationTemplate, instanceId);
            try
            {
                using (FileStream inputStream = new FileStream(fileName, FileMode.Open))
                {
                    XmlReader rdr = XmlReader.Create(inputStream);
                    XmlDocument doc = new XmlDocument();
                    doc.Load(rdr);
                    return doc;
                }
            }
            catch (Exception exception)
            {
                throw new FileLoadException(exception.Message, fileName);
            }
        }

        public override void Save(Guid instanceId, XmlDocument doc)
        {
            string fileName = string.Format(InstanceLocationTemplate, instanceId);
            doc.Save(fileName);
        }
    }
}
