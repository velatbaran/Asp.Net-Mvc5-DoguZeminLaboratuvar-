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
    [Table("About")]
    public class About : MyEntityBase
    {
        [DisplayName("Başlık"), Required(ErrorMessage ="{0} alanı boş geçilemez.") , StringLength(50,ErrorMessage ="{0} alanı max. {1} karakter olmalıdır.")]
        public string Title { get; set; }

        [DisplayName("İçerik"), Required(ErrorMessage = "{0} alanı boş geçilemez.")]
        public string Text { get; set; }

        [DisplayName("Resim")]
        public string AboutImage { get; set; }
    }
}
