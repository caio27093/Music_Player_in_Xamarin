using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using MyMusic.Models;
using Plugin.Connectivity;

namespace MyMusic.Service
{
    public class MusicaService
    {


        static HttpClient requisicao = new HttpClient();

        public static async Task<string> ConsultarMusicas()
        {
            try
            {

                string conexao = CrossConnectivity.Current.IsConnected ? "Connected" : "Disconnected";
                if (conexao == "Connected")
                {
                    string resultado;
                    string url = "http://demo1082271.mockable.io/User-Music";
                    HttpWebRequest request;
                    request = (HttpWebRequest)WebRequest.Create ( url );// Cria a Url da requisição.
                    request.Headers.Clear ( );
                    request.ContentType = "application/json";
                    request.Method = "GET";

                    WebResponse retorno = request.GetResponse ( );

                    using (Stream stream = request.GetResponse ( ).GetResponseStream ( ))
                    {
                        StreamReader reader = new StreamReader ( stream, Encoding.UTF8 );

                        resultado = reader.ReadToEnd ( );
                    }



                    return (resultado);
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
