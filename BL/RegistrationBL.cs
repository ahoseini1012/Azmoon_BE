
using Agricaltech.DL;

namespace Agricaltech.BL;
public static class RegistrationBL
{
    public static async Task<IEnumerable<Attendee>> GetAttendeeByMobile(string MobileNo,DbContext context)
    {
        var result = await DbRepository.GetAttendee(MobileNo,context);
        return result;
    }
}