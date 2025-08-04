// Interface Segregation Principle (ISP)
namespace SolidPrinciples
{
    public class isp
    {
        // Violation of ISP
        /*
        public interface IWorker
        {
            void Work();
            void Eat();
        }
        */
        // Applied ISP
        public interface IWorkable
        {
            void Work();
        }

        public interface IFeedable
        {
            void Eat();
        }

        public class HumanWorker : IWorkable, IFeedable
        {
            public void Work() => Console.WriteLine("Working");
            public void Eat() => Console.WriteLine("Eating");
        }

        public class RobotWorker : IWorkable
        {
            public void Work() => Console.WriteLine("Working");
        }

    }
}
