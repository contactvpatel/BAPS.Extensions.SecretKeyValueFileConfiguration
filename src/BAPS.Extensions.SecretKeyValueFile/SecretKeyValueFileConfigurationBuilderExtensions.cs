using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.FileProviders;

namespace BAPS.Extensions.SecretKeyValueFileConfiguration
{
    /// <summary>
    /// Extension methods for registering <see cref="SecretKeyValueFileConfigurationProvider"/> with <see cref="IConfigurationBuilder"/>.
    /// </summary>
    public static class SecretKeyValueFileConfigurationBuilderExtensions
    {
        /// <summary>
        /// Adds configuration using key/value file (application.properties) file from directory (/vault/secrets).
        /// </summary>
        /// <param name="builder">The <see cref="IConfigurationBuilder"/> to add to.</param>
        /// <param name="directoryPath">The path to the directory.</param>
        /// <returns>The <see cref="IConfigurationBuilder"/>.</returns>
        /// 
        public static IConfigurationBuilder AddSecretKeyValueFile(this IConfigurationBuilder builder, string directoryPath)
            => builder.AddSecretKeyValueFile(directoryPath, optional: false, reloadOnChange: false);

        /// <summary>
        /// Adds configuration using key/value file (application.properties) file from directory (/vault/secrets).
        /// </summary>
        /// <param name="builder">The <see cref="IConfigurationBuilder"/> to add to.</param>
        /// <param name="directoryPath">The path to the directory.</param>
        /// <param name="optional">Whether the directory is optional.</param>
        /// <returns>The <see cref="IConfigurationBuilder"/>.</returns>
        public static IConfigurationBuilder AddSecretKeyValueFile(this IConfigurationBuilder builder, string directoryPath, bool optional)
            => builder.AddSecretKeyValueFile(directoryPath, optional, reloadOnChange: false);

        /// <summary>
        /// Adds configuration using key/value file (application.properties) file from directory (/vault/secrets).
        /// </summary>
        /// <param name="builder">The <see cref="IConfigurationBuilder"/> to add to.</param>
        /// <param name="directoryPath">The path to the directory.</param>
        /// <param name="optional">Whether the directory is optional.</param>
        /// <param name="reloadOnChange">Whether the configuration should be reloaded if the files are changed, added or removed.</param>
        /// <returns>The <see cref="IConfigurationBuilder"/>.</returns>
        public static IConfigurationBuilder AddSecretKeyValueFile(this IConfigurationBuilder builder, string directoryPath, bool optional, bool reloadOnChange)
            => builder.AddSecretKeyValueFile(source =>
            {
                // Only try to set the file provider if its not optional or the directory exists 
                if (!optional && Directory.Exists(directoryPath))
                {
                    source.FileProvider = new PhysicalFileProvider(directoryPath);
                }
                source.Optional = optional;
                source.ReloadOnChange = reloadOnChange;
            });

        /// <summary>
        /// Adds configuration using key/value file (application.properties) file from directory (/vault/secrets).
        /// </summary>
        /// <param name="builder">The <see cref="IConfigurationBuilder"/> to add to.</param>
        /// <param name="configureSource">Configures the source.</param>
        /// <returns>The <see cref="IConfigurationBuilder"/>.</returns>
        public static IConfigurationBuilder AddSecretKeyValueFile(this IConfigurationBuilder builder, Action<SecretKeyValueFileConfigurationSource> configureSource)
            => builder.Add(configureSource);
    }
}
