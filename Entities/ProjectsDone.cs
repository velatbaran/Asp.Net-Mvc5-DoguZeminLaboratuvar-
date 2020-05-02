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
    [Table("ProjectsDone")]
    public class ProjectsDone : MyEntityBase
    {
        [DisplayName("Başlık"), Required(ErrorMessage = "{0} alanı boş geçilemez."), StringLength(150, ErrorMessage = "{0} alanı max. {1} karakter olmalıdır.")]
        public string Title { get; set; }

        [DisplayName("İçerik"), Required(ErrorMessage = "{0} alanı boş geçilemez.")]
        public string Text { get; set; }

        [DisplayName("İş Veren"), Required(ErrorMessage = "{0} alanı boş geçilemez."), StringLength(250, ErrorMessage = "{0} alanı max. {1} karakter olmalıdır.")]
        public string GiveJob { get; set; }

        [DisplayName("Faaliyet Alanı")]
        public int ServicesId { get; set; }

        [DisplayName("Proje Tarihi"), Required(ErrorMessage = "{0} alanı boş geçilemez.")]
        public DateTime ProjectDate { get; set; }

        [DisplayName("Proje Url"), StringLength(250, ErrorMessage = "{0} alanı max. {1} karakter olmalıdır.")]
        public string ProjectUrl { get; set; }

        [DisplayName("Resim_1")]
        public string SlideImage1 { get; set; }

        [DisplayName("Resim_2")]
        public string SlideImage2 { get; set; }

        [DisplayName("Resim_3")]
        public string SlideImage3 { get; set; }

        public virtual Services Services { get; set; }
    }
}
