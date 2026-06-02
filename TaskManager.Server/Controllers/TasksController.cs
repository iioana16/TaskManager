using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/tasks")]
public class TasksController : ControllerBase
{
    private readonly ITaskRepository _repository;

    public TasksController(ITaskRepository repository)
    {
        _repository = repository;
    }

    [HttpGet]
    public async Task<ActionResult<List<TaskItem>>> GetAll()
        => Ok(await _repository.GetAllAsync());

    [HttpPost]
    public async Task<ActionResult<string>> Create(TaskItem task)
        => Ok(await _repository.AddAsync(task));

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(string id, TaskItem task)
    {
        await _repository.UpdateAsync(id, task);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        await _repository.DeleteAsync(id);
        return NoContent();
    }

    [HttpGet("search")]
    public async Task<ActionResult<List<TaskItem>>> Search([FromQuery] string title)
    {
        if (string.IsNullOrWhiteSpace(title))
            return BadRequest("Title is required.");

        var tasks = await _repository.SearchByTitleAsync(title);

        return Ok(tasks);
    }
}