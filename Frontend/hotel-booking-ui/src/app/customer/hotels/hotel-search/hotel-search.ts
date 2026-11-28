
import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { RouterLink } from '@angular/router';
@Component({
  selector: 'app-hotel-search',
  standalone: true,
  imports: [CommonModule, FormsModule, RouterLink],
  templateUrl: './hotel-search.html',
  styleUrl: './hotel-search.css',
})
export class HotelSearch {

  city = '';
  checkInDate = '';
  checkOutDate = '';
  guests = 1;

  hasSearched = false;
  selectedHotel: any = null;

  hotels = [
    {
      id: 1,
      name: 'Grand Palace Hotel',
      city: 'Chennai',
      location: 'T Nagar, Chennai',
      rating: 5,
      price: 4500,
      description: 'Experience premium comfort and luxury with spacious rooms and excellent hospitality.',
      imageClass: 'hotel-one'
    },
    {
      id: 2,
      name: 'Ocean View Resort',
      city: 'Pondicherry',
      location: 'White Town, Pondicherry',
      rating: 4,
      price: 3200,
      description: 'Enjoy a relaxing stay with beautiful coastal surroundings and comfortable rooms.',
      imageClass: 'hotel-two'
    },
    {
      id: 3,
      name: 'Green Valley Stay',
      city: 'Ooty',
      location: 'Fern Hill, Ooty',
      rating: 4,
      price: 2800,
      description: 'A peaceful getaway surrounded by greenery and scenic hill station views.',
      imageClass: 'hotel-three'
    },
    {
      id: 4,
      name: 'Royal Heritage Hotel',
      city: 'Madurai',
      location: 'Near Meenakshi Temple, Madurai',
      rating: 5,
      price: 3800,
      description: 'Discover traditional hospitality with modern amenities in the heart of Madurai.',
      imageClass: 'hotel-four'
    }
  ];

  filteredHotels = [...this.hotels];

  searchHotels() {
    this.hasSearched = true;

    const searchCity = this.city.trim().toLowerCase();

    this.filteredHotels = this.hotels.filter(hotel =>
      hotel.city.toLowerCase().includes(searchCity)
    );

    this.selectedHotel = null;
  }

  resetSearch() {
    this.city = '';
    this.checkInDate = '';
    this.checkOutDate = '';
    this.guests = 1;

    this.filteredHotels = [...this.hotels];
    this.hasSearched = false;
    this.selectedHotel = null;
  }

  viewHotel(hotel: any) {
    this.selectedHotel = hotel;
  }
}
