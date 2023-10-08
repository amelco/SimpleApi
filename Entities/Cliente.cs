namespace SimpleApi.Entities
{
    public class Cliente : EntityBase
    {
        public string? Nome { get; set; }
        public string? Email { get; set; }
        public string? Usuario { get; set; }
        public int Senha { get; set; }
    }
}
