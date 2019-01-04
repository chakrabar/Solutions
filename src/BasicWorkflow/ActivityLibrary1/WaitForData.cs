using System.Activities;

namespace ActivityLibrary1
{
    public sealed class WaitForData<T> : NativeActivity<T>
    {
        [RequiredArgument]
        public InArgument<string> BookmarkName { get; set; }

        //true: creates a bookmark and can go idla
        protected override bool CanInduceIdle
        {
            get { return true; }
        }

        protected override void Execute(NativeActivityContext context)
        {
            //do other stuffs then prepare for pause
            context.CreateBookmark(
                BookmarkName.Get(context),
                new BookmarkCallback(DataArrived));
        }

        private void DataArrived(NativeActivityContext context, Bookmark bookmark, object value)
        {
            //Result is OutArgument<T> automatically created
            //fill & validate the data in value
            Result.Set(context, value);
        }
    }
}
