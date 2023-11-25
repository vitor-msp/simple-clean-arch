namespace SimpleCleanArch.Application.Contract.Gateways;

public interface ISendMailGateway
{
    void Send(string message);
}