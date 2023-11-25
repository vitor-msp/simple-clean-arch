using System.Text.Json;

namespace SimpleCleanArch.Api.Presenters;

public class ErrorPresenter
{
    public string ErrorMessage { get; private set; } = "";

    private ErrorPresenter() { }

    public static string From(string errorMessage)
    {
        var output = new ErrorPresenter()
        {
            ErrorMessage = errorMessage
        };
        return JsonSerializer.Serialize(output);
    }
}