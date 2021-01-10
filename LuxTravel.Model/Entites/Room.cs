﻿using System;
using System.Collections.Generic;
using System.Text;

namespace LuxTravel.Model.Entities
{
    public class Room
    {
        public System.Guid Id { get; set; }
        public string Name { get; set; }
        public System.Guid HotelId { get; set; }
        public Guid RoomTypeId { get; set; }
        public int RoomFloor { get; set; }
        public int RoomNumber { get; set; }
        public Guid RoomStatusId { get; set; }
        public virtual Hotel Hotel { get; set; }

        public Decimal CurrentPrice { get; set; }   
        public  virtual RoomType RoomType { get; set; }
        //public virtual ICollection<BookingDetail> RoomDetails { get; set; }



    }
}
