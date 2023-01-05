namespace PessoasAPI.Models
{
    public class Contato
    {

        public int ContatoId { get; set; }
        public int PessoaId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public int Idade { get; set; }
        public string Telefone { get; set; }
    }
}
