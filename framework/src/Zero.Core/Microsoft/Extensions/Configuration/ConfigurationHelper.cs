namespace Microsoft.Extensions.Configuration
{
    public static class ConfigurationHelper
    {
        /// <summary>
        /// 初始化配置位置
        /// </summary>
        /// <param name="options"></param>
        /// <param name="builderAction"></param>
        /// <returns></returns>
        public static IConfigurationRoot BuildConfiguration(
            ZeroConfigurationBuilderOptions options = null,
            Action<IConfigurationBuilder> builderAction = null)
        {
            options ??= new ZeroConfigurationBuilderOptions();

            if (options.BasePath.IsNullOrEmpty())
            {
                options.BasePath = Directory.GetCurrentDirectory();
            }
            // json 文件配置
            var builder = new ConfigurationBuilder()
                .SetBasePath(options.BasePath)
                .AddJsonFile(options.FileName + ".json", optional: options.Optional, reloadOnChange: options.ReloadOnChange);

            if (!options.EnvironmentName.IsNullOrEmpty())
            {
                builder = builder.AddJsonFile($"{options.FileName}.{options.EnvironmentName}.json", optional: options.Optional, reloadOnChange: options.ReloadOnChange);
            }
            // 用户机密配置
            if (options.EnvironmentName == "Development")
            {
                if (options.UserSecretsId != null)
                {
                    builder.AddUserSecrets(options.UserSecretsId);
                }
                else if (options.UserSecretsAssembly != null)
                {
                    builder.AddUserSecrets(options.UserSecretsAssembly, true);
                }
            }
            // 环境变量配置
            builder = builder.AddEnvironmentVariables(options.EnvironmentVariablesPrefix);
            // 命令行配置
            if (options.CommandLineArgs != null)
            {
                builder = builder.AddCommandLine(options.CommandLineArgs);
            }

            builderAction?.Invoke(builder);

            return builder.Build();
        }
    }
}
