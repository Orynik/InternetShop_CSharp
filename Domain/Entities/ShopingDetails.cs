using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class ShopingDetails
    {
        [Required(ErrorMessage = "Укажите свое имя")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Укажите свой адрес")]
        [Display(Name="Первый адрес")]
        public string line1 { get; set; }
        [Display(Name = "Второй адрес")]
        public string line2 { get; set; }
        [Display(Name = "Третий адрес")]
        public string line3 { get; set; }

        [Required(ErrorMessage = "Укажите вашу страну")]
        [Display(Name = "Страна")]
        public string Country { get; set; }
        [Required(ErrorMessage = "Укажите ваш город")]
        [Display(Name = "Город")]
        public string City { get; set; }

        public bool WrapGift{ get; set; }
    }
}
