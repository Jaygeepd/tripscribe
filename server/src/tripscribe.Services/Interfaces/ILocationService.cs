﻿using tripscribe.Services.DTOs;

namespace tripscribe.Services.Services;

public interface ILocationService
{
    IList<LocationDTO> GetLocation(int id);
    
    IList<LocationDTO> GetLocations(string? name, string? locationType, DateTime? startDate, DateTime? endDate,
        int? stopId);

    void UpdateLocation(int Id, LocationDTO location);
}