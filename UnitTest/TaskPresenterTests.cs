using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Moq;
using Moq.Protected;
using NUnit.Framework.Legacy;
using Tubes_KPL.src.Domain.Models;
using Tubes_KPL.src.Infrastructure.Configuration;
using Tubes_KPL.src.Presentation.Presenters;
using Tubes_KPL.src.Services.Libraries;
using System.Reflection;
using System.Diagnostics;
using Pose;
using Is = NUnit.Framework.Is;

namespace UnitTest;

[TestFixture]
public class TaskPresenter_UpdateTaskStatus_Tests
{
    private Mock<HttpMessageHandler> _httpMessageHandlerMock;
    private HttpClient _httpClient;
    private Mock<IConfigProvider> _configProviderMock;
    private TaskPresenter _presenter;
    private JsonSerializerOptions _jsonOptions;

    [SetUp]
    public void Setup()
    {
        _httpMessageHandlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);
        _httpClient = new HttpClient(_httpMessageHandlerMock.Object);
        _configProviderMock = new Mock<IConfigProvider>();
        _jsonOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            Converters = { new System.Text.Json.Serialization.JsonStringEnumConverter() }
        };

        _configProviderMock.Setup(m => m.GetConfig<Dictionary<string, object>>("ReminderSettings"))
        .Returns(new Dictionary<string, object>
        {
                      { "DaysBeforeDeadline", JsonSerializer.SerializeToElement(3) }
        }).Verifiable();
                // Inisialisasi presenter dengan dependensi
                _presenter = new TaskPresenter(_configProviderMock.Object);
        typeof(TaskPresenter).GetField("_httpClient", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(_presenter, _httpClient);

        _httpMessageHandlerMock.Protected()
            .Setup("Dispose", ItExpr.IsAny<bool>());
    }

    /// Test  valid ID and valid status transition.
    [Test]
    public void UpdateTaskStatus_ValidIdAndStatus_ReturnsSuccess()
    {
        // Arrange
        string idStr = "1";
        int statusIndex = 1; // SedangDikerjakan
        var existingTask = new Tugas { Id = 1, Judul = "Test Tugas", Status = StatusTugas.BelumMulai, Kategori = KategoriTugas.Akademik, Deadline = DateTime.Now.AddDays(2) };
        var updatedTask = new Tugas { Id = 1, Judul = "Test Tugas", Status = StatusTugas.SedangDikerjakan, Kategori = KategoriTugas.Akademik, Deadline = DateTime.Now.AddDays(2) };

        _httpMessageHandlerMock.Protected()
            .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.Is<HttpRequestMessage>(req => req.Method == HttpMethod.Get), ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = JsonContent.Create(existingTask, options: _jsonOptions)
            });

        _httpMessageHandlerMock.Protected()
            .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.Is<HttpRequestMessage>(req => req.Method == HttpMethod.Put), ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = JsonContent.Create(updatedTask, options: _jsonOptions)
            });

        _httpMessageHandlerMock.Protected().Setup("Dispose", ItExpr.IsAny<bool>());

        Shim shim1 = Shim.Replace(() => Tubes_KPL.src.Application.Helpers.InputValidator.InputValidStatus())
            .With(() => statusIndex);
        Shim shim2 = Shim.Replace(() => Tubes_KPL.src.Application.Helpers.InputValidator.TryParseId(default, out It.Ref<int>.IsAny))
            .With((string s, out int id) => { id = 1; return true; });

        PoseContext.Isolate(() =>
        {
            var result = _presenter.UpdateTaskStatus(idStr).Result;
            ClassicAssert.IsTrue(result.IsSuccess);
            StringAssert.Contains("berhasil diubah menjadi", result.Value);
        }, shim1, shim2);
    }


    //get all tasks
    [Test]
    public async Task GetAllTasks_ShouldReturnAllTasks()
    {
        // Arrange
        var mockTasks = new List<Tugas>
    {
        new Tugas { Id = 1, Judul = "Tugas 1", Deadline = DateTime.Now.AddDays(5), Kategori = KategoriTugas.Akademik, Status = StatusTugas.BelumMulai },
        new Tugas { Id = 2, Judul = "Tugas 2", Deadline = DateTime.Now.AddDays(8), Kategori = KategoriTugas.NonAkademik, Status = StatusTugas.SedangDikerjakan }
    };

        _httpMessageHandlerMock.Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.Is<HttpRequestMessage>(req => req.Method == HttpMethod.Get),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = JsonContent.Create(mockTasks, options: _jsonOptions)
            });

        // Act
        var result = await _presenter.GetAllTasks();

        // Assert
        Assert.That(result, Is.Empty, "Result should be empty string because output is written to console.");
    }

    /// Test invalid ID.
    [Test]
    public async Task UpdateTaskStatus_InvalidId_ReturnsFailure()
    {
        string invalidId = "abc";
        StaticHelper.SetStaticMethodReturnValue(typeof(Tubes_KPL.src.Application.Helpers.InputValidator), "TryParseId", false, outValue: 0);

        var result = await _presenter.UpdateTaskStatus(invalidId);

        Assert.That(result.IsSuccess, NUnit.Framework.Is.True);
        StringAssert.Contains("ID tugas tidak valid", result.Error);
    }

    /// Test status transition is not allowed.
    [Test]
    public async Task UpdateTaskStatus_StatusTransitionNotAllowed_ReturnsFailure()
    {
        string idStr = "1";
        int statusIndex = 2;
        var existingTask = new Tugas { Id = 1, Judul = "Test Tugas", Status = StatusTugas.Terlewat, Kategori = KategoriTugas.Akademik, Deadline = DateTime.Now.AddDays(2) };

        StaticHelper.SetStaticMethodReturnValue(typeof(Tubes_KPL.src.Application.Helpers.InputValidator), "InputValidStatus", statusIndex);
        StaticHelper.SetStaticMethodReturnValue(typeof(Tubes_KPL.src.Application.Helpers.InputValidator), "TryParseId", true, outValue: 1);

        _httpMessageHandlerMock.Protected()
            .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.Is<HttpRequestMessage>(req => req.Method == HttpMethod.Get), ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = JsonContent.Create(existingTask, options: _jsonOptions)
            });

        var result = await _presenter.UpdateTaskStatus(idStr);

        Assert.That(result.IsSuccess, NUnit.Framework.Is.True);
        StringAssert.Contains("Tidak bisa mengubah status", result.Error);
    }

    /// Test for exception handling.
    [Test]
    public async Task UpdateTaskStatus_ExceptionThrown_ReturnsFailure()
    {
        string idStr = "1";
        int statusIndex = 1;
        StaticHelper.SetStaticMethodThrows(typeof(Tubes_KPL.src.Application.Helpers.InputValidator), "InputValidStatus", new Exception("Test exception"));

        var result = await _presenter.UpdateTaskStatus(idStr);

        Assert.That(result.IsSuccess, NUnit.Framework.Is.True);
        StringAssert.Contains("Terjadi kesalahan", result.Error);
    }

    /// Performance test for UpdateTaskStatus.
    [Test]
    public void PerformanceTest_UpdateTaskStatus()
    {
        string idStr = "1";
        int statusIndex = 1;
        var existingTask = new Tugas { Id = 1, Judul = "Test Tugas", Status = StatusTugas.BelumMulai, Kategori = KategoriTugas.Akademik, Deadline = DateTime.Now.AddDays(2) };
        var updatedTask = new Tugas { Id = 1, Judul = "Test Tugas", Status = StatusTugas.SedangDikerjakan, Kategori = KategoriTugas.Akademik, Deadline = DateTime.Now.AddDays(2) };
        StaticHelper.SetStaticMethodReturnValue(typeof(Tubes_KPL.src.Application.Helpers.InputValidator), "InputValidStatus", statusIndex);
        StaticHelper.SetStaticMethodReturnValue(typeof(Tubes_KPL.src.Application.Helpers.InputValidator), "TryParseId", true, outValue: 1);
        _httpMessageHandlerMock.Protected()
            .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.Is<HttpRequestMessage>(req => req.Method == HttpMethod.Get), ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = JsonContent.Create(existingTask, options: _jsonOptions)
            });
        _httpMessageHandlerMock.Protected()
            .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.Is<HttpRequestMessage>(req => req.Method == HttpMethod.Put), ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = JsonContent.Create(updatedTask, options: _jsonOptions)
            });

        var stopwatch = new Stopwatch();
        stopwatch.Start();
        var task = _presenter.UpdateTaskStatus(idStr);
        task.Wait();
        stopwatch.Stop();
        ClassicAssert.Less(stopwatch.ElapsedMilliseconds, 1000, "Performance: UpdateTaskStatus should complete within 1 second.");
    }

    //[Test]
    //public async Task GetTaskDetails_ShouldReturnCorrectTaskDetails()
    //{
    //    // Arrange
    //    var taskId = "1"; // ID tugas yang valid
    //    var expectedTask = new Tugas
    //    {
    //        Id = 1,
    //        Judul = "Judul tugas 1",
    //        Deadline = new DateTime(2026, 8, 9),
    //        Kategori = KategoriTugas.Akademik,
    //        Status = StatusTugas.Selesai
    //    };

    //    // Act
    //    var result = await _presenter.GetTaskDetails(taskId);

    //    // Assert
    //    var expectedOutput = "Detail Tugas #1:\n" +
    //                         $"Judul: {expectedTask.Judul}\n" +
    //                         $"Kategori: {expectedTask.Kategori}\n" +
    //                         $"Status: {expectedTask.Status}\n" +
    //                         $"Deadline: 09 August 2026";

    //    // Verifikasi apakah hasil yang diterima sesuai dengan ekspektasi
    //    Assert.That(result, NUnitIs.EqualTo(expectedOutput));

    //    // Untuk tujuan debug, Anda bisa mencetak hasilnya
    //    Console.WriteLine("Expected:\n" + expectedOutput);
    //    Console.WriteLine("Actual:\n" + result);
    //}

    [TearDown]
    public void TearDown()
    {
        _httpClient?.Dispose();
    }
}

