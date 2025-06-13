namespace Application.Configuration
{
    public interface IConfigProvider
    {
        T GetConfig<T>(string key);

        void SetConfig<T>(string key, T value);

        void Save();
    }
}