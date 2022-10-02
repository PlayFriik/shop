﻿using Base.Infrastructure.Services;
using Shop.Application.Models.v1;
using Shop.Application.Repositories.v1;
using Shop.Application.Services.v1;

namespace Shop.Infrastructure.Services.v1;

public class LocationService : BaseService<Location, ILocationRepository>, ILocationService
{
    public LocationService(ILocationRepository locationRepository) : base(locationRepository)
    {
    }
}