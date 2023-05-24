namespace Zero
{
    /// <summary>
    /// 应用创建配置
    /// </summary>
    public class ZeroApplicationCreationOptions
    {
        public ZeroApplicationCreationOptions()
        {
            Configuration = new ZeroConfigurationBuilderOptions();
        }
        /// <summary>
        /// 此属性中的选项仅在未注册IConfiguration时生效。
        /// </summary>
        public ZeroConfigurationBuilderOptions Configuration { get; }
    }
}
