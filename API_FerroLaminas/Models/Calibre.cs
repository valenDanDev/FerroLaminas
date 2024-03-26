namespace API_FerroLaminas.Models
{
    public class Calibre
    {
        public int Id { get; set; }
        public decimal MedidaCalibre { get; set; }

        // Clave foránea para la relación con Material
        public int MaterialId { get; set; }
        public Material Material { get; set; }
    }
}
