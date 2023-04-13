namespace ClipboardApi.Models;

public record Clipboard
(
    Guid Id,
    string UserId,
    List<Record> Records
);