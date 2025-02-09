namespace SimpleCleanArch.Application.Contract;

public interface ISendMailGateway
{
    Task Send(string message);
}