
using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using ToUs.Resources.CustomControl;
using ToUs.Utilities;

namespace ToUs.ViewModel.PreviewViewModel
{
    public class PreviewViewModel : ViewModelBase
    {
        public ObservableCollection<Class> _class;

        public PreviewViewModel()
        {
            _class = new ObservableCollection<Class>();
            _class.Add(new Class(2, "12"));
            _class.Add(new Class(3, "23"));
            _class.Add(new Class(5, "34"));
            _class.Add(new Class(6, "678"));
            _class.Add(new Class(2, "34"));
            _class.Add(new Class(4, "45"));
            _class.Add(new Class(4, "678"));
        }
           
    }

    public class Class
    {
        public int day { get; set; }
        public string lesson { get; set; }

        public Class(int day, string lesson)
        {
            this.day = day;
            this.lesson = lesson;
        }
    }

   
}
