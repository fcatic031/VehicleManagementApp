﻿using System.ComponentModel.DataAnnotations.Schema;

namespace VehicleManagement.Models
{
    [Table("VehicleMake")]
    public class VehicleMake
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Abrv { get; set; }

        public virtual ICollection<VehicleModel> VehicleModels { get; set; } = new List<VehicleModel>();
    }
}
