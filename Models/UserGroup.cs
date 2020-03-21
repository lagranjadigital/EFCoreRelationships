using System.ComponentModel.DataAnnotations.Schema;

namespace EfRelations.Models
{
    [Table("UsuariosGrupos")]
    public class UserGroup
    {
        public int UserId { get; set; }
        public Group Grupo { get; set; }

        public int GroupId { get; set; }
        public User Usuario { get; set; }
    }
}