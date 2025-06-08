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
public class TaskPresenterTestsGetByDateRange
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
    public async Task GetTasksByDateRange_ShouldReturnError_WhenStartDateInvalid()
    {
        // Arrange
        var startDate = "invalid-date";
        var endDate = "10/05/2025";

        // Act
        var result = await _presenter.GetTasksByDateRange(startDate, endDate);

        // Assert
        Assert.That(result, Is.EqualTo("Tanggal mulai tidak valid! Gunakan format DD/MM/YYYY."));
    }

    [Test]
    public async Task GetTasksByDateRange_ShouldReturnError_WhenEndDateInvalid()
    {
        // Arrange
        var startDate = "01/05/2025";
        var endDate = "invalid-date";

        // Act
        var result = await _presenter.GetTasksByDateRange(startDate, endDate);

        // Assert
        Assert.That(result, Is.EqualTo("Tanggal akhir tidak valid! Gunakan format DD/MM/YYYY."));
    }
}
