using System;

namespace GatewaySelector.Model
{
    public class GatewaySwitchModel
    {
        public string IpAddress { get; set; }
        public string IpAddressGateway { get; set; }

        public string ChangeGateway()
        {
            var result = new ExecuteCommand().RunCommand("Get-NetRoute -DestinationPrefix 0.0.0.0/0 | Select-Object -ExpandProperty NextHop");

            if (result != string.Empty)
                return result;
            else
                throw new Exception($"Error set gateway: {result}");
        }

        public string GetGateway()
        {
            var result = new ExecuteCommand().RunCommand("Get-NetRoute -DestinationPrefix 0.0.0.0/0 | Select-Object -ExpandProperty NextHop");

            if (result != string.Empty)
                return result;
            else
                throw new Exception($"Error get gateway address.");
        }

        public string GetIpAddress()
        {
            var result = new ExecuteCommand().RunCommand("((Get-WmiObject -Class Win32_NetworkAdapterConfiguration | where {$_.DHCPEnabled -ne $null -and $_.DefaultIPGateway -ne $null}).IPAddress | Where-Object { $_ -match '(\\d{1,3}\\.){3}\\d{1,3}' })[0]");

            if (result != string.Empty)
                return result;
            else
                throw new Exception($"Error get Ip address address.");
        }

        public bool SetGateway(Gateways gatewy)
        {
            var ipAddress = new Gateway()[gatewy];

            new ExecuteCommand().RunCommand($"ROUTE DELETE 0.0.0.0 {this.GetGateway()}");

            new ExecuteCommand().RunCommand($"ROUTE ADD 0.0.0.0  MASK 0.0.0.0 {ipAddress}");

            new Settings().Save(gatewy.ToString());

            return this.GetGateway() == ipAddress;
        }
    }

    public class Gateway
    {
        public string this[Gateways gateway]
        {
            get
            {
                switch (gateway)
                {
                    case Gateways.Orange:
                        return "192.168.1.1";
                    case Gateways.TMobile:
                        return "192.168.1.3";
                    default:
                        return string.Empty;
                }
            }
        }

        public Gateways this[string ipAddress]
        {
            get
            {
                switch (ipAddress)
                {
                    case "192.168.1.1":
                        return Gateways.Orange;
                    case "192.168.1.3":
                        return Gateways.TMobile;
                    default:
                        return Gateways.NONE;
                }
            }
        }
    }

    public enum Gateways
    {
        NONE = 0,
        Orange = 1,
        TMobile = 2
    }
}