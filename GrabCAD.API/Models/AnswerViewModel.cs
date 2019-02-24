﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GrabCAD.API.ViewModels
{
    public class AnswerViewModel
    {
        [Required(ErrorMessage = "ConnectionId is required")]
        public string ConnectionId { get; set; }

        [Required(ErrorMessage = "Answer is required")]
        public int Answer { get; set; }
    }
}