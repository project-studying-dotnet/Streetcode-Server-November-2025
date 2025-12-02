using System.ComponentModel.DataAnnotations;

namespace Streetcode.BLL.DTO.Payment
{
    public class PaymentDtoo
    {
        [Required]
        public long Amount { get; set; }

        public string? RedirectUrl { get; set; }
    }
}
