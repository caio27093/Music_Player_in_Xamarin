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
    public class PlayerModelView
    {
        int id;
        int idmus;
        public PlayerModelView(int id_Alb)
        {
            id = id_Alb;

            _ = ConsultaMusicaFavoritaAsync ( );
            Musicas = new ObservableCollection<Musica> ( ) {
                new Musica { id = 100000, name = " ",   artist = " " , cover = "remover" }
            };
            _  = ConsultaAsync ( );
            Albums = new ObservableCollection<Album> ( ) {
                new Album { id = 1, name = " ",      artist = "  t" , cover = "remover" }
            };


        }

        public async Task ConsultaAsync()
        {
                
            var teste = await Service.AlbumService.ConsultarAlbums();

                List<Album> listaDeAlbums = new List<Album>();
                XmlDocument retornoMusica = new XmlDocument();
                retornoMusica = (XmlDocument)JsonConvert.DeserializeXmlNode(Convert.ToString(teste), "Album");

                int id = 0;
                foreach (XmlNode retorno in retornoMusica.SelectNodes("Album"))
            {
                foreach (XmlNode musica in retorno.SelectNodes("Album"))
                {
                    int xid = 0;
                    string xname = string.Empty;
                    string xartist = string.Empty;
                    string xcover = string.Empty;

                    if (musica.SelectSingleNode("id") != null) xid = Convert.ToInt32(musica.SelectSingleNode("id").InnerText);
                    if (musica.SelectSingleNode("name") != null) xname = musica.SelectSingleNode("name").InnerText;
                    if (musica.SelectSingleNode("artist") != null) xartist = musica.SelectSingleNode("artist").InnerText;
                    if (musica.SelectSingleNode("cover") != null) xcover = musica.SelectSingleNode("cover").InnerText;


                    var contador = listaDeAlbums.Where(x => x.name == xname).ToList();
                    if (contador != null)
                    {
                        if (contador.Count() == 0)
                        {
                            if (id == xid)
                            {
                                Albums.Add(new Album()
                                {
                                    id = xid,
                                    name = xname,
                                    artist = xartist,
                                    cover = xcover

                                });
                            }
                        }
                    }


                }
            }
                if (Albums[0].cover == "remover")
                {
                    Albums.Remove(Albums[0]);
                }
            
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
                    if (musica.SelectSingleNode("id") != null) xid_album = Convert.ToInt32(musica.SelectSingleNode("id_album").InnerText);
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
                            if (id == xid_album)
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


                                View.MusicaAlbum.quantidade++;
                            }

                        }
                    }


                }
            }
            Musicas = new ObservableCollection<Musica> ( listaDeMusicas );
        }
        public ObservableCollection<Musica> Musicas { get; set; }

        public ObservableCollection<Album> Albums { get; set; }
    }
}
