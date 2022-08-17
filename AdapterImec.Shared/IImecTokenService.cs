namespace AdapterImec.Shared;

public  interface IImecTokenService
{
    Task<string?> GetTokenAsync();
}
