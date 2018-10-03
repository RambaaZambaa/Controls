using System.Collections.Generic;

namespace Controls.Models
{
    public class Person : BaseObject
    {
        public Person()
        {

        }

        public Chor Chor { get { return _Chor; } set { if (SetProperty(ref _Chor, value)) HasChanges = true; } }


        private Chor _Chor;
    }
}
