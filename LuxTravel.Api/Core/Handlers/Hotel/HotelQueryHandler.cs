﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CommonFunctionality.Core;
using LuxTravel.Api.Core.Queries;
using LuxTravel.Model.Dtos;
using MediatR;
using CommonFunctionality.Helper;
using LuxTravel.Model.BaseRepository;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace LuxTravel.Api.Core.Handlers.Hotel
{
    public class HotelQueryHandler : RequestHandlerBase,
        IRequestHandler<GetAllHotelsQuery, IEnumerable<HotelDto>>,
        IRequestHandler<GetDetailHotelQuery, HotelDetailDto>

    {
        private readonly UnitOfWork _unitOfWork = new UnitOfWork();
        public HotelQueryHandler(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }


        public Task<IEnumerable<HotelDto>> Handle(GetAllHotelsQuery request, CancellationToken cancellationToken)
        {
            string listRoomTypes = string.Empty;
            if (request.RoomTypeIds != null && request.RoomTypeIds.Any())
            {
                listRoomTypes = string.Join(",", request.RoomTypeIds.Select(r => r));
            }

            if (string.IsNullOrEmpty(request.Sort))
            {
                request.Sort = " Name ";
            }

            if (request.PageIndex <1)
            {
                request.PageIndex = 1;
            }

            if (request.PageSize <10)
            {
                request.PageSize = 10;
            }
            var data = _unitOfWork.Context.SpGetListHotel.FromSqlInterpolated($@"EXEC [dbo].[GetListHotel] @CityId = {request.CityId},  @RoomTypeIds={listRoomTypes} , @Rating = {request.Rating}, @GuestCount = {request.GuestCount}, @PageIndex = {request.PageIndex}, @PageSize = {request.PageSize}, @Sort = {request.Sort}").ToList();

            var records = _mapper.Map<IEnumerable<HotelDto>>(data);

            return Task.FromResult(records);
        }

        private async Task GetSmallestPrice(Guid hotelId)
        {
            var rooms = await _unitOfWork.RoomRepository.GetMany(r => r.HotelId == hotelId);

            //var smallestPrice = rooms.Min(r=>r.Pr)

        }

        private async Task<List<string>> GetHotelImages(Guid id)
        {
            var images = await _unitOfWork.PhotoRepository.GetMany(r => r.ObjectId == id);
            var imageUrls = new List<string>();
            if (images != null && images.Any())
            {
                foreach (var image in images)
                {
                    imageUrls.Add(image.Url);
                }
            }

            return imageUrls;
        }
        private async Task<List<AvailableRoomDto>> GetRoomForHotel(Guid hotelId)
        {
            var rooms = _unitOfWork.Context.SpGetRoomByHotels
                .FromSqlInterpolated($"GetRoomByHotelId {hotelId} ").ToList();
            var result = _mapper.Map<List<AvailableRoomDto>>(rooms);
            if (result.Count >0)
            {
                //Get image for room
                var roomIds = result.Select(r => r.RoomId).ToList();
                var photos = await _unitOfWork.PhotoRepository.GetMany(r => roomIds.Contains(r.ObjectId));
                result.ForEach(r =>
                {
                    var pics = photos.Where(p => p.ObjectId == r.RoomId).ToList();
                    r.ImageUrls = pics.Select(c => c.Url).ToList();
                });
            }
            return result;
        }
        public async Task<HotelDetailDto> Handle(GetDetailHotelQuery request, CancellationToken cancellationToken)
        {
            //Get all hotel belong to location which have respective city
            var selectedHotel = await _unitOfWork.HotelRepository.GetByIdAsync(request.Id);
            if (selectedHotel != null)
            {
                //Get rooms
                var rooms = await GetRoomForHotel(selectedHotel.Id);
                //Get Images for hotel
                var imageUrls = await GetHotelImages(selectedHotel.Id);
                var result =
                    new HotelDetailDto()
                    {
                        Id = selectedHotel.Id,
                        DateFrom = request.DateFrom,
                        DateTo = request.DateTo,
                        GuestId = GuestId,
                        GuestCount = request.GuestCount,
                        HotelName = selectedHotel.Name,
                        AvailableRooms = rooms,
                        ImageUrls = imageUrls
                    };

                return result;
            }
            return new HotelDetailDto();
        }
    }
}
