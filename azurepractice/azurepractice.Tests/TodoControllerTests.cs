using Microsoft.AspNetCore.Mvc;
using azurepractice.Controllers;
using azurepractice.Dto.Request;
using azurepractice.Entity;

namespace azurepractice.Tests;

public class TodoControllerTests
{
    private TodoController CreateController() => new();

    [Fact]
    public void GetAll_ReturnsEmptyList_WhenNoTodos()
    {
        var controller = CreateController();

        var result = controller.GetAll() as OkObjectResult;

        Assert.NotNull(result);
        Assert.Empty((List<Todo>)result.Value!);
    }

    [Fact]
    public void Create_ReturnsTodo_WithCorrectTitle()
    {
        var controller = CreateController();

        var result = controller.Create(new CreateTodoRequest("Test Todo")) as CreatedAtActionResult;

        Assert.NotNull(result);
        var todo = result.Value as Todo;
        Assert.Equal("Test Todo", todo!.Title);
        Assert.False(todo.IsDone);
    }

    [Fact]
    public void GetById_ReturnsNotFound_WhenTodoDoesNotExist()
    {
        var controller = CreateController();

        var result = controller.GetById(999);

        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public void Delete_ReturnsNoContent_WhenTodoExists()
    {
        var controller = CreateController();
        controller.Create(new CreateTodoRequest("To Delete"));

        var result = controller.Delete(1);

        Assert.IsType<NoContentResult>(result);
    }

    [Fact]
    public void Update_ReturnsTodo_WithUpdatedValues()
    {
        var controller = CreateController();
        controller.Create(new CreateTodoRequest("Original"));

        var result = controller.Update(1, new UpdateTodoRequest("Updated", true)) as OkObjectResult;

        Assert.NotNull(result);
        var todo = result.Value as Todo;
        Assert.Equal("Updated", todo!.Title);
        Assert.True(todo.IsDone);
    }
}