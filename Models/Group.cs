using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace EfRelations.Models
{
    [Table("Grupos")]
    public class Group
    {
        public Group(string nombre)
        {            
            this.Nombre = nombre;
        }
        public int Id { get; set; }
        public string Nombre { get; set; }
        public List<UserGroup> UsuarioGrupo { get; set; }
    }
}