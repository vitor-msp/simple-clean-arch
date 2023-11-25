using SimpleCleanArch.Application.Contract.Gateways;

namespace SimpleCleanArch.Infra;

public class SendMailGateway: ISendMailGateway
{
    public void Send(string message) {
        Console.WriteLine(message);
    }
}