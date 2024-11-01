import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { TaskService } from './services/task.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html'
})
export class AppComponent implements OnInit {
  title = 'app';
  static userPreferences = `/${'task-list'}`;
  constructor(
    public taskService: TaskService,
    private readonly router: Router
    ) { }

  ngOnInit() {
 this.router.navigate(['/task-list'])
  }

   
}
