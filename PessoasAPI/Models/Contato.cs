namespace PessoasAPI.Models
{
    public class Contato
    {
        public Pessoa PessoaContato { get; set; }
        public int ContatoId { get; set; }
        public int PessoaId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public int Idade { get; set; }
        public long Number { get; set; }
        public long Phone { get; set; }
    }
}
