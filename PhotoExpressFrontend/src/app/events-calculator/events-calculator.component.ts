import { Component } from '@angular/core';
import { FormGroup, FormControl, ReactiveFormsModule } from '@angular/forms';

@Component({
  selector: 'app-events-calculator',
  standalone: true,
  imports: [ReactiveFormsModule], 
  templateUrl: './events-calculator.component.html',
  styleUrl: './events-calculator.component.css'
})


export class EventsCalculatorComponent {
  costForm = new FormGroup({
    numberOfStudents: new FormControl(1),
    baseServiceCost: new FormControl(200), // Precio base del servicio
    capAndGown: new FormControl(false) // Checkbox para toga y birrete
  });

  totalCost = 0;

  calculateTotal() {
    if(this.costForm.value.baseServiceCost &&this.costForm.value.numberOfStudents){
      const baseCost = this.costForm.value.baseServiceCost;
      const numStudents = this.costForm.value.numberOfStudents;
      const additionalCost = this.costForm.value.capAndGown ? 20 : 0; // Costo adicional por estudiante si se selecciona toga y birrete
      this.totalCost = baseCost + numStudents * additionalCost;
    }

  }
}