/// Helper class to mock static methods for testing.
public static class StaticHelper
{
    public static void SetStaticMethodReturnValue(Type type, string methodName, object returnValue, object outValue = null)
    {
        Shim shim = null;
        if (methodName == "InputValidStatus")
        {
            shim = Shim.Replace(() => Tubes_KPL.src.Application.Helpers.InputValidator.InputValidStatus())
                .With(() => (int)returnValue);
        }
        else if (methodName == "TryParseId")
        {
            shim = Shim.Replace(() => Tubes_KPL.src.Application.Helpers.InputValidator.TryParseId(default, out It.Ref<int>.IsAny))
                .With((string s, out int id) => { id = (int)outValue; return (bool)returnValue; });
        }
        PoseContext.Isolate(() => { }, shim);
    }

    public static void SetStaticMethodThrows(Type type, string methodName, Exception ex)
    {
        if (methodName == "InputValidStatus")
        {
            Shim shim = Shim.Replace(() => Tubes_KPL.src.Application.Helpers.InputValidator.InputValidStatus())
                .With(() => throw ex);
            PoseContext.Isolate(() => { }, shim);
        }
    }
}


// Code Program Unitest Delete Tugas : bintang 
[TestFixture]
public class TaskPresenter_DeleteTask_Tests
{
    private Mock<HttpMessageHandler> _httpMessageHandlerMock;
    private HttpClient _httpClient;
    private Mock<IConfigProvider> _configProviderMock;
    private TaskPresenter _presenter;
    private JsonSerializerOptions _jsonOptions;

