

using Agricaltech.BL;
using Dapper;

namespace Agricaltech.DL;
public static class DbRepository
{
    public static async Task<IEnumerable<Attendee>> GetAttendee(string MobileNo, DbContext context)
    {
        string query =  $"select * from dbo.attendees where MobileNo = '{MobileNo}'";
        try
        {
            var con = context.CreateConnection();
            var attendees =await con.QueryAsync<Attendee>(query);
            return attendees;
        }
        catch (System.Exception e)
        {
            System.Console.WriteLine(e.Message);
            throw new NotImplementedException();
        }
    }
}