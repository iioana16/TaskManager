public interface ITaskRepository
{
    Task<List<TaskItem>> GetAllAsync();
    Task<TaskItem?> GetByIdAsync(string id);
    Task<string> AddAsync(TaskItem task);
    Task UpdateAsync(string id, TaskItem task);
    Task DeleteAsync(string id);
    Task<List<TaskItem>> SearchByTitleAsync(string title);
}