namespace Thanatos;

using Discord;
using Discord.WebSocket;
using DotNetEnv;

class Program
{
    private DiscordSocketClient _client = new();

    static async Task Main()
        => await new Program().RunBotAsync();

    private async Task RunBotAsync()
    {
        Env.Load();
        string token = Environment.GetEnvironmentVariable("DISCORD_TOKEN")!;

        _client = new DiscordSocketClient();
        _client.Log += LogAsync;
        _client.MessageReceived += MessageReceivedAsync;

        await _client.LoginAsync(TokenType.Bot, token);
        await _client.StartAsync();

        await Task.Delay(-1);
    }

    private Task LogAsync(LogMessage log)
    {
        Console.WriteLine(log);
        return Task.CompletedTask;
    }

    private async Task MessageReceivedAsync(SocketMessage message)
    {
        if (message.Content == "!ping")
            await message.Channel.SendMessageAsync("Pong!");
    }
}