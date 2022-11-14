using tripscribe.Services.DTOs;

namespace tripscribe.Services.Services;

public interface IStopService
{
    IList<StopDTO> GetStops(int? id = null, string? name = null, DateTime? startArrivedDate = null, DateTime? endArrivedDate = null, DateTime? startDepartedDate = null, DateTime? endDepartedDate = null, int? journeyId = null);
    void UpdateStop(int id, StopDTO stop);

}