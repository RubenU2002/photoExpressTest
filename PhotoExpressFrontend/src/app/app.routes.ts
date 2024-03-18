import { Routes } from '@angular/router';
import { EventsComponent } from './events/events.component'; // Asegúrate de que este componente existe
import { EventsCalculatorComponent } from './events-calculator/events-calculator.component';
import { EditEventComponent } from './edit-event/edit-event.component';
import { EventModificationLogComponent } from './event-modification-log/event-modification-log.component';
import { ModificationDetailsComponent } from './modification-details/modification-details.component';
export const routes: Routes = [
  { path: 'events', component: EventsComponent },
  { path: 'events-calculator', component: EventsCalculatorComponent },
  {path: 'EditEvent/:eventId', component:EditEventComponent},
  {path:'EventModificationLog/:eventId', component:EventModificationLogComponent},
  {path:'modificationDetails', component:ModificationDetailsComponent}
];
