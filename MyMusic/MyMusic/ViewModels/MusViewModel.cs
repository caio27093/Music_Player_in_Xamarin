using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using MyMusic.Models;
using Newtonsoft.Json;

namespace MyMusic.ViewModels
{


    public class MusViewModel
    {
        //MyMusic.View.Musicas m = new View.Musicas();
        public MusViewModel()
        {
            _  = ConsultaMusicaFavoritaAsync();
        }

        public async Task ConsultaMusicaFavoritaAsync()
        {

            var teste = await Service.MusicaService.ConsultarMusicas();
            List<Musica> listaDeMusicas = new List<Musica>();
            XmlDocument retornoMusica = new XmlDocument();
            retornoMusica = (XmlDocument)JsonConvert.DeserializeXmlNode(Convert.ToString(teste), "Music");
            foreach (XmlNode retorno in retornoMusica.SelectNodes("Music"))
            {
                foreach (XmlNode musica in retorno.SelectNodes("Music"))
                {
                    int xid = 0;
                    int xid_album = 0;
                    string xname = string.Empty;
                    string xartist = string.Empty;
                    string xcover = string.Empty;
                    string xcolor = string.Empty;
                    string xaudio = string.Empty;
                    bool xfavorito = false;
                    string ximage = string.Empty;

                    if (musica.SelectSingleNode("id") != null) xid = Convert.ToInt32(musica.SelectSingleNode("id").InnerText);
                    if (musica.SelectSingleNode("id_album") != null) xid_album = Convert.ToInt32(musica.SelectSingleNode("id_album").InnerText);
                    if (musica.SelectSingleNode("name") != null) xname = musica.SelectSingleNode("name").InnerText;
                    if (musica.SelectSingleNode("artist") != null) xartist = musica.SelectSingleNode("artist").InnerText;
                    if (musica.SelectSingleNode("cover") != null) xcover = musica.SelectSingleNode("cover").InnerText;
                    if (musica.SelectSingleNode("color") != null) xcolor = (musica.SelectSingleNode("color").InnerText);
                    if (musica.SelectSingleNode("audio") != null) xaudio = musica.SelectSingleNode("audio").InnerText;
                    if (musica.SelectSingleNode("isFavorite") != null) xfavorito = Convert.ToBoolean(musica.SelectSingleNode("isFavorite").InnerText);


                    var contador = listaDeMusicas.Where(x => x.name == xname).ToList();
                    if (contador != null)
                    {
                        if (contador.Count() == 0)
                        {
                                if (xfavorito == true)
                                {
                                    ximage = "coracaonormal";
                                }
                                else
                                {
                                    ximage = "coracaobazul";
                                }
                            listaDeMusicas.Add(new Musica()
                            {
                                id = xid,
                                name = xname,
                                artist = xartist,
                                cover = xcover,
                                color = xcolor,
                                audio = xaudio,
                                isFavorite = xfavorito,
                                HeartImage = ximage


                            });




                            //QueueMusicString.Add(xaudio);
                        }
                    }


                }
            }
            Musicas = new ObservableCollection<Musica> ( listaDeMusicas );  


        }



        public async Task<List<Musica>>  ConsultaSources(int id_album)
        {

            var teste = await Service.MusicaService.ConsultarMusicas();
            List<Musica> listaDeMusicas = new List<Musica>();
            XmlDocument retornoMusica = new XmlDocument();
            retornoMusica = (XmlDocument)JsonConvert.DeserializeXmlNode(Convert.ToString(teste), "Music");
            foreach (XmlNode retorno in retornoMusica.SelectNodes("Music"))
            {
                foreach (XmlNode musica in retorno.SelectNodes("Music"))
                {

                    string xaudio = string.Empty;
                    string xname = string.Empty;
                    string xartist = string.Empty;
                    int xid_album = 0;

                    if (musica.SelectSingleNode("audio") != null) xaudio = musica.SelectSingleNode("audio").InnerText;
                    if (musica.SelectSingleNode("id_album") != null) xid_album = Convert.ToInt32(musica.SelectSingleNode("id_album").InnerText);
                    if (musica.SelectSingleNode("name") != null) xname = (musica.SelectSingleNode("name").InnerText);
                    if (musica.SelectSingleNode("artist") != null) xartist = (musica.SelectSingleNode("artist").InnerText);


                    var contador = listaDeMusicas.Where(x => x.audio == xaudio).ToList();
                    if (contador != null)
                    {
                        if (contador.Count() == 0)
                        {
                            if (id_album == xid_album)
                            {
                                listaDeMusicas.Add(new Musica()
                                {
                                    audio = xaudio,
                                    name = xname,
                                    artist = xartist

                                });
                            }

                            //QueueMusicString.Add(xaudio);
                        }
                    }


                }
            }

            return listaDeMusicas;

        }

