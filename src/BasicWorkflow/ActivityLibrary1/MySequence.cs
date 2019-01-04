using System;
using System.Activities;
using System.Collections.ObjectModel;

namespace ActivityLibrary1
{
    public class MySequence : NativeActivity
    {
        Collection<Activity> _children;
        //exposed variables that can be set from outside (e.g. developer)
        Collection<Variable> _variables;

        //implementation variable - internal variable
        Variable<int> _currentindex;
        CompletionCallback _completed;

        public Collection<Activity> Activities
        {
            get { return _children; }
        }
        public Collection<Variable> Variables
        {
            get { return _variables; }
        }

        public MySequence()
        {
            _children = new Collection<Activity>();
            _variables = new Collection<Variable>();
            _currentindex = new Variable<int>();

            _completed = new CompletionCallback(Child_complete);
        }

        //what actually it is doing!!!
        protected override void CacheMetadata(NativeActivityMetadata metadata)
        {
            //base.CacheMetadata(metadata);
            //these variables needs to be managed & persisted
            metadata.SetVariablesCollection(_variables);
            //these activities need to be managed
            metadata.SetChildrenCollection(_children);
            //this internal variable needs to be managed by runtime
            metadata.AddImplementationVariable(_currentindex);
        }
        //since they are cached to metadata, they will be available irrespective 
        //of whether they are pesristed, pasued-resumed or whatever

        private void Child_complete(NativeActivityContext context, ActivityInstance completedInstance)
        {
            var index = _currentindex.Get(context);
            _currentindex.Set(context, ++index);

            //sequenctial, scheduling next activity only when previous one is completed
            if (index < _children.Count)
                context.ScheduleActivity(_children[index], _completed);
        }

        protected override void Execute(NativeActivityContext context)
        {
            _currentindex.Set(context, 0);
            if (_children.Count > 0)
                context.ScheduleActivity(_children[0], _completed);
        }
    }
}
