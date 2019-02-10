using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RequestSystem.Models
{
    public class Note
    {
        //DateTime myDateTime = DateTime.Now;
        //public string CreateDate = myDateTime.ToString("yyyy-MM-dd HH:mm:ss.fff");

        [Display(Name = "Номер")]
        public int Id { get; set; }

        [Display(Name = "Название")]
        public string Title { get; set; }

        [Display(Name = "Описание")]
        public string Description { get; set; }

        [Display(Name = "Дата создания")]
        [DataType(DataType.DateTime)]
        public DateTime CreateDate { get; set; }
        //[DataType(DataType.Text)]
        //public string CreateDate { get; set; }

        [Display(Name = "Статус: ")]
        public string Status { get; set; }

        [Display(Name = "Комментарий")]
        public string History { get; set; }

        // [Display(Name = "Текущее время")]
        // //[DataType(DataType.DateTime)]
        // public DateTime History { get; set; }
    }
}