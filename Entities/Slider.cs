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
    [Table("Slider")]
    public class Slider : MyEntityBase
    {
        [DisplayName("Başlık"), Required(ErrorMessage = "{0} alanı boş geçilemez."), StringLength(100, ErrorMessage = "{0} alanı max. {1} karakter olmalıdır.")]
        public string Title { get; set; }

        [DisplayName("İçerik"), Required(ErrorMessage = "{0} alanı boş geçilemez."), StringLength(250, ErrorMessage = "{0} alanı max. {1} karakter olmalıdır.")]
        public string Text { get; set; }

        [DisplayName("Resim")]
        public string SlideImage { get; set; }
    }
}
