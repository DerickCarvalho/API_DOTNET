using ApiBitzen.Data;
using Microsoft.EntityFrameworkCore;

namespace ApiBitzen.Usuarios
{
    public static class UsuariosEndpoints
    {
        public static void AddRotasUsuarios(this WebApplication app) {

            var rotasUsuarios = app.MapGroup("usuarios");

            rotasUsuarios.MapPost("",
            async (addUsuarioRequest request, AppDbContext context, CancellationToken ct) => {
                var jaExiste = await context.Usuarios.AnyAsync(usuario => usuario.Nome == request.Nome, ct);

                if (jaExiste) {
                    return Results.Conflict("Já existe!");
                }

                var novoUsuario = new Usuario(request.Nome);

                await context.Usuarios.AddAsync(novoUsuario, ct);

                await context.SaveChangesAsync(ct);

                return Results.Ok(novoUsuario);
            });

            // Retornar todos os usuários
            rotasUsuarios.MapGet("", async (AppDbContext context, CancellationToken ct) => {
                var usuarios = await context
                    .Usuarios
                    .Where(usuario => usuario.Ativo)
                    .ToListAsync(ct);
                return usuarios;
            });

            // Editar nome usuario
            rotasUsuarios.MapPut("{id}",
            async (Guid id, updateUsuarioRequest request, AppDbContext context, CancellationToken ct) => {
                var usuario = await context.Usuarios
                    .SingleOrDefaultAsync(usuario => usuario.Id == id, ct);
                
                if (usuario == null) {
                    return Results.NotFound("Usuário não encontrado!");
                }

                usuario.AtualizarNome(request.Nome);

                await context.SaveChangesAsync(ct);

                return Results.Ok(usuario);
            });

            // Deletar
            rotasUsuarios.MapDelete("{id}", 
            async (Guid id, AppDbContext context, CancellationToken ct) => {
                var usuario = await context
                .Usuarios
                .SingleOrDefaultAsync(usuario => usuario.Id == id, ct);

                if (usuario == null) {
                    return Results.NotFound("Usuário não encontrado!");
                }

                usuario.InavitarUsuario();
                await context.SaveChangesAsync(ct);

                return Results.Ok(usuario);
            });
        }
    }
}