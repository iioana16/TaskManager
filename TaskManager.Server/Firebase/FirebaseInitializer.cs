using Google.Cloud.Firestore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using TaskManager.Server.Repositories;

namespace TaskManager.Server.Infrastructure
{
    public static class FirebaseInitializer
    {
        public static void AddFirebase(this IServiceCollection services, IConfiguration configuration)
        {
            var credentialsRelativePath = configuration["Firebase:CredentialsPath"];
            var credentialsFullPath = Path.Combine(AppContext.BaseDirectory, credentialsRelativePath);

            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", credentialsFullPath);

            var projectId = configuration["Firebase:ProjectId"];
            var firestoreDb = FirestoreDb.Create(projectId);

            services.AddSingleton(firestoreDb);
            services.AddScoped<ITaskRepository, TaskRepository>();
        }
    }
}