using BuenoStore.BuildingBlocks.Models;
using Newtonsoft.Json;
using System.Net;
using System.Text;

namespace BuenoStore.Gateway.Api.Services
{
    public abstract class ServiceBase
    {
        protected StringContent ObterConteudo(object dado) => new StringContent(JsonConvert.SerializeObject(dado), Encoding.UTF8, "application/json");

        protected async Task<T> DeserializarObjetoResponse<T>(HttpResponseMessage responseMessage) 
            => JsonConvert.DeserializeObject<T>(await responseMessage.Content.ReadAsStringAsync());

        protected bool TratarErrosResponse(HttpResponseMessage response)
        {
            if (response.StatusCode == HttpStatusCode.BadRequest) 
                return false;

            response.EnsureSuccessStatusCode();
            return true;
        }

        protected ResponseResult RetornoOk()
        {
            return new ResponseResult();
        }
    }
}
