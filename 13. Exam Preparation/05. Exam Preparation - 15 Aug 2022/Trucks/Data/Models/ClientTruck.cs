using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trucks.Data.Models
{
    public class ClientTruck
    {
        [Required]
        [ForeignKey(nameof(Client))]
        public int ClientId { get; set; }

        [Required]

        public virtual Client Client { get; set; } = null!;

        [Required]
        [ForeignKey(nameof(Truck))]
        public int TruckId { get; set; }

        [Required]

        public virtual Truck Truck { get; set; } = null!;
    }
}
