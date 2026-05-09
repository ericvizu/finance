namespace Finance.Entities;

public class User
{
    private string id; // PK
    private string name; // Nome
    private string email; // Email
    private string password_hash; // Hash da senha
    private DateTime created_at; // Quando foi criado
    private DateTime updated_at; // Ultima vez atualizado
}