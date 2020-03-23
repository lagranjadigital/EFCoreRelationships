using System;
using System.Collections.Generic;
using System.Linq;
using EfRelations.Data;
using EfRelations.Models;
using Microsoft.EntityFrameworkCore;

namespace EfRelations.Views
{
    public class UsersView
    {

        private AppDataContext _context;
        public void AddUser()
        {
            Console.Clear();

            Console.WriteLine("Bienvenido. Este programa es un ejemplo de relaciones Multi-Multi mediante EF Core v5.0");
            Console.WriteLine("=======================================================================================");
            Console.WriteLine("\n---> NUEVO USUARIO:");
            Console.WriteLine("===================");
            Console.WriteLine();

            Console.Write("\t---> Nombre Completo: ");
            var nombre = Console.ReadLine();

            User usuario = new User(nombre);

            using (_context = new AppDataContext())
            {
                _context.Usuarios.Add(usuario);

                _context.SaveChanges();
            }

            Console.WriteLine("\n---> Usuario creado correctamente:");
            Console.WriteLine($"\t---> ID: {usuario.Id} \n\t---> Nombre: {usuario.Nombre}");
            Console.Write("\n---> Presione ENTER para continuar...");
            Console.Read();

            return;
        }

        public void ViewUsers()
        {
            Console.Clear();

            Console.WriteLine("Bienvenido. Este programa es un ejemplo de relaciones Multi-Multi mediante EF Core v5.0");
            Console.WriteLine("=======================================================================================");
            Console.WriteLine("\n---> VER USUARIOS:");
            Console.WriteLine("===================");
            Console.WriteLine();

            Console.WriteLine("\t|  ID  |    NOMBRE    |");
            Console.WriteLine();

            using (_context = new AppDataContext())
            {
                var usuarios = _context.Usuarios.ToList();

                foreach (User user in usuarios)
                {
                    Console.WriteLine($"\t|  {user.Id}  |    {user.Nombre}    ");
                }
            }

            Console.Write("\n---> Presione ENTER para continuar... ");
            Console.Read();

            return;
        }

        public void RemoveUser()
        {
            Console.Clear();

            Console.WriteLine("Bienvenido. Este programa es un ejemplo de relaciones Multi-Multi mediante EF Core v5.0");
            Console.WriteLine("=======================================================================================");

            var User = this.SelectUser();

            if (User == null)
            {
                Console.WriteLine("\n---> Usuario Inexistente");
                Console.Write("\n---> Presione ENTER para continuar... ");
                Console.ReadLine();

                return;
            }

            System.Console.Write($"\n---> ¿Seguro desea eliminar al usuario {User.Nombre}? s/n: ");

            var opcion = char.Parse(Console.ReadLine());

            if (opcion == 's')
            {

                using (_context = new AppDataContext())
                {
                    _context.Usuarios.Remove(User);

                    _context.SaveChanges();
                }

                Console.WriteLine("\n---> Usuario removido con éxito.");

                Console.Write("\n---> Presione ENTER para continuar... ");

                Console.ReadLine();

                return;
            }
            else
            {
                this.RemoveUser();
            }

        }        

        public User SelectUser()
        {
            Console.WriteLine("\n---> SELECCIONAR USUARIOS:");
            Console.WriteLine("==========================");
            Console.WriteLine();

            Console.WriteLine("\t|  ID  |    NOMBRE    |");
            Console.WriteLine();

            using (_context = new AppDataContext())
            {
                var usuarios = _context.Usuarios
                .Include(u => u.UsuarioGrupo)
                .ToList();

                foreach (User user in usuarios)
                {
                    Console.WriteLine($"\t|  {user.Id}  |    {user.Nombre}    ");
                }

                Console.Write("\n---> Seleccione el usuario: ");

                try
                {
                    var UserId = int.Parse(Console.ReadLine());
                    return _context.Usuarios.FirstOrDefault(u => u.Id == UserId);
                }
                catch (System.Exception)
                {
                    return null;
                }
            }
        }

