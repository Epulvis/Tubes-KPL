# Struktur Proyek Aplikasi Manajemen Tugas Mahasiswa

## Struktur Folder
```
project-root/
│
├── /src/                    # Source code utama
│   ├── /UI/                 # Antarmuka pengguna (Windows Forms / WPF)
│   ├── /BLL/                # Business Logic Layer
│   ├── /DAL/                # Data Access Layer
│   └── /Config/             # Konfigurasi runtime dan file
│
├── /data/                  # Database SQLite dan file ekspor (PDF/CSV)
│
├── /test/                  # Unit test (misalnya menggunakan MSTest atau xUnit)
│
├── /docs/                  # Dokumentasi proyek
│   ├── arsitektur.md       # Desain arsitektur aplikasi
│   ├── skema_database.plantuml  # Diagram skema database
│   └── README.md           # Deskripsi struktur proyek
│
├── /cli/                   # Modul CLI untuk integrasi fitur (opsional)
│
└── TaskManager.sln         # File solusi Visual Studio
```

## Deskripsi Singkat Komponen
- **UI**: Form untuk input tugas, filter, reminder, dan ekspor laporan
- **BLL**: Logika bisnis seperti validasi, statistik, prioritas tugas
- **DAL**: Akses basis data SQLite dan ekspor file
- **Config**: File JSON/XML untuk runtime configuration
- **Docs**: Dokumentasi teknis dan visual diagram
- **Test**: Unit test per modul sesuai pembagian anggota

## Diagram Skema
Lihat file `skema_database.plantuml` untuk relasi antar tabel, seperti `Tugas`, `Konfigurasi`, `Statistik`, dan `Prioritas`.

---

Disusun oleh: Tim Pengembang
Tahun: 2025

