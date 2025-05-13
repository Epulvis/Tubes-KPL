using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Tubes_KPL.src.Domain.Models;
using Tubes_KPL.src.Infrastructure.Configuration;
using NUnit.Framework;
using Tubes_KPL.src.Presentation.Presenters;
using Moq;

namespace UnitTest;

[TestFixture]
public class TaskPresenterTests
{
    private TaskPresenter _presenter;
    private Mock<IConfigProvider> _mockConfigProvider;

    [SetUp]
    public void Setup()
    {
        // Inisialisasi JsonSerializerOptions jika diperlukan

        // Inisialisasi IConfigProvider Anda
        _mockConfigProvider = new Mock<IConfigProvider>();
        _mockConfigProvider.Setup(m => m.GetConfig<Dictionary<string, object>>("ReminderSettings"))
        .Returns(new Dictionary<string, object>
        {
              { "DaysBeforeDeadline", JsonSerializer.SerializeToElement(3) }
        }).Verifiable();
        // Inisialisasi presenter dengan dependensi
        _presenter = new TaskPresenter(_mockConfigProvider.Object);
    }

    [Test]
    public async Task GetTaskDetails_ShouldReturnCorrectTaskDetails()
    {
        // Arrange
        var taskId = "1"; // ID tugas yang valid
        var expectedTask = new Tugas
        {
            Id = 1,
            Judul = "Judul tugas 1",
            Kategori = KategoriTugas.Akademik,
            Status = StatusTugas.Selesai
        };

        // Act
        var result = await _presenter.GetTaskDetails(taskId);

        // Assert
        var expectedOutput = "Detail Tugas #1:\n" +
                             $"Judul: {expectedTask.Judul}\n" +
                             $"Kategori: {expectedTask.Kategori}\n" +
                             $"Status: {expectedTask.Status}\n" +
                             $"Deadline: 09 August 2026";

        // Verifikasi apakah hasil yang diterima sesuai dengan ekspektasi
        Assert.That(result, Is.EqualTo(expectedOutput));

        // Untuk tujuan debug, Anda bisa mencetak hasilnya
        Console.WriteLine("Expected:\n" + expectedOutput);
        Console.WriteLine("Actual:\n" + result);
    }

    [Test]
    public async Task GetTaskDetails_ShouldReturnNotFound()
    {
        // Arrange
        var taskId = "999"; // ID tugas yang tidak valid
        // Act
        var result = await _presenter.GetTaskDetails(taskId);
        // Assert
        var expectedOutput = "Tugas tidak ditemukan!";
        Assert.That(result, Is.EqualTo(expectedOutput));
        // Untuk tujuan debug, Anda bisa mencetak hasilnya
        Console.WriteLine("Expected:\n" + expectedOutput);
        Console.WriteLine("Actual:\n" + result);
    }

    [Test]
    public async Task GetTaskDetails_ShouldReturnError()
    {
        // Arrange
        var taskId = "abc"; // ID tugas yang tidak valid
        // Act
        var result = await _presenter.GetTaskDetails(taskId);
        // Assert
        var expectedOutput = "ID tugas tidak valid! Pastikan berupa angka positif.";
        Assert.That(result, Is.EqualTo(expectedOutput));
        // Untuk tujuan debug, Anda bisa mencetak hasilnya
        Console.WriteLine("Expected:\n" + expectedOutput);
        Console.WriteLine("Actual:\n" + result);
    }

    [Test]
    public async Task GetTaskDetails_ShouldReturnEmpty()
    {
        // Arrange
        var taskId = ""; // ID tugas yang kosong
        // Act
        var result = await _presenter.GetTaskDetails(taskId);
        // Assert
        var expectedOutput = "ID tugas tidak valid! Pastikan berupa angka positif.";
        Assert.That(result, Is.EqualTo(expectedOutput));
        // Untuk tujuan debug, Anda bisa mencetak hasilnya
        Console.WriteLine("Expected:\n" + expectedOutput);
        Console.WriteLine("Actual:\n" + result);
    }

    [Test]
    public async Task GetTaskDetails_ShouldReturnReminderSettingConfig()
    {
        // Arrange
        var taskId = "12"; // ID tugas yang valid
        var expectedTask = new Tugas
        {
            Id = 12,
            Judul = "test 13/05",
            Kategori = KategoriTugas.Akademik,
            Status = StatusTugas.BelumMulai
        };

        // Act
        var result = await _presenter.GetTaskDetails(taskId);

        // Assert
        var expectedOutput = $"Detail Tugas #{expectedTask.Id}:\n" +
                             $"Judul: {expectedTask.Judul}\n" +
                             $"Kategori: {expectedTask.Kategori}\n" +
                             $"Status: {expectedTask.Status}\n" +
                             $"Deadline: 16 May 2025\n"+
                             "Peringatan: Deadline 3 hari lagi!";

        // Verifikasi apakah hasil yang diterima sesuai dengan ekspektasi
        Assert.That(result, Is.EqualTo(expectedOutput));

        // Untuk tujuan debug, Anda bisa mencetak hasilnya
        Console.WriteLine("Expected:\n" + expectedOutput);
        Console.WriteLine("Actual:\n" + result);
    }

}
