using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QiMata.CPPTools.ClassGenerator.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private string _namespace;

        public string Namespace
        {
            get
            {
                return _namespace;
            }
            set
            {
                _namespace = value;
                RaisePropertyChanged(() => this.Namespace);
            }
        }

        private string _className;

        public string ClassName
        {
            get
            {
                return _className;
            }
            set
            {
                _className = value;
                RaisePropertyChanged(() => this.ClassName);
            }
        }
    }
}
