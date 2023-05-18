using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
namespace JmeterCLIDemo
{
    //DIstrict model for database
    [DataContract]
    [Serializable]
    public class District
    {
        [Key]
        [DataMember(Name ="ID")]
        public int ID { get; set; }

        [Required]
        [DataMember(Name ="STCode")]
        public int STCode { get; set; } = 24;

        [Required]
        [MaxLength(100)]
        [DataMember(Name ="StateName")]
        public string StateName { get; set; }

        [Required]
        [Range(400,500,ErrorMessage ="Negative values of district code are not allowed")]
        [DataMember(Name = "DTCode")]
        public int DTCode { get; set; }

        [Required]
        [MaxLength(100)]
        [DataMember(Name = "DistrictName")]
        public string DistrictName { get; set; }

        [Required]
        [DataMember(Name = "SDTCode")]
        [Range(3500, 4000, ErrorMessage ="Negative values of subdistrict code are not allowed")]
        public int SDTCode { get; set; }

        [Required]
        [MaxLength(100)]
        [DataMember(Name = "SubDistrictName")]
        public string SubDistrictName { get; set; }

        [Required]
        [Range(800000, 850000, ErrorMessage = "Negative values of town code are not allowed")]
        [DataMember(Name = "TownCode")]
        public int TownCode { get; set; }

        [Required]
        [DataMember(Name = "AreaName")]
        [MaxLength(100)]
        public string AreaName { get; set;}
    }
}
