using Agricaltech.BL;

namespace Agricaltech.DL;
public interface IDbRepository
{
    Task<IEnumerable<Attendee>> GetAttendee(string MobileNo);
}
