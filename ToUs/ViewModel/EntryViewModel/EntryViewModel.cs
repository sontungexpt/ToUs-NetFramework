using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ToUs.Utilities;

namespace ToUs.ViewModel.EntryViewModel
{
    class EntryViewModel: ViewModelBase
    {
        public EntryViewModel() { }
        public ICommand CloseAppCommand { get; set; }
    }
}
