using EncurtadorUrl.Data.Context;
using EncurtadorUrl.Data.Model;

namespace EncurtadorUrl.Service.Encurtador
{
    public class UrlService : IUrlService
    {
        private readonly EncurtadorUrlDbContext _contexto;
        
        public UrlService(EncurtadorUrlDbContext contexto)
        {
            _contexto = contexto;
        }

        public string EncurtarUrl(string urlOriginal, string esquema, string host)
        {
            string hash = Math.Abs(urlOriginal.GetHashCode()).ToString("x");
            string urlFinal = $"{esquema}://{host}/{hash}";
            DateTime dataExpiração = DateTime.Today.AddDays(15);

            _contexto.Urls.Add(new Url { UrlOriginal = urlOriginal, UrlEncurtada = urlFinal, DataExpiracao = dataExpiração });
            _contexto.SaveChanges();

            return urlFinal;
        }

        public string Redirect(string hash)
        {
            var url = _contexto.Urls.Where(u => u.UrlEncurtada.EndsWith(hash)).FirstOrDefault();

            if (url != null)
            {
                return url.UrlOriginal;
            }

            return null;
        }
    }
}
