using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QuizMgnt.Models
{
    public class Quiz
    {
        public int quiz_id { get; set; }
        [Required(ErrorMessage ="Quiz needs to have a title")]
        public string quiz_title { get; set; }
        [Required(ErrorMessage ="Cannt leave Category Empty")]
        public int cat_id { get; set; }
        [Required(ErrorMessage ="Cannot Leave marks empty!")]
        [Range(1,250,ErrorMessage ="Marks should be between 1-250")]
        public int total_marks { get; set; }
        public IEnumerable<SelectListItem> category { get; set; }
        public string cat_type { get; set; }
    }
}