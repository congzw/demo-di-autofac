using System;

namespace Demo.Libs.Foos
{
    /// <summary>
    /// Base class for dependencies. Used simply to avoid redundant code; it's not
    /// actually required to have a common derivation chain.
    /// </summary>
    public class BaseFooService : IFooService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BaseFooService"/> class.
        /// </summary>
        public BaseFooService()
        {
            this.InstanceId = Guid.NewGuid();
        }

        /// <summary>
        /// Gets the unique instance ID for the dependency.
        /// </summary>
        /// <value>
        /// A <see cref="System.Guid"/> that indicates the unique ID for the
        /// instance.
        /// </value>
        public Guid InstanceId { get; private set; }

        public override string ToString()
        {
            return this.GetType().Name + " : " + this.GetHashCode();
        }
    }
}
