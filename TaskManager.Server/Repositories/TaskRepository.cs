using Google.Cloud.Firestore;

namespace TaskManager.Server.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly FirestoreDb _db;
        private readonly CollectionReference _collection;

        public TaskRepository(FirestoreDb firestoreDb)
        {
            _db = firestoreDb;
            _collection = _db.Collection("tasks");
        }

        public async Task<List<TaskItem>> GetAllAsync()
        {
            var snapshot = await _collection.GetSnapshotAsync();
            var tasks = new List<TaskItem>();

            foreach (var doc in snapshot.Documents)
            {
                var task = doc.ConvertTo<TaskItem>();
                task.Id = doc.Id;                   
                tasks.Add(task);
            }

            return tasks;
        }

        public async Task<TaskItem?> GetByIdAsync(string id)
        {
            var docRef = _collection.Document(id);
            var snapshot = await docRef.GetSnapshotAsync();

            if (snapshot.Exists)
            {
                var task = snapshot.ConvertTo<TaskItem>();
                task.Id = snapshot.Id;
                return task;
            }

            return null;
        }

        public async Task<string> AddAsync(TaskItem task)
        {
            var docRef = await _collection.AddAsync(task);
            return docRef.Id;
        }

        public async Task UpdateAsync(string id, TaskItem task)
        {
            var docRef = _collection.Document(id);
            await docRef.SetAsync(task, SetOptions.MergeAll);
        }

        public async Task DeleteAsync(string id)
        {
            var docRef = _collection.Document(id);
            await docRef.DeleteAsync();
        }

        public async Task<List<TaskItem>> SearchByTitleAsync(string title)
        {
            var snapshot = await _collection.GetSnapshotAsync();

            return snapshot.Documents
                .Select(doc =>
                {
                    var task = doc.ConvertTo<TaskItem>();
                    task.Id = doc.Id;
                    return task;
                })
                .Where(t =>
                    !string.IsNullOrWhiteSpace(t.Title) &&
                    t.Title.Contains(title, StringComparison.OrdinalIgnoreCase))
                .ToList();
        }
    }
}