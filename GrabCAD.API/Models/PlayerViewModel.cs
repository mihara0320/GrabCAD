using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GrabCAD.API.Models
{
    public class PlayerViewModel
    {
        [Required(ErrorMessage = "ConnectionId is required")]
        public string ConnectionId { get; set; }
    }
}
