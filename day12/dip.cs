// Dependency Inversion Principle (DIP)
namespace SolidPrinciples
{
    public class dip
    {
        // Violation of DIP
        /*
        public class LightBulb
        {
            public void TurnOn() => Console.WriteLine("Light is on");
            public void TurnOff() => Console.WriteLine("Light is off");
        }

        public class Switch
        {
            private LightBulb bulb = new LightBulb();

            public void Operate()
            {
                bulb.TurnOn();
            }
        }
        */

        // DIP Applied
        public interface ISwitchable
        {
            void TurnOn();
            void TurnOff();
        }

        public class LightBulb : ISwitchable
        {
            public void TurnOn() => Console.WriteLine("Light is on");
            public void TurnOff() => Console.WriteLine("Light is off");
        }

        public class Switch
        {
            private ISwitchable device;

            public Switch(ISwitchable device)
            {
                this.device = device;
            }

            public void Operate()
            {
                device.TurnOn();
            }
        }

    }
}
