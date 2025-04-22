# Arsitektur Aplikasi Manajemen Tugas Mahasiswa

## 1. Tujuan Desain
Merancang arsitektur modular dan skalabel untuk aplikasi manajemen tugas mahasiswa berbasis C#, yang memungkinkan pengelolaan tugas akademik dan non-akademik secara efisien oleh pengguna. Aplikasi ini dibangun dengan memperhatikan alur kerja tim sebanyak 5 orang, serta mendukung ekspansi dan pemeliharaan jangka panjang.

## 2. Komponen Utama

### 2.1. Antarmuka Pengguna (UI)
- **Framework**: Windows Forms / WPF
- **Fungsi**:
  - Input & update data tugas (judul, deadline, status, kategori)
  - Tampilan daftar tugas (dengan filter & pencarian)
  - Tombol ekspor laporan
  - Reminder visual deadline

### 2.2. Logika Bisnis (Business Logic Layer / BLL)
- **Fungsi**:
  - Validasi input (defensive programming)
  - Filter, sort, dan hitung statistik tugas
  - Mapping status ke prioritas & warna (table-driven logic)
  - Reminder & konfigurasi runtime
  - Unit test untuk setiap fungsi kritikal

### 2.3. Lapis Data (Data Access Layer / DAL)
- **Fungsi**:
  - CRUD data tugas (judul, deadline, status, kategori)
  - Simpan konfigurasi ke file lokal (JSON/XML)
  - Ekspor data ke CSV atau PDF
  - Generalisasi akses data (misal: List<T>)

### 2.4. Basis Data
- **Struktur Data**: SQLite (embedded, ringan untuk aplikasi desktop)
- **Tabel**:
  - `Tugas`: id, judul, deadline, status, kategori
  - `Konfigurasi`: key, value

### 2.5. API Internal
- **Fungsi**:
  - Memisahkan komunikasi antara UI, BLL, dan DAL
  - Menyediakan interface terstruktur untuk interaksi antar modul
  - Pola: Dependency Injection

## 3. Alur Data dan Komunikasi

1. **Pengguna** menginput tugas melalui UI
2. UI mengirim data ke BLL untuk validasi dan logika
3. BLL memanggil DAL untuk menyimpan ke basis data
4. Untuk menampilkan tugas:
   - UI memanggil BLL untuk mengambil dan menyaring data
   - BLL meminta data dari DAL, lalu memprosesnya (filter, statistik)
5. Ekspor data:
   - UI meminta BLL menyiapkan data
   - BLL mengambil data dari DAL dan membentuk format ekspor
6. Reminder diatur di BLL berdasarkan runtime config dan dikirimkan ke UI

## 4. Tanggung Jawab Tim

- **Anggota 1**: Implementasi dan unit test CRUD di DAL
- **Anggota 2**: Statistik dan filter di BLL, performance test
- **Anggota 3**: Runtime config, defensive programming
- **Anggota 4**: Mapping logika prioritas dari tabel, table-driven logic, unit test logika
- **Anggota 5**: Generalisasi fungsi, dokumentasi, integrasi CLI

## 5. Skalabilitas
- Modularisasi memungkinkan penambahan fitur (notifikasi email, kolaborasi multi-user)
- API internal memudahkan porting ke versi web/mobile
- Logging & monitoring dapat ditambahkan di lapisan BLL

## 6. Dokumentasi
- Semua modul harus diberi komentar XML (C#)
- Penamaan dan struktur mengikuti standar .NET coding guideline
- Dokumen teknis tambahan dapat ditulis dalam folder `/docs`

---

Disusun oleh: Arsitek Perangkat Lunak (2025)

