using Microsoft.AspNetCore.SignalR;

public class MyHub : Hub<IMyHub>
{
    public string GetConnectionId()
    {
        return Context.ConnectionId;
    }

    public async Task AddToGroup(string connectionId,string groupName)
    {
        await Groups.AddToGroupAsync(connectionId,groupName);
    }

    public async Task RemoveFromGroup(string connectionId,string groupName)
    {
        await Groups.RemoveFromGroupAsync(connectionId,groupName);
    }
}