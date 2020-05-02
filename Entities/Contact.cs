using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    [Table("Contact")]
    public class Contact : MyEntityBase
    {
        [DisplayName("Adres"), Required(ErrorMessage = "{0} alanı boş geçilemez."),
    StringLength(250, ErrorMessage = "{0} alanı max. {1} karakter olmalıdır.")]
        public string Addresses { get; set; }

        [DisplayName("Telefoon_1"), Required(ErrorMessage = "{0} alanı boş geçilemez."),
    StringLength(50, ErrorMessage = "{0} alanı max. {1} karakter olmalıdır.")]
        public string Phone1 { get; set; }

        [DisplayName("Telefoon_2")]
        public string Phone2 { get; set; }

        [DisplayName("E-Mail"), Required(ErrorMessage = "{0} alanı boş geçilemez."),
    StringLength(50, ErrorMessage = "{0} alanı max. {1} karakter olmalıdır."), DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [DisplayName("Twitter"),StringLength(50, ErrorMessage = "{0} alanı max. {1} karakter olmalıdır.")]
        public string Twitter { get; set; }

        [DisplayName("Instagram"),StringLength(50, ErrorMessage = "{0} alanı max. {1} karakter olmalıdır.")]
        public string Instagram { get; set; }

        [DisplayName("Youtube"),StringLength(50, ErrorMessage = "{0} alanı max. {1} karakter olmalıdır.")]
        public string Youtube { get; set; }

        [DisplayName("Linkedin"),StringLength(50, ErrorMessage = "{0} alanı max. {1} karakter olmalıdır.")]
        public string Linkedin { get; set; }

    }
}
