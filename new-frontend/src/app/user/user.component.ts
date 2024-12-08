// user.component.ts

import { Component, OnInit } from '@angular/core';
import { UserService } from './user.service';
import { Router } from '@angular/router';
import { ActivatedRoute } from '@angular/router'; // To fetch user ID from route if necessary
import { Location } from '@angular/common'; // To navigate back

@Component({
  selector: 'app-user',
  templateUrl: './user.component.html',
  styleUrls: ['./user.component.scss']
})
export class UserComponent implements OnInit {

  user: any = {
    userId: null,
    username: '',
    email: '',
    password: '',
    role: 'User',
    isActive: true
  };
  totalUsers: number = 0;
  activeUsers: number = 0;
  isLoading: boolean = false;
  isEditing: boolean = false;

  constructor(
    private userService: UserService, 
    private router: Router, 
    private route: ActivatedRoute,
    private location: Location
  ) { }

  ngOnInit(): void {
    // Check if an edit action is triggered, and load user data accordingly
    const userId = this.route.snapshot.paramMap.get('userId');
    if (userId) {
      this.isEditing = true;
      this.loadUserData(Number(userId));
    }
    this.loadUserStats();
  }

  // Load individual user data
  loadUserData(userId: number): void {
    this.isLoading = true;
    this.userService.getUserById(userId).subscribe((data) => {
      this.user = data;
      this.isLoading = false;
    }, error => {
      this.isLoading = false;
      alert('Error fetching user data');
    });
  }

  // Load user statistics (total users, active users)
  loadUserStats(): void {
    this.userService.getTotalUsers().subscribe((data) => {
      this.totalUsers = data.totalUsers;
      this.activeUsers = data.activeUsers;
    }, error => {
      alert('Error fetching user statistics');
    });
  }

  // Save or update user data
  saveUser(): void {
    if (this.isEditing) {
      this.userService.updateUser(this.user.userId, this.user).subscribe(() => {
        alert('User updated successfully');
      });
    } else {
      this.userService.createUser(this.user).subscribe((createdUser) => {
        this.user = createdUser;
        alert('User created successfully');
      });
    }
  }

  // Navigate to different parts of the app
  navigateToDashboard(): void {
    this.router.navigate(['/dashboard']);
  }

  viewProfile(): void {
    this.router.navigate(['/profile']);
  }

  navigateToSettings(): void {
    this.router.navigate(['/settings']);
  }

  editUser(): void {
    this.router.navigate(['/edit-user', this.user.userId]);
  }

  deactivateUser(): void {
    if (confirm('Are you sure you want to deactivate this user?')) {
      this.userService.deactivateUser(this.user.userId).subscribe(() => {
        alert('User account deactivated');
        this.user.isActive = false;
      });
    }
  }

  // Navigate back to previous page
  goBack(): void {
    this.location.back();
  }
}
