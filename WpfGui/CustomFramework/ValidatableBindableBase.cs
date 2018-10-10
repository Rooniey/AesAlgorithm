using FluentValidation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;

namespace WpfGui.CustomFramework
{
    public abstract class ValidatableBindableBase<T> : BindableBase, INotifyDataErrorInfo
    {
        protected ValidatableBindableBase(AbstractValidator<T> validator)
        {
            _validator = validator;
        }

        private readonly AbstractValidator<T> _validator;

        private readonly Dictionary<string, List<string>> _errors = new Dictionary<string, List<string>>();

        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged = delegate { };

        public System.Collections.IEnumerable GetErrors(string propertyName)
        {
            if (string.IsNullOrEmpty(propertyName))
            {
                return null;
            }

            return _errors.ContainsKey(propertyName) ? _errors[propertyName] : null;
        }

        public bool HasErrors => _errors.Count > 0;

        protected override void SetProperty<TValue>(ref TValue member, TValue val, [CallerMemberName] string propertyName = null)
        {
            base.SetProperty(ref member, val, propertyName);
            ValidateProperty(propertyName, val);
        }

        private void ValidateProperty<TValue>(string propertyName, TValue value)
        {
            ValidationContext<T> context = ProvideContext(propertyName);
            var results = _validator.Validate(context);

            if (results.Errors.Any())
            {
                _errors[propertyName] = results.Errors.Select(c => c.ErrorMessage).ToList();
            }
            else
            {
                _errors.Remove(propertyName);
            }
            ErrorsChanged(this, new DataErrorsChangedEventArgs(propertyName));
        }

        protected abstract ValidationContext<T> ProvideContext(string propertyName);
    }
}