
import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-hotel-details',
  standalone: true,
  imports: [CommonModule, RouterLink],
  templateUrl: './hotel-details.html',
  styleUrl: './hotel-details.css',
})
export class HotelDetails {

  hotel = {
    id: 1,
    name: 'Grand Palace Hotel',
    city: 'Chennai',
    location: 'T Nagar, Chennai',
    rating: 5,
    price: 4500,
    description: 'Experience premium comfort and luxury with spacious rooms and excellent hospitality.',
    amenities: [
      'Free Wi-Fi',
      'Swimming Pool',
      'Restaurant',
      'Air Conditioning',
      'Room Service',
      'Parking'
    ]
  };

}
