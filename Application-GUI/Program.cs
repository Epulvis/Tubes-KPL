using Application.Configuration;
using Application.Controller;
using Application.View;
using Application.Services;
using Application.Helpers;

namespace Application_GUI
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();
            HttpClient httpClient = new HttpClient();
            InputValidator inputValidator = new InputValidator();
            StatusStateMachine statusStateMachine = new StatusStateMachine();

            TaskService taskService = new TaskService(httpClient, inputValidator, statusStateMachine);

            // Buat instance View (Form) dan Presenter
            TaskManagementForm view = new TaskManagementForm();
            TaskPresenter presenter = new TaskPresenter(view, taskService, inputValidator);

            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            System.Windows.Forms.Application.Run(view);
        }
    }
}