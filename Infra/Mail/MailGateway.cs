using Microsoft.Extensions.Options;
using SimpleCleanArch.Application.Contract;

namespace SimpleCleanArch.Infra;

public class MailGateway(IOptions<MailConfiguration> options) : IMailGateway
{
    private readonly string _connectionString = options.Value.ConnectionString
        ?? throw new Exception("Missing Mail Gateway configuration.");

    public async Task SendMail(string message)
    {
        Console.WriteLine(message);
    }
}