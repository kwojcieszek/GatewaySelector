using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

namespace GatewaySelector.ViewModel
{
    public class GatewaySwitchViewModel : INotifyPropertyChanged
    {
        private Model.GatewaySwitchModel _gatewaySwitchModel = new Model.GatewaySwitchModel();
        private ICommand _setGatewayTMobile = null;
        private ICommand _setGatewayOrange = null;

        public event PropertyChangedEventHandler PropertyChanged;

        public string IpAddress
        {
            get => this._gatewaySwitchModel.GetIpAddress();
            set
            {
                this._gatewaySwitchModel.IpAddress = value;
                onPropertyChange(nameof(IpAddress));
            }
        }

        public string IpAddressGateway
        {
            get
            {
                var ipAddress = this._gatewaySwitchModel.GetGateway();

                return $"{ipAddress} {new Model.Gateway()[ipAddress]}";
            }
            set
            {
                this._gatewaySwitchModel.IpAddressGateway = value;
                onPropertyChange(nameof(IpAddressGateway));
            }
        }

        private ICommand SetGateway(Model.Gateways gateway, ref ICommand command)
        {
            if (command == null)
                command = new RelayCommand(
                  (object o) =>
                  {
                      this._gatewaySwitchModel.SetGateway(gateway);
                      onPropertyChange(nameof(IpAddressGateway));

                      MessageBox.Show($"Zmieniono bramę na: {gateway} {new Model.Gateway()[gateway]}", "Informacja", MessageBoxButton.OK, MessageBoxImage.Information);
                  });

            return command;
        }

        public ICommand SetGatewayTMobile
        {
            get => this.SetGateway(Model.Gateways.TMobile, ref this._setGatewayTMobile);
        }

        public ICommand SetGatewayOrange
        {
            get => this.SetGateway(Model.Gateways.Orange, ref this._setGatewayOrange);
        }

        private void onPropertyChange(string name)
        {
            if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs(name));
        }
    }
}