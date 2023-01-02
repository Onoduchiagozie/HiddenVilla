using HiddenVilla_Client.Model;

namespace HiddenVilla_Client.Service.Repo
{
    public interface IStripePaymentService
    {
        public Task<SuccessModel> Checkout(StripePayment payment);  
    }
}
