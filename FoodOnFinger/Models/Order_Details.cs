namespace FoodOnFinger.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Order_Details
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int OrderID { get; set; }

        public int? CuisineID { get; set; }

        public string Address { get; set; }

        public int? ProductID { get; set; }

        [Column(TypeName = "date")]
        public DateTime? Date { get; set; }

        [StringLength(50)]
        public string Contact { get; set; }

        [Column(TypeName = "money")]
        public decimal? Total { get; set; }

        public virtual Cuisine Cuisine { get; set; }

        public virtual Product Product { get; set; }
    }
}
