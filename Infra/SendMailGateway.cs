using SimpleCleanArch.Application.Contract;

namespace SimpleCleanArch.Infra;

public class SendMailGateway : ISendMailGateway
{
    public async Task Send(string message)
    {
        Console.WriteLine(message);
    }
}