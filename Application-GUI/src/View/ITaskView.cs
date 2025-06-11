using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Application.Models;

namespace Application.View
{
    public interface ITaskView
    {
        string GetTaskTitleInput();
        string GetTaskDescriptionInput();
        DateTime GetTaskDueDateInput();
        int GetSelectedTaskId();
        StatusTugas GetNewTaskStatusInput();
        DateTime GetFilterStartDateInput();
        DateTime GetFilterEndDateInput();
        string GetExportFormatInput();
        string GetExportFilePathInput(string defaultFileName, string filter);

        void DisplayTasks(List<Tugas> tasks);
        void DisplayTaskDetails(string task);
        void DisplayMessage(string message, string caption, MessageBoxIcon icon);
        void ClearInputs();

        event Action AddTaskRequested;
        event Action ViewTasksRequested;
        event Action ViewTaskDetailsRequested;
        event Action UpdateTaskStatusRequested;
        event Action DeleteTaskRequested;
        event Action FilterTasksByDateRequested;
        event Action ExportTasksRequested;
        event Action FormLoaded;
    }
}
