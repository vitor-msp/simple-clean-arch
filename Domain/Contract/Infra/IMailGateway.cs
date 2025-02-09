namespace SimpleCleanArch.Domain.Contract.Infra;

public interface IMailGateway
{
    Task SendMail(string message);
}