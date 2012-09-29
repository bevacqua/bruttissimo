using Bruttissimo.Domain.Entity.Email;

namespace Bruttissimo.Domain.Repository
{
    public interface IEmailRepository
    {
        void Send(EmailMessageModel model);
    }
}
