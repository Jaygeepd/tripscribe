﻿using tripscribe.Services.DTOs;

namespace tripscribe.Services.Services;

public interface IStopService
{
    StopDTO GetStop(int id);

    IList<StopDTO> GetStops(string? name = null, DateTime? startArrivedDate = null, DateTime? endArrivedDate = null, DateTime? startDepartedDate = null, DateTime? endDepartedDate = null, int? journeyId = null);

    void CreateStop(StopDTO stop);
    
    void UpdateStop(int id, StopDTO stop);

    void DeleteStop(int id);


    IList<ReviewDTO> GetStopReviews(int id);
}