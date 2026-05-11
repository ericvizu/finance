namespace Finance.Entities;

public class User
{
    public Guid Id { get ; set; } // PK - garantir sequencial - talvez usar NEWSEQUENTIALID() no sql
    public string Name {get; set; } // Nome
    public string Email {get; set; } // Email
    public string PasswordHash {get; set; } // Hash da senha
    public DateTime CreatedAt {get; set; } // Quando foi criado
    public DateTime? UpdatedAt {get; set; } // Ultima vez atualizado - pode nunca ter sido atualizado
}