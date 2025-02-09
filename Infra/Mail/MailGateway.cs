using Microsoft.Extensions.Options;
using SimpleCleanArch.Domain.Contract.Infra;

namespace SimpleCleanArch.Infra;

public class MailGateway(IOptions<MailConfiguration> options) : IMailGateway
{
    private readonly string _connectionString = options.Value.ConnectionString
        ?? throw new Exception("Missing Mail Gateway configuration.");

    public async Task SendMail(SendMailInput input)
    {
        Console.WriteLine(input.Recipient);
    }
}