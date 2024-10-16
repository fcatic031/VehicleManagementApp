using System.ComponentModel.DataAnnotations.Schema;

namespace VehicleManagement.Models
{
    [Table("VehicleModel")]
    public class VehicleModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Abrv { get; set; }
        public int VehicleMakeId { get; set; }
        public virtual VehicleMake VehicleMake { get; set; }
    }
}
