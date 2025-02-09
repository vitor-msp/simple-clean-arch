namespace SimpleCleanArch.Domain.Contract.Infra;

public interface IMailGateway
{
    Task SendMail(SendMailInput input);
}

public record SendMailInput(string Recipient, string Subject, string Body);