    [SetUp]
    public void Setup()
    {
        _httpMessageHandlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);
        _httpClient = new HttpClient(_httpMessageHandlerMock.Object);
        _configProviderMock = new Mock<IConfigProvider>();
        _jsonOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            Converters = { new System.Text.Json.Serialization.JsonStringEnumConverter() }
        };

        _presenter = (TaskPresenter)Activator.CreateInstance(typeof(TaskPresenter), _configProviderMock.Object);
        typeof(TaskPresenter).GetField("_httpClient", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(_presenter, _httpClient);

        _httpMessageHandlerMock.Protected()
            .Setup("Dispose", ItExpr.IsAny<bool>());
    }

    [Test]
    public async Task DeleteTask_ValidId_ReturnsSuccess()
    {
        // Arrange
        string idStr = "1";
        var task = new Tugas { Id = 1, Judul = "Test Tugas", Kategori = KategoriTugas.Akademik, Status = StatusTugas.BelumMulai, Deadline = DateTime.Now.AddDays(2) };

        _httpMessageHandlerMock.Protected()
            .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.Is<HttpRequestMessage>(req => req.Method == HttpMethod.Get), ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = JsonContent.Create(task, options: _jsonOptions)
            });

        _httpMessageHandlerMock.Protected()
            .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.Is<HttpRequestMessage>(req => req.Method == HttpMethod.Delete), ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(new HttpResponseMessage(HttpStatusCode.OK));

        // Act
        var result = await _presenter.DeleteTask(idStr);

        // Assert
        Assert.That(result, Does.Contain($"Tugas '{task.Judul}' berhasil dihapus"));
    }

    [Test]
    public async Task DeleteTask_InvalidId_ReturnsError()
    {
        // Arrange
        string invalidId = "abc";

        // Act
        var result = await _presenter.DeleteTask(invalidId);

        // Assert
        Assert.That(result, Does.Contain("ID tugas tidak valid"));
    }

    [Test]
    public async Task DeleteTask_TaskNotFound_ReturnsError()
    {
        // Arrange
        string idStr = "1";

        _httpMessageHandlerMock.Protected()
            .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.Is<HttpRequestMessage>(req => req.Method == HttpMethod.Get), ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(new HttpResponseMessage(HttpStatusCode.NotFound));

        // Act
        var result = await _presenter.DeleteTask(idStr);

        // Assert
        Assert.That(result, Does.Contain("Tugas tidak ditemukan"));
    }

    [Test]
    
    public async Task DeleteTask_ExceptionThrown_ReturnsError()
    {
        // Arrange
        string idStr = "1";

        _httpMessageHandlerMock.Protected()
            .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
            .ThrowsAsync(new Exception("Test exception"));

        // Act
        var result = await _presenter.DeleteTask(idStr);

        // Assert
        Assert.That(result, Does.Contain("Error: Test exception"));
    }

    [TearDown]
    public void TearDown()
    {
        _httpClient?.Dispose();
    }
}

