using Caliburn.Micro;

namespace WpfGui.ViewModels
{
    public class ShellViewModel
    {
        private ILog _logger = LogManager.GetLog(typeof(ShellViewModel));

        public AesFormViewModel AesFormViewModel { get; }

        public ShellViewModel(AesFormViewModel aesFormViewModel)
        {
            _logger.Info("Initializing shell view model");
            AesFormViewModel = aesFormViewModel;
        }
    }
}