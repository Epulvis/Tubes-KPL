namespace Application.Configuration
{
    public interface IConfigProvider
    {
        // Mengambil konfigurasi berdasarkan key dan mengembalikannya dalam tipe data T
        T GetConfig<T>(string key);

        // Menyimpan nilai konfigurasi dengan key tertentu
        void SetConfig<T>(string key, T value);

        // Menyimpan seluruh perubahan konfigurasi ke penyimpanan permanen
        void Save();
    }
}