        public async Task<List<Musica>> ConsultaTodas()
        {

            var teste = await Service.MusicaService.ConsultarMusicas();
            List<Musica> listaDeMusicas = new List<Musica>();
            XmlDocument retornoMusica = new XmlDocument();
            retornoMusica = (XmlDocument)JsonConvert.DeserializeXmlNode(Convert.ToString(teste), "Music");
            foreach (XmlNode retorno in retornoMusica.SelectNodes("Music"))
            {
                foreach (XmlNode musica in retorno.SelectNodes("Music"))
                {

                    string xaudio = string.Empty;
                    string xname = string.Empty;
                    string xartist = string.Empty;
                    int xid_album = 0;
                    bool xfavorito = false;

                    if (musica.SelectSingleNode("audio") != null) xaudio = musica.SelectSingleNode("audio").InnerText;
                    if (musica.SelectSingleNode("id_album") != null) xid_album = Convert.ToInt32(musica.SelectSingleNode("id_album").InnerText);
                    if (musica.SelectSingleNode("isFavorite") != null) xfavorito = Convert.ToBoolean(musica.SelectSingleNode("isFavorite").InnerText);
                    if (musica.SelectSingleNode("name") != null) xname = (musica.SelectSingleNode("name").InnerText);
                    if (musica.SelectSingleNode("artist") != null) xartist = (musica.SelectSingleNode("artist").InnerText);


                    var contador = listaDeMusicas.Where(x => x.audio == xaudio).ToList();
                    if (contador != null)
                    {
                        if (contador.Count() == 0)
                        {

                                listaDeMusicas.Add(new Musica()
                                {
                                    audio = xaudio,
                                    name = xname,
                                    artist = xartist

                                });
                            
                            

                            //QueueMusicString.Add(xaudio);
                        }
                    }


                }
            }

            return listaDeMusicas;

        }


        public async Task<List<Musica>> ConsultaTodasAlbum(int idAlb)
        {

            var teste = await Service.MusicaService.ConsultarMusicas();
            List<Musica> listaDeMusicas = new List<Musica>();
            XmlDocument retornoMusica = new XmlDocument();
            retornoMusica = (XmlDocument)JsonConvert.DeserializeXmlNode(Convert.ToString(teste), "Music");
            foreach (XmlNode retorno in retornoMusica.SelectNodes("Music"))
            {
                foreach (XmlNode musica in retorno.SelectNodes("Music"))
                {

                    string xaudio = string.Empty;
                    string xname = string.Empty;
                    string xartist = string.Empty;
                    int xid_album = 0;
                    bool xfavorito = false;

                    if (musica.SelectSingleNode("audio") != null) xaudio = musica.SelectSingleNode("audio").InnerText;
                    if (musica.SelectSingleNode("id_album") != null) xid_album = Convert.ToInt32(musica.SelectSingleNode("id_album").InnerText);
                    if (musica.SelectSingleNode("isFavorite") != null) xfavorito = Convert.ToBoolean(musica.SelectSingleNode("isFavorite").InnerText);
                    if (musica.SelectSingleNode("name") != null) xname = (musica.SelectSingleNode("name").InnerText);
                    if (musica.SelectSingleNode("artist") != null) xartist = (musica.SelectSingleNode("artist").InnerText);


                    var contador = listaDeMusicas.Where(x => x.audio == xaudio).ToList();
                    if (contador != null)
                    {
                        if (contador.Count() == 0)
                        {
                            if (xid_album== idAlb)
                            {

                                listaDeMusicas.Add(new Musica()
                                {
                                    audio = xaudio,
                                    artist = xartist,
                                    name =xname

                                });
                            }

                            //QueueMusicString.Add(xaudio);
                        }
                    }


                }
            }

            return listaDeMusicas;

        }


        public async Task<List<Musica>> ConsultaFavoritas()
        {

            var teste = await Service.MusicaService.ConsultarMusicas();
            List<Musica> listaDeMusicas = new List<Musica>();
            XmlDocument retornoMusica = new XmlDocument();
            retornoMusica = (XmlDocument)JsonConvert.DeserializeXmlNode(Convert.ToString(teste), "Music");
            foreach (XmlNode retorno in retornoMusica.SelectNodes("Music"))
            {
                foreach (XmlNode musica in retorno.SelectNodes("Music"))
                {

                    string xaudio = string.Empty;
                    string xname = string.Empty;
                    string xartist = string.Empty;
                    int xid_album = 0;
                    bool xfavorito = false;

                    if (musica.SelectSingleNode("audio") != null) xaudio = musica.SelectSingleNode("audio").InnerText;
                    if (musica.SelectSingleNode("id_album") != null) xid_album = Convert.ToInt32(musica.SelectSingleNode("id_album").InnerText);
                    if (musica.SelectSingleNode("isFavorite") != null) xfavorito = Convert.ToBoolean(musica.SelectSingleNode("isFavorite").InnerText);
                    if (musica.SelectSingleNode("name") != null) xname = (musica.SelectSingleNode("name").InnerText);
                    if (musica.SelectSingleNode("artist") != null) xartist = (musica.SelectSingleNode("artist").InnerText);


                    var contador = listaDeMusicas.Where(x => x.audio == xaudio).ToList();
                    if (contador != null)
                    {
                        if (contador.Count() == 0)
                        {
                            if (xfavorito)
                            {
                                listaDeMusicas.Add(new Musica()
                                {
                                    audio = xaudio,
                                    name = xname,
                                    artist = xartist

                                });
                            }

                            //QueueMusicString.Add(xaudio);
                        }
                    }


                }
            }

            return listaDeMusicas;

        }

        public ObservableCollection<Musica> Musicas { get; set; }
        public ObservableCollection<MusicaList> Mp3UrlList { get; set; }

        public ObservableCollection<Album> Albums { get; set; }


    }


}
