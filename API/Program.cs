
using API.Storage;
using System.Text.Json.Serialization;
using Tubes_KPL.src.Domain.Models;
using Tubes_KPL.src.Application.Helpers;

var builder = WebApplication.CreateBuilder(args);
// Menambahkan layanan untuk dokumentasi API (Swagger)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Mengatur opsi serialisasi JSON agar enum dikonversi ke string
builder.Services.Configure<Microsoft.AspNetCore.Http.Json.JsonOptions>(options =>
{
    options.SerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

var app = builder.Build();

// Konfigurasi pipeline HTTP untuk development (menampilkan Swagger UI)
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Middleware untuk menangani error global dan mengembalikan pesan error JSON
app.Use(async (context, next) =>
{
    try
    {
        await next();
    }
    catch (Exception ex)
    {
        context.Response.StatusCode = 500;
        await context.Response.WriteAsJsonAsync(new { error = "Internal Server Error", detail = ex.Message });
    }
});

// Endpoint untuk mengambil semua data tugas
app.MapGet("/api/tugas", () =>
{
    var tasks = TugasStorage<List<Tugas>>.GetTugas();
    return Results.Ok(tasks);
});

// Endpoint untuk mengambil data tugas berdasarkan ID
app.MapGet("/api/tugas/{id:int}", (int id) =>
{
    var tasks = TugasStorage<List<Tugas>>.GetTugas();
    var task = tasks.FirstOrDefault(u => u.Id == id);
    return task is null ? Results.NotFound() : Results.Ok(task);
});

// Endpoint untuk menambah data tugas baru
app.MapPost("/api/tugas", (Tugas newTugas) =>
{
    var tasks = TugasStorage<List<Tugas>>.GetTugas();

    if (!InputValidator.IsValidDeadline(newTugas.Deadline))
        return Results.BadRequest("Input not valid");

    newTugas.Id = tasks.Count == 0 ? 1 : tasks.Max(u => u.Id) + 1;
    tasks.Add(newTugas);


    TugasStorage<List<Tugas>>.SaveTugas(tasks);
    return Results.Created($"/users/{newTugas.Id}", newTugas);

});

// Endpoint untuk mengubah data tugas berdasarkan ID
app.MapPut("/api/tugas/{id:int}", (int id, Tugas updatedTugas) =>
{
    var tasks = TugasStorage<List<Tugas>>.GetTugas();

    if (!InputValidator.IsValidDeadline(updatedTugas.Deadline))
        return Results.BadRequest("Input not valid");


    var task = tasks.FirstOrDefault(u => u.Id == id);
    if (task is null) return Results.NotFound();

    if (!InputValidator.IsValidDeadline(updatedTugas.Deadline)) return Results.BadRequest("Input not valid");

    task.Judul = updatedTugas.Judul;
    task.Status = updatedTugas.Status;
    task.Deadline = updatedTugas.Deadline;
    task.Kategori = updatedTugas.Kategori;
    TugasStorage<List<Tugas>>.SaveTugas(tasks);
    return Results.Ok(task);
});

// Endpoint untuk menghapus data tugas berdasarkan ID
app.MapDelete("/api/tugas/{id:int}", (int id) =>
{
    var tasks = TugasStorage<List<Tugas>>.GetTugas();
    var task = tasks.FirstOrDefault(u => u.Id == id);
    if (task is null) return Results.NotFound();

    tasks.Remove(task);
    TugasStorage<List<Tugas>>.SaveTugas(tasks);
    return Results.Ok($"Task with ID {id} has been deleted successfully.");
});

// filter untuk by params (deadline, status, kategori)
app.MapGet("/api/tugas/filter", (string? status, string? kategori, DateTime? deadline) =>
{
    var tasks = TugasStorage<List<Tugas>>.GetTugas();
    if (status != null)
    {
        tasks = tasks.Where(t => t.Status.ToString().Equals(status, StringComparison.OrdinalIgnoreCase)).ToList();
    }
    if (kategori != null)
    {
        tasks = tasks.Where(t => t.Kategori.ToString().Equals(kategori, StringComparison.OrdinalIgnoreCase)).ToList();
    }
    if (deadline != null)
    {
        tasks = tasks.Where(t => t.Deadline.Date == deadline.Value.Date).ToList();
    }
    return Results.Ok(tasks);
});

// Mendapatkan Tugas Berdasarkan Rentang Waktu
app.MapGet("/api/tugas/date-range", (DateTime startDate, DateTime endDate) =>
{
    var tasks = TugasStorage<List<Tugas>>.GetTugas();
    var filteredTasks = tasks.Where(t => t.Deadline.Date >= startDate.Date && t.Deadline.Date <= endDate.Date).ToList();

    return Results.Ok(filteredTasks);
});

app.Run("http://localhost:4000");