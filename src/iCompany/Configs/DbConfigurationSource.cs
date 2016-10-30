using Microsoft.Extensions.Configuration;

namespace iCompany.Configs
{
    public class DbConfigurationSource : IConfigurationSource
    {
        private IConfigurationRoot defaultValue;
        public DbConfigurationSource(IConfigurationRoot defaultValue)
        {
            this.defaultValue = defaultValue;
        }

        public IConfigurationProvider Build(IConfigurationBuilder builder)
        {
            return new DbConfigurationProvider(defaultValue);
        }
    }

    public static class DbConfigurationExtensions
    {
        public static IConfigurationBuilder AddDbConfig(this IConfigurationBuilder builder, IConfigurationRoot defaultValue)
        {
            return builder.Add(new DbConfigurationSource(defaultValue));
        }
    }
}
