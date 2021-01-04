﻿using System;
using System.Collections.Generic;
using System.Text;
using LuxTravel.Model.Entites;

namespace LuxTravel.Model.Entities
{
    public class Hotel
    {
        public System.Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Url { get; set; }
        public bool IsDeleted { get; set; }
        public Guid? CreatedBy { get; set; }
        public System.DateTime CreatedOn { get; set; }
        public Guid? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public Guid? HotelLocationId { get; set; }
        public virtual ICollection<Booking> Bookings { get; set; }
        public virtual ICollection<Room> Rooms { get; set; }
        public virtual HotelLocation HotelLocation { get; set; }
        public  virtual ICollection<HotelRating> Ratings { get; set; }

    }
}
