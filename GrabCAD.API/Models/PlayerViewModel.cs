using System.ComponentModel.DataAnnotations;

namespace GrabCAD.API.Models
{
    public class PlayerViewModel
    {
        [Required(ErrorMessage = "ConnectionId is required")]
        public string ConnectionId { get; set; }
    }
}
