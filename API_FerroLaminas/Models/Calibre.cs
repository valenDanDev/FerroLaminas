namespace API_FerroLaminas.Models
{
    public class Calibre
    {
        public int Id { get; set; }
        public decimal MedidaCalibre { get; set; }

        // FK
        public int MaterialId { get; set; }
        public Material Material { get; set; }
    }
}
