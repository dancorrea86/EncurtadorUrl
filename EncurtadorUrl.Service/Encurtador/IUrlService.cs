namespace EncurtadorUrl.Service.Encurtador
{
    public interface IUrlService
    {
        string EncurtarUrl(string urlOriginal, string esquema, string host);

        string Redirect(string hash);
    }
}
