namespace GatewaySelector
{
    public class Start
    {
        public void SetGateway()
        {
            var gateway = new Settings().Read();

            if (gateway == null)
                return;

            switch (gateway)
            {
                case "Orange":
                    new Model.GatewaySwitchModel().SetGateway(Model.Gateways.Orange);
                    break;
                case "TMobile":
                    new Model.GatewaySwitchModel().SetGateway(Model.Gateways.TMobile);
                    break;
            }
        }
    }
}
