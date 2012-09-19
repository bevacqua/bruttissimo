using System;
using System.IO;
using Bruttissimo.Common;
using Bruttissimo.Domain.Entity;
using Bruttissimo.Domain.Logic;
using Bruttissimo.Tests.Mocking;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using log4net;

namespace Bruttissimo.Tests
{
    [TestClass]
    public class LinkCrawlerTests
    {
        private readonly ILog log = LogManager.GetLogger(typeof (LinkCrawlerTests));
        private ILinkCrawlerService linkCrawler;

        [TestInitialize]
        public void TestInit()
        {
            MockHelpers.SetupFakeWebClient();
            linkCrawler = MockHelpers.FakeLinkCrawler();
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public void CrawlHttpResource_WithNull_ThrowsArgumentNullException()
        {
            // Act
            linkCrawler.CrawlHttpResource(null);
        }

        private readonly Uri UriSensacional = new Uri("test://elsensacional.infonews.com/nota/7150-video-increible-gordito-alzado-le-da-murra-a-la-novia-dentro-de-una-pileta/");
        private readonly Uri UriInfobae = new Uri("test://www.infobae.com/notas/635464-Roger-Waters-ahora-aseguro-que-fue-tergiversado-respecto-del-tema-Malvinas.html");
        private readonly Uri UriMinuto = new Uri("test://www.minutouno.com.ar/minutouno/nota/161280-malvinas-cfk-reitero-la-idea-de-ampliar-vuelos/");
        private readonly Uri UriCronica = new Uri("test://www.cronica.com.ar/diario/2012/03/05/22140-matan-a-delincuente-en-feroz-tiroteo.html");
        private readonly Uri UriTierra = new Uri("test://www.tierradeperiodistas.com/index.php?op=noticia&id=67484");
        private readonly Uri UriYahoo = new Uri("test://es.noticias.yahoo.com/indignacion-en-suiza-tras-la-maternidad-una-jubilada-194759466.html");
        private readonly Uri UriMsn = new Uri("test://noticias.es.msn.com/entorno/insolito/la-muneca-que-balbucea-palabrotas");
        private readonly Uri UriNacion = new Uri("test://www.lanacion.com.ar/1454024-waters-nego-haber-afirmado-categoricamente-que-las-malvinas-son-argentinas");
        private readonly Uri UriClarin = new Uri("test://www.clarin.com/ciudades/Alerta-tormentas-Buenos-Aires-provincias_0_658134328.html");
        private readonly Uri UriRosario = new Uri("test://www.rosario3.com/noticias/policiales/noticias.aspx?idNot=107910&Casi-una-fija:-con-lluvia,-accidente-en-Circunvalaci%C3%B3n");
        private readonly Uri UriVeintiseis = new Uri("test://www.26noticias.com.ar/los-docentes-bonaerenses-ratifican-el-paro-y-no-habra-clases-martes-y-miercoles-148162.html");
        private readonly Uri UriVocero = new Uri("test://www.vocero.com/insolitas/mujer-de-95-sale-de-ataud-seis-dias-despues-de-haber-muerto");
        private readonly Uri UriRazon = new Uri("test://www.la-razon.com/nacional/Delegados-Bolivia-EEUU-cooperacion-comercial_0_1571842855.html");
        private readonly Uri UriBiochile = new Uri("test://www.biobiochile.cl/2011/11/08/mujer-enloquece-luego-de-que-amiga-la-eliminara-de-facebook-e-incendia-su-casa.shtml");

        private readonly Uri UriImageProgrammers = new Uri("test://cdn.sstatic.net/programmers/img/logo.png?v=123");
        private readonly Uri UriImageFacebook = new Uri("test://a8.sphotos.ak.fbcdn.net/hphotos-ak-ash4/424073_3445312773582_1293528893_33315385_487270322_n.jpg");
        private readonly Uri UriImageCodingHorror = new Uri("test://www.codinghorror.com/blog/images/coding-horror-official-logo-small.png");
        private readonly Uri UriImageVeintiseis = new Uri("test://www.26noticias.com.ar/adjuntos/imagen/portada/180929.jpg");

        #region News Endpoint Tests

        [TestMethod]
        public void CrawlHttpResource_WithUriSensacional_ReturnsLinkEntity()
        {
            CrawlHttpResource_WithUri_ReturnsLinkEntity(UriSensacional);
        }

        [TestMethod]
        public void CrawlHttpResource_WithUriSensacional_ReturnsLinkEntityWithTitle()
        {
            CrawlHttpResource_WithUri_ReturnsLinkEntityWithTitle(UriSensacional);
        }

        [TestMethod]
        public void CrawlHttpResource_WithUriSensacional_ReturnsLinkEntityWithDescription()
        {
            CrawlHttpResource_WithUri_ReturnsLinkEntityWithDescription(UriSensacional);
        }

        [TestMethod]
        public void CrawlHttpResource_WithUriSensacional_ReturnsLinkEntityWithThumbnail()
        {
            CrawlHttpResource_WithUri_ReturnsLinkEntityWithThumbnail(UriSensacional);
        }

        [TestMethod]
        public void CrawlHttpResource_WithUriSensacional_ReturnsEmptyImage()
        {
            CrawlHttpResource_WithUriHtml_ReturnsEmptyImage(UriSensacional);
        }


        [TestMethod]
        public void CrawlHttpResource_WithUriInfobae_ReturnsLinkEntity()
        {
            CrawlHttpResource_WithUri_ReturnsLinkEntity(UriInfobae);
        }

        [TestMethod]
        public void CrawlHttpResource_WithUriInfobae_ReturnsLinkEntityWithTitle()
        {
            CrawlHttpResource_WithUri_ReturnsLinkEntityWithTitle(UriInfobae);
        }

        [TestMethod]
        public void CrawlHttpResource_WithUriInfobae_ReturnsLinkEntityWithDescription()
        {
            CrawlHttpResource_WithUri_ReturnsLinkEntityWithDescription(UriInfobae);
        }

        [TestMethod]
        public void CrawlHttpResource_WithUriInfobae_ReturnsLinkEntityWithThumbnail()
        {
            CrawlHttpResource_WithUri_ReturnsLinkEntityWithThumbnail(UriInfobae);
        }

        [TestMethod]
        public void CrawlHttpResource_WithUriInfobae_ReturnsEmptyImage()
        {
            CrawlHttpResource_WithUriHtml_ReturnsEmptyImage(UriInfobae);
        }

        [TestMethod]
        public void CrawlHttpResource_WithUriMinuto_ReturnsLinkEntity()
        {
            CrawlHttpResource_WithUri_ReturnsLinkEntity(UriMinuto);
        }

        [TestMethod]
        public void CrawlHttpResource_WithUriMinuto_ReturnsLinkEntityWithTitle()
        {
            CrawlHttpResource_WithUri_ReturnsLinkEntityWithTitle(UriMinuto);
        }

        [TestMethod]
        public void CrawlHttpResource_WithUriMinuto_ReturnsLinkEntityWithDescription()
        {
            CrawlHttpResource_WithUri_ReturnsLinkEntityWithDescription(UriMinuto);
        }

        // [TestMethod]
        public void CrawlHttpResource_WithUriMinuto_ReturnsLinkEntityWithThumbnail()
        {
            CrawlHttpResource_WithUri_ReturnsLinkEntityWithThumbnail(UriMinuto);
        }

        [TestMethod]
        public void CrawlHttpResource_WithUriMinuto_ReturnsEmptyImage()
        {
            CrawlHttpResource_WithUriHtml_ReturnsEmptyImage(UriMinuto);
        }

        [TestMethod]
        public void CrawlHttpResource_WithUriCronica_ReturnsLinkEntity()
        {
            CrawlHttpResource_WithUri_ReturnsLinkEntity(UriCronica);
        }

        [TestMethod]
        public void CrawlHttpResource_WithUriCronica_ReturnsLinkEntityWithTitle()
        {
            CrawlHttpResource_WithUri_ReturnsLinkEntityWithTitle(UriCronica);
        }

        [TestMethod]
        public void CrawlHttpResource_WithUriCronica_ReturnsLinkEntityWithDescription()
        {
            CrawlHttpResource_WithUri_ReturnsLinkEntityWithDescription(UriCronica);
        }

        // [TestMethod]
        public void CrawlHttpResource_WithUriCronica_ReturnsLinkEntityWithThumbnail()
        {
            CrawlHttpResource_WithUri_ReturnsLinkEntityWithThumbnail(UriCronica);
        }

        [TestMethod]
        public void CrawlHttpResource_WithUriCronica_ReturnsEmptyImage()
        {
            CrawlHttpResource_WithUriHtml_ReturnsEmptyImage(UriCronica);
        }

        [TestMethod]
        public void CrawlHttpResource_WithUriTierra_ReturnsLinkEntity()
        {
            CrawlHttpResource_WithUri_ReturnsLinkEntity(UriTierra);
        }

        [TestMethod]
        public void CrawlHttpResource_WithUriTierra_ReturnsLinkEntityWithTitle()
        {
            CrawlHttpResource_WithUri_ReturnsLinkEntityWithTitle(UriTierra);
        }

        [TestMethod]
        public void CrawlHttpResource_WithUriTierra_ReturnsLinkEntityWithDescription()
        {
            CrawlHttpResource_WithUri_ReturnsLinkEntityWithDescription(UriTierra);
        }

        // [TestMethod]
        public void CrawlHttpResource_WithUriTierra_ReturnsLinkEntityWithThumbnail()
        {
            CrawlHttpResource_WithUri_ReturnsLinkEntityWithThumbnail(UriTierra);
        }

        [TestMethod]
        public void CrawlHttpResource_WithUriTierra_ReturnsEmptyImage()
        {
            CrawlHttpResource_WithUriHtml_ReturnsEmptyImage(UriTierra);
        }

        [TestMethod]
        public void CrawlHttpResource_WithUriYahoo_ReturnsLinkEntity()
        {
            CrawlHttpResource_WithUri_ReturnsLinkEntity(UriYahoo);
        }

        [TestMethod]
        public void CrawlHttpResource_WithUriYahoo_ReturnsLinkEntityWithTitle()
        {
            CrawlHttpResource_WithUri_ReturnsLinkEntityWithTitle(UriYahoo);
        }

        [TestMethod]
        public void CrawlHttpResource_WithUriYahoo_ReturnsLinkEntityWithDescription()
        {
            CrawlHttpResource_WithUri_ReturnsLinkEntityWithDescription(UriYahoo);
        }

        [TestMethod]
        public void CrawlHttpResource_WithUriYahoo_ReturnsLinkEntityWithThumbnail()
        {
            CrawlHttpResource_WithUri_ReturnsLinkEntityWithThumbnail(UriYahoo);
        }

        [TestMethod]
        public void CrawlHttpResource_WithUriYahoo_ReturnsEmptyImage()
        {
            CrawlHttpResource_WithUriHtml_ReturnsEmptyImage(UriYahoo);
        }

        [TestMethod]
        public void CrawlHttpResource_WithUriMsn_ReturnsLinkEntity()
        {
            CrawlHttpResource_WithUri_ReturnsLinkEntity(UriMsn);
        }

        [TestMethod]
        public void CrawlHttpResource_WithUriMsn_ReturnsLinkEntityWithTitle()
        {
            CrawlHttpResource_WithUri_ReturnsLinkEntityWithTitle(UriMsn);
        }

        [TestMethod]
        public void CrawlHttpResource_WithUriMsn_ReturnsLinkEntityWithDescription()
        {
            CrawlHttpResource_WithUri_ReturnsLinkEntityWithDescription(UriMsn);
        }

        [TestMethod]
        public void CrawlHttpResource_WithUriMsn_ReturnsLinkEntityWithThumbnail()
        {
            CrawlHttpResource_WithUri_ReturnsLinkEntityWithThumbnail(UriMsn);
        }

        [TestMethod]
        public void CrawlHttpResource_WithUriMsn_ReturnsEmptyImage()
        {
            CrawlHttpResource_WithUriHtml_ReturnsEmptyImage(UriMsn);
        }

        [TestMethod]
        public void CrawlHttpResource_WithUriNacion_ReturnsLinkEntity()
        {
            CrawlHttpResource_WithUri_ReturnsLinkEntity(UriNacion);
        }

        [TestMethod]
        public void CrawlHttpResource_WithUriNacion_ReturnsLinkEntityWithTitle()
        {
            CrawlHttpResource_WithUri_ReturnsLinkEntityWithTitle(UriNacion);
        }

        [TestMethod]
        public void CrawlHttpResource_WithUriNacion_ReturnsLinkEntityWithDescription()
        {
            CrawlHttpResource_WithUri_ReturnsLinkEntityWithDescription(UriNacion);
        }

        [TestMethod]
        public void CrawlHttpResource_WithUriNacion_ReturnsLinkEntityWithThumbnail()
        {
            CrawlHttpResource_WithUri_ReturnsLinkEntityWithThumbnail(UriNacion);
        }

        [TestMethod]
        public void CrawlHttpResource_WithUriNacion_ReturnsEmptyImage()
        {
            CrawlHttpResource_WithUriHtml_ReturnsEmptyImage(UriNacion);
        }

        [TestMethod]
        public void CrawlHttpResource_WithUriClarin_ReturnsLinkEntity()
        {
            CrawlHttpResource_WithUri_ReturnsLinkEntity(UriClarin);
        }

        [TestMethod]
        public void CrawlHttpResource_WithUriClarin_ReturnsLinkEntityWithTitle()
        {
            CrawlHttpResource_WithUri_ReturnsLinkEntityWithTitle(UriClarin);
        }

        [TestMethod]
        public void CrawlHttpResource_WithUriClarin_ReturnsLinkEntityWithDescription()
        {
            CrawlHttpResource_WithUri_ReturnsLinkEntityWithDescription(UriClarin);
        }

        [TestMethod]
        public void CrawlHttpResource_WithUriClarin_ReturnsLinkEntityWithThumbnail()
        {
            CrawlHttpResource_WithUri_ReturnsLinkEntityWithThumbnail(UriClarin);
        }

        [TestMethod]
        public void CrawlHttpResource_WithUriClarin_ReturnsEmptyImage()
        {
            CrawlHttpResource_WithUriHtml_ReturnsEmptyImage(UriClarin);
        }

        [TestMethod]
        public void CrawlHttpResource_WithUriRosario_ReturnsLinkEntity()
        {
            CrawlHttpResource_WithUri_ReturnsLinkEntity(UriRosario);
        }

        [TestMethod]
        public void CrawlHttpResource_WithUriRosario_ReturnsLinkEntityWithTitle()
        {
            CrawlHttpResource_WithUri_ReturnsLinkEntityWithTitle(UriRosario);
        }

        [TestMethod]
        public void CrawlHttpResource_WithUriRosario_ReturnsLinkEntityWithDescription()
        {
            CrawlHttpResource_WithUri_ReturnsLinkEntityWithDescription(UriRosario);
        }

        [TestMethod]
        public void CrawlHttpResource_WithUriRosario_ReturnsLinkEntityWithThumbnail()
        {
            CrawlHttpResource_WithUri_ReturnsLinkEntityWithThumbnail(UriRosario);
        }

        [TestMethod]
        public void CrawlHttpResource_WithUriRosario_ReturnsEmptyImage()
        {
            CrawlHttpResource_WithUriHtml_ReturnsEmptyImage(UriRosario);
        }

        [TestMethod]
        public void CrawlHttpResource_WithUriVeintiseis_ReturnsLinkEntity()
        {
            CrawlHttpResource_WithUri_ReturnsLinkEntity(UriVeintiseis);
        }

        [TestMethod]
        public void CrawlHttpResource_WithUriVeintiseis_ReturnsLinkEntityWithTitle()
        {
            CrawlHttpResource_WithUri_ReturnsLinkEntityWithTitle(UriVeintiseis);
        }

        [TestMethod]
        public void CrawlHttpResource_WithUriVeintiseis_ReturnsLinkEntityWithDescription()
        {
            CrawlHttpResource_WithUri_ReturnsLinkEntityWithDescription(UriVeintiseis);
        }

        // [TestMethod]
        public void CrawlHttpResource_WithUriVeintiseis_ReturnsLinkEntityWithThumbnail()
        {
            CrawlHttpResource_WithUri_ReturnsLinkEntityWithThumbnail(UriVeintiseis);
        }

        [TestMethod]
        public void CrawlHttpResource_WithUriVeintiseis_ReturnsEmptyImage()
        {
            CrawlHttpResource_WithUriHtml_ReturnsEmptyImage(UriVeintiseis);
        }

        [TestMethod]
        public void CrawlHttpResource_WithUriVocero_ReturnsLinkEntity()
        {
            CrawlHttpResource_WithUri_ReturnsLinkEntity(UriVocero);
        }

        [TestMethod]
        public void CrawlHttpResource_WithUriVocero_ReturnsLinkEntityWithTitle()
        {
            CrawlHttpResource_WithUri_ReturnsLinkEntityWithTitle(UriVocero);
        }

        [TestMethod]
        public void CrawlHttpResource_WithUriVocero_ReturnsLinkEntityWithDescription()
        {
            CrawlHttpResource_WithUri_ReturnsLinkEntityWithDescription(UriVocero);
        }

        [TestMethod]
        public void CrawlHttpResource_WithUriVocero_ReturnsLinkEntityWithThumbnail()
        {
            CrawlHttpResource_WithUri_ReturnsLinkEntityWithThumbnail(UriVocero);
        }

        [TestMethod]
        public void CrawlHttpResource_WithUriVocero_ReturnsEmptyImage()
        {
            CrawlHttpResource_WithUriHtml_ReturnsEmptyImage(UriVocero);
        }

        [TestMethod]
        public void CrawlHttpResource_WithUriRazon_ReturnsLinkEntity()
        {
            CrawlHttpResource_WithUri_ReturnsLinkEntity(UriRazon);
        }

        [TestMethod]
        public void CrawlHttpResource_WithUriRazon_ReturnsLinkEntityWithTitle()
        {
            CrawlHttpResource_WithUri_ReturnsLinkEntityWithTitle(UriRazon);
        }

        [TestMethod]
        public void CrawlHttpResource_WithUriRazon_ReturnsLinkEntityWithDescription()
        {
            CrawlHttpResource_WithUri_ReturnsLinkEntityWithDescription(UriRazon);
        }

        [TestMethod]
        public void CrawlHttpResource_WithUriRazon_ReturnsLinkEntityWithThumbnail()
        {
            CrawlHttpResource_WithUri_ReturnsLinkEntityWithThumbnail(UriRazon);
        }

        [TestMethod]
        public void CrawlHttpResource_WithUriRazon_ReturnsEmptyImage()
        {
            CrawlHttpResource_WithUriHtml_ReturnsEmptyImage(UriRazon);
        }

        [TestMethod]
        public void CrawlHttpResource_WithUriBiochile_ReturnsLinkEntity()
        {
            CrawlHttpResource_WithUri_ReturnsLinkEntity(UriBiochile);
        }

        [TestMethod]
        public void CrawlHttpResource_WithUriBiochile_ReturnsLinkEntityWithTitle()
        {
            CrawlHttpResource_WithUri_ReturnsLinkEntityWithTitle(UriBiochile);
        }

        [TestMethod]
        public void CrawlHttpResource_WithUriBiochile_ReturnsLinkEntityWithDescription()
        {
            CrawlHttpResource_WithUri_ReturnsLinkEntityWithDescription(UriBiochile);
        }

        [TestMethod]
        public void CrawlHttpResource_WithUriBiochile_ReturnsLinkEntityWithThumbnail()
        {
            CrawlHttpResource_WithUri_ReturnsLinkEntityWithThumbnail(UriBiochile);
        }

        [TestMethod]
        public void CrawlHttpResource_WithUriBiochile_ReturnsEmptyImage()
        {
            CrawlHttpResource_WithUriHtml_ReturnsEmptyImage(UriBiochile);
        }

        #endregion

        #region Picture Endpoint Tests

        [TestMethod]
        public void CrawlHttpResource_WithUriImageProgrammers_ReturnsImage()
        {
            CrawlHttpResource_WithUriImage_ReturnsImage(UriImageProgrammers);
        }

        [TestMethod]
        public void CrawlHttpResource_WithUriImageFacebook_ReturnsImage()
        {
            CrawlHttpResource_WithUriImage_ReturnsImage(UriImageFacebook);
        }

        [TestMethod]
        public void CrawlHttpResource_WithUriImageCodingHorror_ReturnsImage()
        {
            CrawlHttpResource_WithUriImage_ReturnsImage(UriImageCodingHorror);
        }

        [TestMethod]
        public void CrawlHttpResource_WithUriImageVeintiseis_ReturnsImage()
        {
            CrawlHttpResource_WithUriImage_ReturnsImage(UriImageVeintiseis);
        }

        #endregion

        public void CrawlHttpResource_WithUri_ReturnsLinkEntity(Uri uri)
        {
            // Arrange
            Stream fakeResponseStream = MockHelpers.GetResourceStream(uri);
            TestWebRequestCreate.CreateTestRequest(fakeResponseStream);

            // Act
            Link crawled = linkCrawler.CrawlHttpResource(uri);

            // Assert
            Assert.IsNotNull(crawled);
        }

        public void CrawlHttpResource_WithUri_ReturnsLinkEntityWithTitle(Uri uri)
        {
            // Arrange
            Stream fakeResponseStream = MockHelpers.GetResourceStream(uri);
            TestWebRequestCreate.CreateTestRequest(fakeResponseStream);

            // Act
            Link crawled = linkCrawler.CrawlHttpResource(uri);

            // Assert
            Assert.IsNotNull(crawled.Title);
        }

        public void CrawlHttpResource_WithUri_ReturnsLinkEntityWithDescription(Uri uri)
        {
            // Arrange
            Stream fakeResponseStream = MockHelpers.GetResourceStream(uri);
            TestWebRequestCreate.CreateTestRequest(fakeResponseStream);

            // Act
            Link crawled = linkCrawler.CrawlHttpResource(uri);

            // Assert
            Assert.IsNotNull(crawled.Description);
        }

        public void CrawlHttpResource_WithUri_ReturnsLinkEntityWithThumbnail(Uri uri)
        {
            // Arrange
            Stream fakeResponseStream = MockHelpers.GetResourceStream(uri);
            TestWebRequestCreate.CreateTestRequest(fakeResponseStream);

            // Act
            Link crawled = linkCrawler.CrawlHttpResource(uri);

            // Assert
            Assert.IsNotNull(crawled.Picture);

            log.Debug(string.Format("{0}: {1}", uri.AbsoluteUri, crawled.Picture));
        }

        public void CrawlHttpResource_WithUriHtml_ReturnsEmptyImage(Uri uri)
        {
            // Arrange
            Stream fakeResponseStream = MockHelpers.GetResourceStream(uri);
            TestWebRequestCreate.CreateTestRequest(fakeResponseStream);

            // Act
            Link link = linkCrawler.CrawlHttpResource(uri);

            // Assert
            Assert.IsTrue(VerifyLinkIsHtml(link));
        }

        public void CrawlHttpResource_WithUriImage_ReturnsImage(Uri uri)
        {
            // Arrange
            Stream fakeResponseStream = MockHelpers.GetResourceStream(uri);
            TestWebRequestCreate.CreateTestRequest(fakeResponseStream);

            // Act
            Link link = linkCrawler.CrawlHttpResource(uri);

            // Assert
            Assert.IsFalse(VerifyLinkIsHtml(link));
        }

        private bool VerifyLinkIsHtml(Link link)
        {
            return !link.Title.NullOrEmpty() || !link.Description.NullOrEmpty() || !link.Picture.NullOrEmpty();
        }
    }
}
