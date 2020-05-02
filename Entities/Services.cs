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
    [Table("Services")]
    public class Services : MyEntityBase
    {
        [DisplayName("Faaliyet Alanı"), Required(ErrorMessage = "{0} alanı boş geçilemez."), StringLength(50, ErrorMessage = "{0} alanı max. {1} karakter olmalıdır.")]
        public string Title { get; set; }

        [DisplayName("Faaliyet Açıklaması"), Required(ErrorMessage = "{0} alanı boş geçilemez.")]
        public string Description { get; set; }

        [DisplayName("Resim")]
        public string ServicesImage { get; set; }

        public virtual List<ProjectsDone> ProjectsDones { get; set; }

        public Services()
        {
            ProjectsDones = new List<ProjectsDone>();
        }

    }
}
