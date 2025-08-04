// Liskov Substitution Principle (LSP)
namespace SolidPrinciples
{
    public class lsp
    {
        // Violation of LSP
        /*
        public class Bird
        {
            public virtual void Fly()
            {
                Console.WriteLine("Flying");
            }
        }

        public class Ostrich : Bird
        {
            public override void Fly()
            {
                throw new NotImplementedException(); // Ostrich can't fly
            }
        }*/

        // LSP Applied
        public abstract class Bird
        {
            public abstract void Move();
        }

        public class Sparrow : Bird
        {
            public override void Move() => Console.WriteLine("Flying");
        }

        public class Ostrich : Bird
        {
            public override void Move() => Console.WriteLine("Running");
        }

    }
}
