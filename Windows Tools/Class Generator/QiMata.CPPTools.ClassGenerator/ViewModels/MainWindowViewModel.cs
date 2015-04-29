using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using QiMata.CPPTools.RazorTextGenerator.Models;

namespace QiMata.CPPTools.ClassGenerator.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private RazorTextGenerator.ClassGenerator _classGenerator;

        public MainWindowViewModel()
        {
            _classGenerator = new RazorTextGenerator.ClassGenerator();
        }

        private ICommand _addPropertyClickedCommand;

        public ICommand AddPropertyClickedCommand
        {
            get { return _addPropertyClickedCommand ?? (_addPropertyClickedCommand = new Command(() => AddPropertyVisible = !AddPropertyVisible)); }
        }

        private bool _addPropertyVisible;

        public bool AddPropertyVisible
        {
            get { return _addPropertyVisible; }
            set
            {
                _addPropertyVisible = value;
                RaisePropertyChanged(() => this.AddPropertyVisible);
            }
        }


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

        private string _generatedCode;

        public string GeneratedCode
        {
            get
            {
                return _generatedCode;
            }
            set
            {
                _generatedCode = value;
                RaisePropertyChanged(() => this.GeneratedCode);
            }
        }



        public override void RaisePropertyChanged(string propertyName)
        {
            base.RaisePropertyChanged(propertyName);

            if ((propertyName == "ClassName" || propertyName == "Namespace") && !String.IsNullOrWhiteSpace(this.ClassName) && !String.IsNullOrWhiteSpace(this.Namespace))
            {
                GeneratedCode = _classGenerator.GenerateClass(new CPPTypeModel
                {
                    ClassName = this.ClassName,
                    Namespaces = this.Namespace.Split(new string[]{ "::", "."},StringSplitOptions.RemoveEmptyEntries)
                });
            }
        }

        class Command : ICommand
        {
            private Action _action;

            public Command(Action action)
            {
                _action = action;
            }

            public bool CanExecute(object parameter)
            {
                return true;
            }

            public event EventHandler CanExecuteChanged;

            public void Execute(object parameter)
            {
                _action();
            }
        }
    }
}
