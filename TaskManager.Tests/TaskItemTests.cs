using Xunit;

public class TaskItemTests
{
    [Fact]
    public void NewTask_ShouldBeIncompleteByDefault()
    {
        var task = new TaskItem();

        Assert.False(task.IsCompleted);
    }

    [Fact]
    public void Task_ShouldStoreTitle()
    {
        var task = new TaskItem
        {
            Title = "Buy groceries"
        };

        Assert.Equal("Buy groceries", task.Title);
    }

    [Fact]
    public void Task_ShouldAllowDescription()
    {
        var task = new TaskItem
        {
            Title = "Study",
            Description = "Prepare for exam"
        };

        Assert.Equal("Prepare for exam", task.Description);
    }

    [Fact]
    public void Task_ShouldHaveCreatedAtDate()
    {
        var task = new TaskItem();

        Assert.NotEqual(default, task.CreatedAt);
    }

    [Fact]
    public void Task_ShouldBeCompleted_WhenIsCompletedIsTrue()
    {
        var task = new TaskItem
        {
            Title = "Finish project",
            IsCompleted = true
        };

        Assert.True(task.IsCompleted);
    }


    [Fact]
    public void Task_ShouldAllowDueDate()
    {
        var dueDate = DateTime.UtcNow.AddDays(1);

        var task = new TaskItem
        {
            Title = "Submit project",
            DueDate = dueDate
        };

        Assert.Equal(dueDate, task.DueDate);
    }
}