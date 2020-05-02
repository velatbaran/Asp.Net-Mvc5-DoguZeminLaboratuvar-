using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.ValueObjects
{
    public class RegisterViewModel
    {
        [DisplayName("Ad"), Required(ErrorMessage = "{0} alanı boş geçilemez."), StringLength(50, ErrorMessage = "{0} alanı max. {1} karakter olmalıdır.")]
        public string Name { get; set; }

        [DisplayName("Soyad"), Required(ErrorMessage = "{0} alanı boş geçilemez."), StringLength(50, ErrorMessage = "{0} alanı max. {1} karakter olmalıdır.")]
        public string Surname { get; set; }

        [DisplayName("Kullanıcı Adı"), Required(ErrorMessage = "{0} alanı boş geçilemez."), StringLength(50, ErrorMessage = "{0} alanı max. {1} karakter olmalıdır.")]
        public string Username { get; set; }

        [DisplayName("Unvan"), Required(ErrorMessage = "{0} alanı boş geçilemez."), StringLength(100, ErrorMessage = "{0} alanı max. {1} karakter olmalıdır.")]
        public string Degree { get; set; }

        [DisplayName("Görev"), Required(ErrorMessage = "{0} alanı boş geçilemez."), StringLength(100, ErrorMessage = "{0} alanı max. {1} karakter olmalıdır.")]
        public string Task { get; set; }

        [DisplayName("E-Mail"), Required(ErrorMessage = "{0} alanı boş geçilemez."),
            StringLength(50, ErrorMessage = "{0} alanı max. {1} karakter olmalıdır."), DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [DisplayName("Twitter"), StringLength(50, ErrorMessage = "{0} alanı max. {1} karakter olmalıdır.")]
        public string Twitter { get; set; }

        [DisplayName("Instagram"), StringLength(50, ErrorMessage = "{0} alanı max. {1} karakter olmalıdır.")]
        public string Instagram { get; set; }

        [DisplayName("Facebook"), StringLength(50, ErrorMessage = "{0} alanı max. {1} karakter olmalıdır.")]
        public string Facebook { get; set; }

        [DisplayName("Linkedin"), StringLength(50, ErrorMessage = "{0} alanı max. {1} karakter olmalıdır.")]
        public string Linkedin { get; set; }

        [DisplayName("Şifre"), Required(ErrorMessage = "{0} alanı boş geçilemez."),
            StringLength(100, ErrorMessage = "{0} alanı max. {1} karakter olmalıdır."), DataType(DataType.Password)]
        public string Password { get; set; }

    }
}