        public void AssignUserToGroup()
        {
            Console.Clear();

            var groupViews = new GruposView();            

            var user = this.SelectUser();

            Console.Clear();

            var group = groupViews.SelectGroup();

            if (user == null || group == null)
            {
                System.Console.WriteLine("\n\t---> Error seleccionando el grupo o el usuario.");
                return;
            }

            try
            {
                System.Console.Write($"\n---> ¿Seguro desea asignar el usuario {user.Nombre} al grupo {group.Nombre}? s/n: ");

                var opcion = Console.ReadLine();

                if (opcion != "s")
                {
                    return;
                }
            }
            catch (System.Exception)
            {
                return;
            }

            using (_context = new AppDataContext())
            {
                // FORMA 1:
                user.UsuarioGrupo.Add(new UserGroup {Usuario = user, Grupo = group});
                _context.Usuarios.Update(user);
                
                // FORMA 2:
                //var userGroup = new UserGroup{Usuario = user, Grupo = group};
                //_context.UsuariosGrupos.Add(new UserGroup{UserId = user.Id, GroupId = group.Id});                

                try
                {
                    _context.SaveChanges();

                    System.Console.WriteLine("\n---> Asignacion correcta.");
                    Console.Write("---> Presione ENTER para continuar... ");
                    Console.ReadLine();
                }
                catch (System.Exception)
                {

                    System.Console.WriteLine("\n\t---> Error. Probablemente el usuario ya pertenezca al grupo.");
                    System.Console.ReadLine();
                    return;
                }

                return;
            }
        }

        public void ViewUsersAndGroups()
        {

            Console.Clear();

            Console.WriteLine("\n---> USUARIOS Y GRUPOS:");
            Console.WriteLine("=======================");

            using (_context = new AppDataContext())
            {
                var users = _context.Usuarios
                    .Include(u => u.UsuarioGrupo)
                    .ThenInclude(ug => ug.Grupo)
                    .ToList();

                foreach (var u in users)
                {
                    System.Console.WriteLine("\n\t---> " + u.Nombre);

                    if (u.UsuarioGrupo.Count() > 0)
                    {
                        foreach (var g in u.UsuarioGrupo)
                        {
                            System.Console.WriteLine($"\t     -> {g.Grupo.Nombre}");
                        }
                    }
                    else
                    {
                        System.Console.WriteLine("\t     -> Este usuario no tiene grupos asignados");
                    }
                }
            }

            Console.Write("\n---> Presione ENTER para continuar... ");
            Console.ReadLine();
        }

        public void RemoveUserFromGroup()
        {
            Console.Clear();

            Console.WriteLine("\n---> QUITAR USUARIOS DE GRUPOS:");
            Console.WriteLine("===============================");

            var User = this.SelectUser();

            if (User == null)
            {
                Console.WriteLine("\n---> Usuario Inexistente");
                Console.Write("\n---> Presione ENTER para continuar... ");
                Console.ReadLine();

                return;
            }

            List<Group> grupos;

            using (_context = new AppDataContext())
            {
                grupos = _context.Grupos
                    .Include(g => g.UsuarioGrupo)
                    .Where(g => g.UsuarioGrupo.Any(ug => ug.UserId == User.Id))
                    .ToList();
            }

            System.Console.WriteLine($"\n\t---> Usuario: {User.Nombre}");

            if (grupos.Count == 0)
            {
                System.Console.WriteLine("\t   -> El usuario no posee grupos.");

                Console.Write("\n---> Presione ENTER para continuar... ");
                Console.ReadLine();

                return;
            }

            foreach (var grupo in grupos)
            {
                System.Console.WriteLine($"\t   {grupo.Id} -> {grupo.Nombre}");
            }

            System.Console.Write("\n---> Seleccione un grupo: ");

            var IdGrupo = int.Parse(Console.ReadLine());

            using (_context = new AppDataContext())
            {
                var grupo = _context.Grupos.FirstOrDefault(g => g.Id == IdGrupo);

                UserGroup userGroup = new UserGroup{GroupId = grupo.Id, UserId = User.Id};

                _context.UsuariosGrupos.Remove(userGroup);

                _context.SaveChanges();
            }

            Console.Write("\n---> Presione ENTER para continuar... ");
            Console.ReadLine();
        }
    
        public void ViewGroupsForUser()
        {
            Console.Clear();

            Console.WriteLine("\n---> VER GRUPOS POR USUARIO:");
            Console.WriteLine("============================");            

            var User = this.SelectUser();
            List<Group> Groups;

            using (_context = new AppDataContext())
            {
                Groups = _context.Grupos
                    .Where(g => g.UsuarioGrupo
                        .Select(ug => ug.Usuario).Contains(User))
                    .ToList();
            }

            System.Console.WriteLine($"\t---> USUARIO: {User.Nombre}");

            foreach (var Group in Groups)
            {
                System.Console.WriteLine($"\t\t-> {Group.Nombre}");
            }

            Console.Write("\n---> Presione ENTER para continuar... ");
            Console.ReadLine();
        }
    }
}