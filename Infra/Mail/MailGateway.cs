using Microsoft.Extensions.Options;
using SimpleCleanArch.Domain.Contract.Infra;

namespace SimpleCleanArch.Infra;

#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
public class MailGateway : IMailGateway
{
    private readonly string _connectionString;

    public MailGateway()
    {
        _connectionString = "";
    }

    public MailGateway(IOptions<MailConfiguration> options)
    {
        _connectionString = options.Value.ConnectionString
          ?? throw new Exception("Missing Mail Gateway configuration.");
    }

    public async Task SendMail(SendMailInput input)
    {
        Console.WriteLine(input.Recipient);
    }
}
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously