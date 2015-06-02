using QiMata.CPPTools.RazorTextGenerator.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace QiMata.CPPTools.ClassGenerator.ViewModels
{
    class PropertyViewModel : Property, INotifyPropertyChanged
    {
        private bool _radioButton1Checked;
        public bool RadioButton1Checked
        {
            get
            {
                return _radioButton1Checked;
            }
            set
            {
                _radioButton1Checked = value;
                base.GetterAccessModifier = AccessModifier.Public;
                RaisePropertyChanged(() => this.RadioButton1Checked);
            }
        }

        private bool _radioButton2Checked;
        public bool RadioButton2Checked
        {
            get
            {
                return _radioButton2Checked;
            }
            set
            {
                _radioButton2Checked = value;
                base.GetterAccessModifier = AccessModifier.Protected;
                RaisePropertyChanged(() => this.RadioButton2Checked);
            }
        }

        private bool _radioButton3Checked;
        public bool RadioButton3Checked
        {
            get
            {
                return _radioButton3Checked;
            }
            set
            {
                _radioButton3Checked = value;
                base.GetterAccessModifier = AccessModifier.Private;
                RaisePropertyChanged(() => this.RadioButton3Checked);
            }
        }

        private bool _radioButton4Checked;
        public bool RadioButton4Checked
        {
            get
            {
                return _radioButton4Checked;
            }
            set
            {
                _radioButton4Checked = value;
                base.SetterAccessModifier = AccessModifier.Public;
                RaisePropertyChanged(() => this.RadioButton4Checked);
            }
        }

        private bool _radioButton5Checked;
        public bool RadioButton5Checked
        {
            get
            {
                return _radioButton5Checked;
            }
            set
            {
                _radioButton5Checked = value;
                base.SetterAccessModifier = AccessModifier.Protected;
                RaisePropertyChanged(() => this.RadioButton5Checked);
            }
        }

        private bool _radioButton6Checked;
        public bool RadioButton6Checked
        {
            get
            {
                return _radioButton6Checked;
            }
            set
            {
                _radioButton6Checked = value;
                base.SetterAccessModifier = AccessModifier.Private;
                RaisePropertyChanged(() => this.RadioButton6Checked);
            }
        }

        private string _propertyName;
        public string PropertyName
        {
            get
            {
                return _propertyName;
            }
            set
            {
                _propertyName = value;
                Name = value;
                RaisePropertyChanged(() => this.PropertyName);
            }
        }

        private string _propertyTypeName;
        public string PropertyTypeName {
            get
            {
                return _propertyTypeName;
            }
            set
            {
                _propertyTypeName = value;
                TypeName = value;
                RaisePropertyChanged(() => this.PropertyTypeName);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void RaisePropertyChanged<TProperty>(Expression<Func<TProperty>> propertyExpression)
        {
            Type type = this.GetType();

            MemberExpression member = propertyExpression.Body as MemberExpression;
            if (member == null)
                throw new ArgumentException(string.Format(
                    "Expression '{0}' refers to a method, not a property.",
                    propertyExpression.ToString()));

            PropertyInfo propInfo = member.Member as PropertyInfo;
            if (propInfo == null)
                throw new ArgumentException(string.Format(
                    "Expression '{0}' refers to a field, not a property.",
                    propertyExpression.ToString()));

            RaisePropertyChanged(propInfo.Name);
        }

        public virtual void RaisePropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
