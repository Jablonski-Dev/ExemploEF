namespace ExemploEF.Models
{
    public class Produto
    {
        public Guid Produtoid { get; set; }
        public string Nome { get; set; }
        public int Estoque { get; set; }


        public Guid CategoriasId { get; set; }
        public Categoria? Categorias { get; set; }
    }
}
