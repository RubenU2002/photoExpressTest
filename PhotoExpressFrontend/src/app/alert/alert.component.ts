import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-alert',
  standalone:true,
  templateUrl: './alert.component.html',
  styleUrls: ['./alert.component.css']
})
export class AlertComponent {
  @Input() message: string = '';
  @Input() type: string = 'success'; // Tipos esperados: 'success' o 'error'
  get alertClass() {
    return {
      'alert-success': this.type === 'success',
      'alert-error': this.type === 'error'
    };
  }
  
}
