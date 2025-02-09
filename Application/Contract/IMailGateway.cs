namespace SimpleCleanArch.Application.Contract;

public interface IMailGateway
{
    Task SendMail(string message);
}