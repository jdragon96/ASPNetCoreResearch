using Blazored.LocalStorage;

namespace Library.Client
{
    public class APIService
    {
        private readonly ILocalStorageService _localStorage;
        private readonly HttpClient _HttpClient;
        public APIService(ILocalStorageService localStorage, IHttpClientFactory HttpClientFactory)
        {
            _localStorage = localStorage;
            _HttpClient = HttpClientFactory.CreateClient("API");
        }

        public async Task<T?> POST<T>(string uri, StringContent content)
        {
            try
            {
                var res = await _HttpClient.PostAsJsonAsync(uri, content);
                if (res.IsSuccessStatusCode)
                {
                    return await res.Content.ReadFromJsonAsync<T>();
                }
            }
            catch (Exception ex)
            {

            }
            return default(T);
        }

        public async Task<T?> POSTAuth<T>(string uri, StringContent content)
        {
            try
            {
                if (!await SetToken())
                    return default(T);
                var res = await _HttpClient.PostAsJsonAsync(uri, content);
                if (res.IsSuccessStatusCode)
                {
                    return await res.Content.ReadFromJsonAsync<T>();
                }
            }
            catch (Exception ex)
            {

            }
            return default(T);
        }

        public async Task<T?> GETAuth<T>(string uri)
        {
            try
            {

                if (!await SetToken())
                    return default(T);
                var res = await _HttpClient.GetAsync(uri);
                if (res.IsSuccessStatusCode)
                {
                    return await res.Content.ReadFromJsonAsync<T>();
                }
            }
            catch (Exception ex)
            {
                var a = 10;
                var msg = ex.Message;
            }
            return default(T);
        }


        private async Task<bool> SetToken()
        {
            var token = await _localStorage.GetItemAsync<string>("access_token");
            if (token == null)
                return false;
            _HttpClient.DefaultRequestHeaders.Remove("Authorization");
            _HttpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
            return true;
        }
    }
}
