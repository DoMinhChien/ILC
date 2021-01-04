﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LuxTravel.Model.Dtos;
using MediatR;

namespace LuxTravel.Api.Core.Queries
{
    public class GetAllRoomTypesQuery : IRequest<IEnumerable<RoomTypeDto>>
    {
    }
}
