﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using CommonFunctionality.Api;
using CommonFunctionality.Helper;
using LuxTravel.Api.Core.Commands;
using LuxTravel.Api.Core.Queries;
using LuxTravel.Model.Dtos;

namespace LuxTravel.Api.Controllers
{
    public class HotelController : ApiControllerBase
    {

        public HotelController(IServiceProvider serviceProvider) : base(serviceProvider)
        {

        }
        //[AllowAnonymous]
        [HttpGet("api/hotels")]
        public async Task<PagedList<HotelDto>> Get([FromQuery] GetAllHotelsQuery model)
        {
            return await SendRequestAsync(model);
        }

        [HttpGet("api/hotel/{id}")]
        public async Task<HotelDto> Get(Guid id)
        {
            var result = await SendRequestAsync(new GetDetailHotelQuery() { Id = id });

            return result;
        }
        [HttpPost("api/hotel")]
        public async Task<bool> Create(CreateHotelCommand command)
        {
            var result = await SendRequestAsync(command);

            return result;
        }
    }
}