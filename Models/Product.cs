using System.ComponentModel.DataAnnotations;

namespace Eksamen.Models
{
    public class Product
    {
        //Attributes that are needed for the correct functioning of the system
        public int ProductId { get; set; }
        [RegularExpression(@"[a-zA-ZæøåÆØÅ. \-]{2,20}", ErrorMessage = "The Name must be letters and between 2 to 20 characters.")]
        [Display(Name = "Product name")]
        public string Name { get; set; } = string.Empty;
        public int ProducerID{ get; set; }
        //Attributes that are mandatory from Norwegian and/or EU law
        public decimal Energy{ get; set; }
        public decimal Fat{ get; set; }
        public decimal Saturates{ get; set; }
        public decimal Carbs{ get; set; }
        public decimal Sugars{ get; set; }
        public decimal Protein{ get; set; }
        public decimal Salt{ get; set; }
        //Attributes that are optional in Norwegian and/or EU law
        public decimal? MonoUns{ get; set; }
        public decimal? PolyUns{ get; set; }
        public decimal? Polyols{ get; set; }
        public decimal? Starch{ get; set; }
        public decimal? Fiber{ get; set; }
        //Additional vitamins and minerals that are optional by law
        public decimal? VitaminA{ get; set; }
        public decimal? VitaminD{ get; set; }
        public decimal? VitaminE{ get; set; }
        public decimal? VitaminK{ get; set; }
        public decimal? VitaminC{ get; set; }
        public decimal? Thiamine{ get; set; }
        public decimal? Riboflavin{ get; set; }
        public decimal? Niacin{ get; set; }
        public decimal? VitaminB6{ get; set; }
        public decimal? FolicAcid{ get; set; }
        public decimal? VitaminB12{ get; set; }
        public decimal? Biotin{ get; set; }
        public decimal? PantothenicAcid{ get; set; }
        public decimal? Potassium{ get; set; }
        public decimal? Chloride{ get; set; }
        public decimal? Calcium{ get; set; }
        public decimal? Phosphorus{ get; set; }
        public decimal? Magnesium{ get; set; }
        public decimal? Iron{ get; set; }
        public decimal? Zinc{ get; set; }
        public decimal? Copper{ get; set; }
        public decimal? Manganese{ get; set; }
        public decimal? Fluoride{ get; set; }
        public decimal? Selenium{ get; set; }
        public decimal? Chromium{ get; set; }
        public decimal? Molybdenum{ get; set; }
        public decimal? Iodine{ get; set; }
        public string? ImageUpload { get; set; }

     }
}