using System;
using Bruttissimo.Domain.Logic;

namespace Bruttissimo.Tests.Integration
{
    public class TestTemplateServiceInConstructor
    {
        private readonly IEmailTemplateService templateService;

        public TestTemplateServiceInConstructor(IEmailTemplateService templateService)
        {
            if (templateService == null)
            {
                throw new ArgumentNullException("templateService");
            }
            this.templateService = templateService;
        }
    }
}
