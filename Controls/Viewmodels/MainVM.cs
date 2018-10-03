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
            Chöre = new List<Chor> { new Chor { Id = 1, Name = "Großer Chor" }, new Chor { Id = 2, Name = "Jugendchor" }, new Chor { Id = 2, Name = "Kinderchor" } };
            Persons = new List<Person>();

            Random r = new Random();

            for (int i = 0; i < 1000; i++)
            {
                Persons.Add(new Person { Name = $"Name {i}", Chor = Chöre[r.Next(0, 2)], HasChanges = false });
            }
        }
        public bool PopupVisible { get { return _PopupVisible; } set { SetProperty(ref _PopupVisible, value); } }
        public RelayCommand ShowPopupCommand { get; private set; }

        private bool _PopupVisible;

        public List<Chor> Chöre { get; set; }
        public List<Person> Persons { get; set; }

    }
}
