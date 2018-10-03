using Controls.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controls.Viewmodels
{
    public class MainVM : ViewModelBase
    {
        public MainVM()
        {
            ShowPopupCommand = new RelayCommand(p => PopupVisible = !PopupVisible);

            for (int i = 0; i < 1000; i++)
            {
                Chöre.Add(new Chor { Name = $"Chor {i}", HasChanges = false });
            }

            Random r = new Random();

            for (int i = 0; i < 10000; i++)
            {
                Persons.Add(new Person { Name = $"Name {i}", Chor = Chöre[r.Next(0, 1000)], HasChanges = false });
            }

        }
        public bool PopupVisible { get { return _PopupVisible; } set { SetProperty(ref _PopupVisible, value); } }
        public RelayCommand ShowPopupCommand { get; private set; }

        private bool _PopupVisible;

        public List<Chor> Chöre { get; set; } = new List<Chor>();
        public List<Person> Persons { get; set; } = new List<Person>();

    }
}
