namespace Morpher.Kernel
{
    /// <summary>
    /// Abstract base class for defining mapping profiles.
    /// </summary>
    public abstract class BaseProfile
    {
        /// <summary>
        /// Configures mappings by registering them with the MorpherManager.
        /// </summary>
        /// <param name="manager">MorpherManager instance to configure mappings.</param>
        public abstract void ConfigureMappings(MorpherManager manager);
    }
}
