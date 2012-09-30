using Bruttissimo.Common.Guard;
using Bruttissimo.Domain.Logic;
using Bruttissimo.Domain.Logic.Email.RazorEngine;

namespace Bruttissimo.Tests.Integration
{
    public class TestTemplateServiceInConstructor
    {
        private readonly IEmailTemplateService templateService;

        public TestTemplateServiceInConstructor(IEmailTemplateService templateService)
        {
            Ensure.That(() => templateService).IsNotNull();

            this.templateService = templateService;
        }
    }
}
