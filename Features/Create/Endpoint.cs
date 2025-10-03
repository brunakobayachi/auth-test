namespace Features.Create;

public class Endpoint : Endpoint<Request, Response>
{
    public override void Configure()
    {
        Post("/api/user/create");
        
    }

    public override async Task HandleAsync(Request req, CancellationToken ct)
    {
        await Send.OkAsync(new()
        {
            Name = req.Name,
            Age = req.Age,
            Color = req.Color
        });
    }
}
