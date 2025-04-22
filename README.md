# 📘 Aplikasi Manajemen Tugas Mahasiswa - Tugas Besar KPL

## 📌 Deskripsi Proyek
Aplikasi ini dirancang untuk membantu mahasiswa dalam mencatat, mengelola, dan memantau progres tugas akademik maupun non-akademik seperti tugas kuliah, proyek kelompok, dan kegiatan organisasi. Proyek ini dikerjakan oleh tim beranggotakan 5 orang dalam konteks tugas besar mata kuliah **Konstruksi Perangkat Lunak (KPL)**.

Aplikasi dibangun menggunakan **C# dengan Visual Studio**, memanfaatkan pendekatan arsitektur modular agar mudah dikembangkan dan dipelihara.

---

## ⚙️ Fitur Utama
- CRUD tugas: tambah, ubah, dan hapus data tugas
- Filter tugas berdasarkan deadline, status, dan kategori
- Reminder deadline visual
- Statistik jumlah tugas selesai, belum, dan overdue
- Mapping status ke prioritas/warna
- Export laporan ke format PDF/CSV
- Konfigurasi runtime (prioritas default, file config)
- Dukungan CLI untuk integrasi otomatis
- Dokumentasi internal dengan XML Comments

---

## 🧩 Arsitektur Komponen

Struktur komponen berdasarkan tiga lapisan utama:

- **UI Layer**: Windows Forms/WPF untuk antarmuka pengguna
- **BLL (Business Logic Layer)**: Proses logika aplikasi, statistik, validasi, dan filter
- **DAL (Data Access Layer)**: Interaksi dengan database SQLite dan ekspor file

Diagram dan deskripsi lengkap tersedia di [`/docs/arsitektur.md`](./docs/arsitektur.md)&#8203;:contentReference[oaicite:0]{index=0}.

---

## 🗃️ Struktur Proyek

Struktur folder mengacu pada praktik terbaik dalam pengembangan perangkat lunak berbasis C#:


📁 Referensi: [`/docs/Sturuktur_Folder.md`](./docs/Sturuktur_Folder.md)&#8203;:contentReference[oaicite:1]{index=1}

---

## 🧠 Pembagian Tugas Anggota

| Anggota     | Tugas Utama                  | Sub-tugas                                                                 |
|-------------|------------------------------|--------------------------------------------------------------------------|
| **Anggota 1** | CRUD                        | Kelola data tugas, tulis unit test CRUD                                 |
| **Anggota 2** | Filter & Statistik          | Filter by deadline/status, hitung tugas, performa filter                 |
| **Anggota 3** | Runtime Configuration       | Konfigurasi default, baca/simpan file, defensive programming             |
| **Anggota 4** | Table-driven Construction   | Mapping status → prioritas/warna, fungsi lookup, unit test logika        |
| **Anggota 5** | Parameterisasi & Dokumentasi| Refactor fungsi ke generic, dokumentasi XML, integrasi fitur ke CLI      |

🖼️ Lihat gambar pembagian lengkap di dokumentasi referensi visual.

---

## 🗄️ Skema Basis Data

Skema database dirancang menggunakan SQLite dan digambarkan dengan **PlantUML**. Terdiri dari tabel berikut:

- `Tugas` — Menyimpan detail tugas
- `Konfigurasi` — Runtime setting
- `Prioritas` — Mapping status ke warna/prioritas
- `Statistik` — Ringkasan progres tugas

📄 Diagram visual tersedia di: [`/docs/skema_database.plantuml`](./docs/skema_database.plantuml)

---

## 🧪 Teknologi & Tools

- **Bahasa**: C# (.NET)
- **IDE**: Visual Studio 2022
- **Database**: SQLite
- **Testing**: MSTest / xUnit
- **Ekspor File**: CSV, PDF
- **Dokumentasi**: Markdown
- **Visualisasi Skema**: PlantUML

---

## ✍️ Kontributor

Disusun dan dikembangkan oleh Tim Mahasiswa RPL 2025  
Universitas Telkom | Fakultas Informatika 

---

