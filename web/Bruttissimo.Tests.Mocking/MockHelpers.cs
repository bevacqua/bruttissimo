using System;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Reflection;
using System.Web;
using Bruttissimo.Common.Extensions;
using Bruttissimo.Common.Guard;
using Bruttissimo.Common.Utility;
using Bruttissimo.Domain.Entity.Entities;
using Bruttissimo.Domain.Logic.Email.RazorEngine;
using Bruttissimo.Domain.Logic.Email.Template;
using Bruttissimo.Domain.Logic.MiniMembership;
using Bruttissimo.Domain.Logic.Repository;
using Bruttissimo.Domain.Logic.Service;
using Bruttissimo.Domain.Repository;
using Bruttissimo.Domain.Service;
using Bruttissimo.Extensions.RazorEngine;
using Moq;
using RazorEngine.Configuration;

namespace Bruttissimo.Tests.Mocking
{
    public static class MockHelpers
    {
        #region Repository

        public static ILinkRepository FakeLinkRepository()
        {
            Mock<ILinkRepository> mock = new Mock<ILinkRepository>();
            ILinkRepository linkRepository = mock.Object;
            return linkRepository;
        }

        public static IPictureRepository FakeImageRepository()
        {
            Mock<IPictureRepository> mock = new Mock<IPictureRepository>();
            IPictureRepository pictureRepository = mock.Object;
            return pictureRepository;
        }

        public static PictureStorageRepository FakeImageStorageRepository()
        {
            HttpContextBase httpContext = MvcMockHelpers.FakeHttpContext();
            return new PictureStorageRepository(httpContext);
        }

        public static Mock<IUserRepository> MockUserRepository()
        {
            Mock<IUserRepository> mock = new Mock<IUserRepository>();

            mock.Setup(x => x
                                .GetByEmail(It.IsAny<string>()))
                .Returns(Mock.Of<User>());

            return mock;
        }

        #endregion

        #region Service

        public static ILinkCrawlerService FakeLinkCrawler()
        {
            HttpHelper httpHelper = new HttpHelper();
            return new LinkCrawlerService(httpHelper);
        }

        public static IPictureService FakeImageService()
        {
            IPictureRepository pictureRepository = FakeImageRepository();
            IPictureStorageRepository imageStorageRepository = Mock.Of<IPictureStorageRepository>();
            FileSystemHelper fsHelper = new FileSystemHelper();
            IPictureService pictureService = new PictureService(pictureRepository, imageStorageRepository, fsHelper);
            return pictureService;
        }

        public static ILinkService FakeLinkService()
        {
            ILinkRepository linkRepository = FakeLinkRepository();
            ILinkCrawlerService linkCrawler = FakeLinkCrawler();
            HttpHelper httpHelper = new HttpHelper();
            return new LinkService(linkRepository, linkCrawler, httpHelper);
        }

        public static IUserService FakeUserService(IUserRepository userRepository = null)
        {
            return new UserService(userRepository ?? MockUserRepository().Object, FakeEmailService());
        }

        public static IEmailTemplateService GetRazorTemplateService(Type type)
        {
            TemplateServiceConfiguration configuration = new TemplateServiceConfiguration
            {
                Resolver = new EmbeddedTemplateResolver(type)
            };
            IEmailTemplateService service = new EmailTemplateService(configuration);
            return service;
        }

        public static IEmailService FakeEmailService()
        {
            IEmailTemplateService templateService = GetRazorTemplateService(typeof (EmailTemplate));
            IEmailRepository emailRepository = FakeEmailRepository();
            return new EmailService(templateService, emailRepository);
        }

        private static IEmailRepository FakeEmailRepository()
        {
            SmtpClient client = FakeSmtpClient();
            return new EmailRepository(client);
        }

        private static SmtpClient FakeSmtpClient()
        {
            string domain = AppDomain.CurrentDomain.BaseDirectory;
            string path = Path.Combine(domain, "EmailPickup");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            SmtpClient client = new SmtpClient
            {
                PickupDirectoryLocation = path,
                DeliveryMethod = SmtpDeliveryMethod.SpecifiedPickupDirectory
            };
            return client;
        }

        #endregion

        public static MiniMembershipProvider FakeMiniMembership(IUserRepository userRepository = null)
        {
            MiniMembershipProvider miniMembership = new MiniMembershipProvider(userRepository);
            return miniMembership;
        }

        #region Utility

        /// <summary>
        /// Loads the fake response from the file system instead of the web, in order to decrease testing response times.
        /// </summary>
        public static string GetFakeResponse(Uri uri)
        {
            Ensure.That(() => uri).IsNotNull();

            using (Stream response = GetResourceStream(uri))
            {
                return response.ReadFully();
            }
        }

        /// <summary>
        /// Loads the fake response from the file system instead of the web, in order to decrease testing response times.
        /// </summary>
        public static Stream GetResourceStream(Uri uri)
        {
            Ensure.That(() => uri).IsNotNull();

            string resourceName = GetResourceNameFromUri(uri);
            Assembly executing = Assembly.GetCallingAssembly();
            Stream stream = executing.GetManifestResourceStream(resourceName);
            return stream;
        }

        /// <summary>
        /// Maps the test uri to a physical location on disk.
        /// </summary>
        public static string GetResourceNameFromUri(Uri uri)
        {
            string resource = uri.GetLeftPart(UriPartial.Path).Replace(uri.GetLeftPart(UriPartial.Scheme), string.Empty)
                .Replace("/", string.Empty)
                .Replace("?", string.Empty)
                .Replace(":", string.Empty);

            string path = string.Concat("Bruttissimo.Tests.ResourceStream.", resource);
            return path;
        }

        #endregion

        /// <summary>
        /// Initializes a web request factory that allows us to mock web responses.
        /// </summary>
        public static void SetupFakeWebClient()
        {
            WebRequest.RegisterPrefix("test", new TestWebRequestCreate());
        }
    }
}
