namespace SimpleCleanArch.Application.Contract.Gateways;

public interface ISendMailGateway
{
    Task Send(string message);
}