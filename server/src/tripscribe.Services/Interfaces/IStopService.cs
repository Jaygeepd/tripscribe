using tripscribe.Services.DTOs;

namespace tripscribe.Services.Services;

public interface IStopService
{
    IList<StopDTO> GetStop(int id);

    IList<StopDTO> GetStops(string? name = null, DateTime? startArrivedDate = null, DateTime? endArrivedDate = null, DateTime? startDepartedDate = null, DateTime? endDepartedDate = null, int? journeyId = null);
    void UpdateStop(int id, StopDTO stop);

}