using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace todo.Model
{
    public class Data
    {
        public int ID { get; set; }
        [Required]
        public string Title { get; set; }
        public string Text { get; set; }
        public List<Checklist> checklist { get; set; }
        public List<Label> label { get; set; }
        public bool IsPinned { get; set; }
    }
        
    public class Checklist
    {
        public int ID { get; set; }
        public string text { get; set; }
    }

    public class Label
    {
        public int ID { get; set; }
        public string text { get; set; }
    }
}