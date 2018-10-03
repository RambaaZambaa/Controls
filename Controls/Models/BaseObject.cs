using Controls.Viewmodels;

namespace Controls.Models
{
    public class BaseObject : ViewModelBase
    {
        public int Id { get; set; }
        private string _Name;
        private bool _HasChanges;

        public string Name { get { return _Name; } set { SetProperty(ref _Name, value); } }
        public bool HasChanges { get { return _HasChanges; } set { SetProperty(ref _HasChanges, value); } }
    }
}
