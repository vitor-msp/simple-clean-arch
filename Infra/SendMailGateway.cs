using SimpleCleanArch.Application.Contract.Gateways;

namespace SimpleCleanArch.Infra;

public class SendMailGateway : ISendMailGateway
{
    public async Task Send(string message)
    {
        Console.WriteLine(message);
    }
}