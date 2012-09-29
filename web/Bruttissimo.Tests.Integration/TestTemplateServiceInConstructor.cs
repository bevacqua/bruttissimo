using System;
using Bruttissimo.Common.Guard;
using Bruttissimo.Domain.Logic;

namespace Bruttissimo.Tests.Integration
{
    public class TestTemplateServiceInConstructor
    {
        private readonly IEmailTemplateService templateService;

        public TestTemplateServiceInConstructor(IEmailTemplateService templateService)
        {
            Ensure.That(templateService, "templateService").IsNotNull();

            this.templateService = templateService;
        }
    }
}
