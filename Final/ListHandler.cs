using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final
{
    public sealed class ListHandler
    {
        public ObservableCollection<Manager> ContactList { get; set; }

        private ListHandler()
        {

        }
        static readonly ListHandler instance = new ListHandler();

        public static ListHandler Instance
        {
            get { return instance; }
        }
        

    }
}
