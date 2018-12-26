using System;
using System.Activities;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Windows.Markup;

namespace ActivityLibrary1
{
    public class WithValidation : CodeActivity
    {

        [RequiredArgument]
        public InArgument<string> TwitterHandle { get; set; }

        [RequiredArgument]
        [OverloadGroup("User")]
        public InArgument<User> User { get; set; }

        [RequiredArgument]
        [OverloadGroup("UserId")]
        public InArgument<int> UserId { get; set; }

        [RequiredArgument]
        [OverloadGroup("Address")]
        public InArgument<string> StreetAddress { get; set; }

        [RequiredArgument]
        [OverloadGroup("Address")]
        public InArgument<string> City { get; set; }

        [RequiredArgument]
        [OverloadGroup("Address")]
        public InArgument<string> PostalCode { get; set; }

        [RequiredArgument]
        [OverloadGroup("Location")]
        public InArgument<decimal> Longitude { get; set; }

        [RequiredArgument]
        [OverloadGroup("Location")]
        public InArgument<decimal> Latitude { get; set; }

        protected override void Execute(CodeActivityContext context)
        {
            throw new NotImplementedException();
        }
    }

    public class DbUpdate : AsyncCodeActivity
    {
        [RequiredArgument]
        [OverloadGroup("ConnectionString")]
        [DefaultValue(null)]
        public InArgument<string> ProviderName { get; set; }

        [RequiredArgument]
        [OverloadGroup("ConnectionString")]
        [DependsOn("ProviderName")]
        [DefaultValue(null)]
        public InArgument<string> ConnectionString { get; set; }

        [RequiredArgument]
        [OverloadGroup("ConfigFileSectionName")]
        [DefaultValue(null)]
        public InArgument<string> ConfigName { get; set; }

        [DefaultValue(null)]
        public CommandType CommandType { get; set; }

        [RequiredArgument]
        public InArgument<string> Sql { get; set; }

        [DependsOn("Sql")]
        [DefaultValue(null)]
        public IDictionary<string, Argument> Parameters { get; }

        [DependsOn("Parameters")]
        public OutArgument<int> AffectedRecords { get; set; }

        protected override IAsyncResult BeginExecute(AsyncCodeActivityContext context, AsyncCallback callback, object state)
        {
            throw new NotImplementedException();
        }

        protected override void EndExecute(AsyncCodeActivityContext context, IAsyncResult result)
        {
            throw new NotImplementedException();
        }
    }
}
