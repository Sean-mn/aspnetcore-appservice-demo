namespace azurepractice.Dto.Request;

public record UpdateTodoRequest(
    string Title,
    bool IsDone
    );