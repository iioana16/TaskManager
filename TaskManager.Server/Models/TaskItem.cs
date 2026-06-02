using Google.Cloud.Firestore;
using System;

[FirestoreData]
public class TaskItem
{
    [FirestoreDocumentId]
    public string Id { get; set; } = string.Empty;
    [FirestoreProperty("title")]
    public string Title { get; set; } = string.Empty;
    [FirestoreProperty("description")]
    public string? Description { get; set; }
    [FirestoreProperty("isCompleted")]
    public bool IsCompleted { get; set; } = false;
    [FirestoreProperty("dueDate")]
    public DateTime? DueDate { get; set; }
    [FirestoreProperty("createdAt")]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}