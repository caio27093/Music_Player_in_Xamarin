using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using MyMusic.Models;
using Plugin.Connectivity;

namespace MyMusic.Service
{
    public class AlbumService
    {


        static HttpClient requisicao = new HttpClient();

        public static async Task<string> ConsultarAlbums()
        {
            try
            {

                string conexao = CrossConnectivity.Current.IsConnected ? "Connected" : "Disconnected";
                if (conexao == "Connected")
                {

                    //string json = JsonConvert.SerializeObject(Lista);

                    //Método do WebService
                    string url = "https://demo1082271.mockable.io/Albums";

                    var response = await requisicao.GetStringAsync(url);

                    return (response);
                }
                else
                {

                    return conexao;

                }
            }
            catch (Exception erro)
            {
                return null;
            }
        }

    }
}
