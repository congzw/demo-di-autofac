using System;

namespace Demo.Libs
{
    public class EmptyService : IDisposable
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public override string ToString()
        {
            return this.Id.ToString();
        }

        public void Dispose()
        {
            Console.WriteLine("Dispose >>> " + this.GetHashCode() );
        }
    }
}
