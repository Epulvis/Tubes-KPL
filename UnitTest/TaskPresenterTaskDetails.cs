//using System;
//using System.Collections.Generic;
//using System.Net;
//using System.Net.Http;
//using System.Net.Http.Json;
//using System.Text.Json;
//using System.Threading;
//using System.Threading.Tasks;
//using Moq;
//using Moq.Protected;
//using Tubes_KPL.src.Domain.Models;
//using Tubes_KPL.src.Infrastructure.Configuration;
//using Tubes_KPL.src.Presentation.Presenters;
//using System.Reflection;
//using Pose;
//using NUnit.Framework.Legacy;

//namespace UnitTest;
//[TestFixture]
//class TaskPresenterTaskDetails
//{
//    private Mock<HttpMessageHandler> _httpMessageHandlerMock;
//    private HttpClient _httpClient;
//    private Mock<IConfigProvider> _configProviderMock;
//    private TaskPresenter _presenter;
//    private JsonSerializerOptions _jsonOptions;

//    [SetUp]
//    public void Setup()
//    {
//        _httpMessageHandlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);
//        _httpClient = new HttpClient(_httpMessageHandlerMock.Object);
//        _configProviderMock = new Mock<IConfigProvider>();
//        _jsonOptions = new JsonSerializerOptions
//        {
//            PropertyNameCaseInsensitive = true,
//            Converters = { new System.Text.Json.Serialization.JsonStringEnumConverter() }
//        };

//        _presenter = (TaskPresenter)Activator.CreateInstance(typeof(TaskPresenter), _configProviderMock.Object);
//        typeof(TaskPresenter).GetField("_httpClient", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(_presenter, _httpClient);
//        typeof(TaskPresenter).GetField("_jsonOptions", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(_presenter, _jsonOptions);
//    }

//    [Test]
//    public async Task GetTaskDetails_ValidId_ReturnsDetailWithWarning()
//    {
//        string idStr = "1";
//        var task = new Tugas
//        {
//            Id = 1,
//            Judul = "Tugas Test",
//            Kategori = KategoriTugas.Akademik,
//            Status = StatusTugas.BelumMulai,
//            Deadline = DateTime.Now.AddDays(1)
//        };

//        var reminderSettings = new Dictionary<string, object>
//        {
//            { "DaysBeforeDeadline", JsonDocument.Parse("2").RootElement }
//        };

//        StaticHelper.SetStaticMethodReturnValue(typeof(Tubes_KPL.src.Application.Helpers.InputValidator), "TryParseId", true, outValue: 1);

//        _httpMessageHandlerMock.Protected()
//            .Setup<Task<HttpResponseMessage>>("SendAsync",
//                ItExpr.Is<HttpRequestMessage>(req => req.Method == HttpMethod.Get),
//                ItExpr.IsAny<CancellationToken>())
//            .ReturnsAsync(new HttpResponseMessage(HttpStatusCode.OK)
//            {
//                Content = JsonContent.Create(task, options: _jsonOptions)
//            });

//        _configProviderMock.Setup(cp => cp.GetConfig<Dictionary<string, object>>("ReminderSettings"))
//            .Returns(reminderSettings);

//        var result = await _presenter.GetTaskDetails(idStr);

//        StringAssert.Contains("Detail Tugas", result);
//        StringAssert.Contains("Deadline", result);
//        StringAssert.Contains("Peringatan", result);
//    }

//    [Test]
//    public async Task GetTaskDetails_IdNotFound_ReturnsNotFoundMessage()
//    {
//        string idStr = "999";

//        StaticHelper.SetStaticMethodReturnValue(typeof(Tubes_KPL.src.Application.Helpers.InputValidator), "TryParseId", true, outValue: 999);

//        _httpMessageHandlerMock.Protected()
//            .Setup<Task<HttpResponseMessage>>("SendAsync",
//                ItExpr.Is<HttpRequestMessage>(req => req.Method == HttpMethod.Get),
//                ItExpr.IsAny<CancellationToken>())
//            .ReturnsAsync(new HttpResponseMessage(HttpStatusCode.NotFound));

//        var result = await _presenter.GetTaskDetails(idStr);

//        Assert.That(result, NUnit.Framework.Is.EqualTo("Tugas tidak ditemukan!"));
//    }

//    [Test]
//    public async Task GetTaskDetails_InvalidId_ReturnsInvalidMessage()
//    {
//        string idStr = "abc";

//        StaticHelper.SetStaticMethodReturnValue(typeof(Tubes_KPL.src.Application.Helpers.InputValidator), "TryParseId", false);

//        var result = await _presenter.GetTaskDetails(idStr);

//        Assert.That(result, NUnit.Framework.Is.EqualTo("ID tugas tidak valid! Pastikan berupa angka positif."));
//    }

//    [TearDown]
//    public void TearDown()
//    {
//        _httpClient?.Dispose();
//    }
//}

