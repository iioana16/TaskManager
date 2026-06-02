import { Component, OnInit } from '@angular/core';
import { Task } from '../../models/task.model';
import { TaskService } from '../../services/task.service';

@Component({
  selector: 'app-task-list',
  templateUrl: './task-list.component.html',
  styleUrls: ['./task-list.component.css']
})
export class TaskListComponent implements OnInit {

  tasks: Task[] = [];
  selectedTask: Task | null = null;

  isEditing = false;
  showModal = false;

  newTaskTitle: string = '';
  searchText: string = '';

  tempTask: Task = { 
    title: '', 
    description: '', 
    isCompleted: false,
    dueDate: ''
  };

  constructor(private taskService: TaskService) {}

  ngOnInit(): void {
    this.loadTasks();
  }

  private sortTasksByDueDate(tasks: Task[]): Task[] {
    return tasks.sort((a, b) => {
      if (!a.dueDate && !b.dueDate) return 0;
      if (!a.dueDate) return 1;
      if (!b.dueDate) return -1;

      return new Date(a.dueDate).getTime() - new Date(b.dueDate).getTime();
    });
  }

  loadTasks(): void {
    this.taskService.getAllTasks().subscribe({
      next: (data) => this.tasks = this.sortTasksByDueDate(data),
      error: () => this.tasks = []
    });
  }

  openAddModal(): void {
    this.isEditing = false;
    this.tempTask = { title: '', description: '', isCompleted: false, dueDate: '' };
    this.showModal = true;
  }

  editTask(task: Task): void {
    this.isEditing = true;
    this.selectedTask = task;

    this.tempTask = {
      ...task,
      description: task.description || '',
      dueDate: task.dueDate ? task.dueDate.substring(0, 10) : ''
    };

    this.showModal = true;
  }

  closeModal(): void {
    this.showModal = false;
  }

  saveTask(): void {
    if (!this.tempTask.title.trim()) return;

    const payload: Task = {
      title: this.tempTask.title.trim(),
      description: this.tempTask.description?.trim() || undefined,
      isCompleted: this.tempTask.isCompleted,
      dueDate: this.tempTask.dueDate
        ? new Date(this.tempTask.dueDate).toISOString()
        : undefined
    };

    console.log('Payload trimis:', payload);

    if (this.isEditing && this.selectedTask?.id) {
      this.taskService.updateTask(this.selectedTask.id, payload).subscribe({
        next: () => {
          this.loadTasks();
          this.closeModal();
        },
        error: (err) => console.error('Update error:', err)
      });
    } else {
      this.taskService.addTask(payload).subscribe({
        next: () => {
          this.loadTasks();
          this.closeModal();
        },
        error: (err) => console.error('Add error:', err)
      });
    }
  }

  toggleComplete(task: Task): void {
    const updated = { ...task, isCompleted: !task.isCompleted };

    if (task.id) {
      this.taskService.updateTask(task.id, updated).subscribe(() => {
        this.loadTasks();
      });
    }
  }

  deleteTask(id: string): void {
    if (confirm('Delete task?')) {
      this.taskService.deleteTask(id).subscribe(() => {
        this.loadTasks();
      });
    }
  }

  searchTasks(): void {
    if (!this.searchText.trim()) {
      this.loadTasks();
      return;
    }

    this.taskService.searchTasks(this.searchText).subscribe({
      next: (data) => this.tasks = this.sortTasksByDueDate(data),
      error: () => this.tasks = []
    });
  }
}
