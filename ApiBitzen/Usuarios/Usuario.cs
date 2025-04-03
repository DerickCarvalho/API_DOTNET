namespace ApiBitzen.Usuarios
{
    public class Usuario
    {
        public Guid Id {get; init;}
        public string Nome {get; private set;}
        public bool Ativo {get; private set;}

        public Usuario(string nome) {
            Nome = nome;
            Id = Guid.NewGuid();
            Ativo = true;
        }

        public void AtualizarNome(string nome) {
            Nome = nome;
        }

        public void InavitarUsuario() {
            Ativo = false;
        }
    }
}