using Microsoft.AspNetCore.SignalR;

public class MyHub : Hub<IMyHub>
{
    public void SayHello()
    {
        Clients.Caller.DisplayMessage("hello from server");
    }
}