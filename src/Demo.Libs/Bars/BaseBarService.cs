using System;

namespace Demo.Libs.Bars
{
    public class BaseBarService : IBarService
    {
        public BaseBarService()
        {
            this.InstanceId = Guid.NewGuid();
        }

        public Guid InstanceId { get; private set; }

        public override string ToString()
        {
            return this.GetType().Name + " : " + this.GetHashCode();
        }
    }
}