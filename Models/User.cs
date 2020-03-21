using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace EfRelations.Models
{
    [Table("Usuarios")]
    public class User
    {
        public User(string nombre)
        {            
            this.Nombre = nombre;

        }
        public int Id { get; set; }
        public string Nombre { get; set; }
        public List<UserGroup> UsuarioGrupo { get; set; }
    }
}