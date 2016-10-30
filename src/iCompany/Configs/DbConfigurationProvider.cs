using iCompany.Areas.Shared.Models;
using iCompany.Models;
using Microsoft.Extensions.Configuration;
using System.Linq;

namespace iCompany.Configs
{
    public class DbConfigurationProvider : ConfigurationProvider
    {
        private IConfigurationRoot defaultValue;

        public DbConfigurationProvider(IConfigurationRoot defaultValue)
        {
            this.defaultValue = defaultValue;
        }

        public override void Load()
        {
            var dbConfigs = new DbConfigs()
            {
                Encrypt = defaultValue.GetValue<bool>("DbConfigs:Encrypt"),
                Type = defaultValue.GetValue<string>("DbConfigs:Type"),
                ConnectionString = defaultValue.GetValue<string>("DbConfigs:ConnectionString")
            };
            using (var db = new CompanyDbContext(dbConfigs))
            {
                db.Database.EnsureCreated();

                //检查默认配置
                var enumerable = defaultValue.AsEnumerable();
                foreach (var value in enumerable)
                {
                    if (value.Value != null)
                    {
                        var config = db.Config.FirstOrDefault(a => a.Name == value.Key);
                        if (config == null)
                        {
                            config = new Config { Name = value.Key, Value = value.Value };
                            db.Config.Add(config);
                        }
                        db.SaveChanges();
                    }
                }

                Data = db.Config.ToDictionary(c => c.Name, c => c.Value);
            }
        }

        public override void Set(string key, string value)
        {
            base.Set(key, value);
            using (var db = new CompanyDbContext(defaultValue.GetValue<DbConfigs>("DbConfigs")))
            {
                var config = db.Config.FirstOrDefault(a => a.Name == key);
                if (config == null)
                {
                    config = new Config { Name = key };
                    db.Config.Add(config);
                }
                config.Value = value;
                db.SaveChanges();
            }
        }
    }
}
