using BeOneSender.BusinessLogic.Services.Bootstrap;

namespace BeOneSender.Gui.Aspnet.Data;

public class BootrapService : IBootrapService
{
    private readonly IConfiguration _configuration;
    private readonly ILoadInformation _loadInformation;

    public BootrapService(IConfiguration configuration, ILoadInformation loadInformation)
    {
        _configuration = configuration;
        _loadInformation = loadInformation;
    }

    public async Task<bool> BootstrapAsync()
    {
        await _loadInformation.LoadData("..");
        return true;
    }
}