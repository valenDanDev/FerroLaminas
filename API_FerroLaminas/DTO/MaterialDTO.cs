namespace API_FerroLaminas.DTO
{
    public class MaterialDTO
    {
        public int Id { get; set; }
        public string Tipo { get; set; }
        public decimal PrecioPorKilo { get; set; }
        public decimal StockKilos { get; set; }
        public string Descripcion { get; set; }
    }
}
