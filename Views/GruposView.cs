using System;
using System.Collections.Generic;
using System.Linq;
using EfRelations.Data;
using EfRelations.Models;
using Microsoft.EntityFrameworkCore;

namespace EfRelations.Views
{
    public class GruposView
    {
        private AppDataContext _context;

        public void AddGroup()
        {
            Console.Clear();

            Console.WriteLine("Bienvenido. Este programa es un ejemplo de relaciones Multi-Multi mediante EF Core v5.0");
            Console.WriteLine("=======================================================================================");
            Console.WriteLine("\n---> NUEVO GRUPO:");
            Console.WriteLine("===================");
            Console.WriteLine();

            Console.Write("\t---> Nombre Del Grupo: ");
            var nombre = Console.ReadLine();

            Group group = new Group(nombre);

            using (_context = new AppDataContext())
            {
                _context.Grupos.Add(group);

                _context.SaveChanges();
            }

            Console.WriteLine("\n---> Grupo creado correctamente:");
            Console.WriteLine($"\t---> Nombre: {group.Nombre}");
            Console.Write("\n---> Presione ENTER para continuar...");
            Console.Read();

            return;
        }

        public void ViewGroups()
        {
            Console.Clear();

            Console.WriteLine("Bienvenido. Este programa es un ejemplo de relaciones Multi-Multi mediante EF Core v5.0");
            Console.WriteLine("=======================================================================================");
            Console.WriteLine("\n---> VER GRUPOS:");
            Console.WriteLine("===================");
            Console.WriteLine();

            Console.WriteLine("\t|  ID  |    NOMBRE    |");
            Console.WriteLine();

            using (_context = new AppDataContext())
            {
                var grupos = _context.Grupos.ToList();

                foreach (Group grupo in grupos)
                {
                    Console.WriteLine($"\t|  {grupo.Id}  |    {grupo.Nombre}    ");
                }
            }

            Console.Write("\n---> Presione ENTER para continuar... ");
            Console.Read();

            return;
        }

        public void RemoveGroup()
        {
            Console.Clear();

            Console.WriteLine("Bienvenido. Este programa es un ejemplo de relaciones Multi-Multi mediante EF Core v5.0");
            Console.WriteLine("=======================================================================================");

            var Grupo = this.SelectGroup();

            if (Grupo == null)
            {
                Console.WriteLine("\n---> Grupo Inexistente");
                Console.Write("\n---> Presione ENTER para continuar... ");
                Console.ReadLine();

                return;
            }

            System.Console.Write($"\n---> ¿Seguro desea eliminar el grupo {Grupo.Nombre}? s/n: ");

            try
            {
                var opcion = char.Parse(Console.ReadLine());

                if (opcion == 's')
                {
                    using (_context = new AppDataContext())
                    {
                        _context.Grupos.Remove(Grupo);

                        _context.SaveChanges();
                    }

                    Console.WriteLine("\n---> Grupo removido con éxito.");

                    Console.Write("\n---> Presione ENTER para continuar... ");

                    Console.ReadLine();

                    return;
                }
                else
                {
                    this.RemoveGroup();
                }
            }
            catch (System.Exception)
            {
                Console.WriteLine("\n---> Grupo no removido.");

                Console.Write("\n---> Presione ENTER para continuar... ");

                Console.ReadLine();

                return;
            }

        }

        public Group SelectGroup()
        {
            Console.WriteLine("\n---> SELECCIONAR GRUPOS:");
            Console.WriteLine("========================");
            Console.WriteLine();

            Console.WriteLine("\t|  ID  |    NOMBRE    |");
            Console.WriteLine();

            using (_context = new AppDataContext())
            {
                var grupos = _context.Grupos                
                .ToList();

                foreach (Group grupo in grupos)
                {
                    Console.WriteLine($"\t|  {grupo.Id}  |    {grupo.Nombre}    ");
                }
                
                Console.Write("\n---> Seleccione el grupo: ");

                try
                {
                    var GrupoId = int.Parse(Console.ReadLine());
                    return grupos.FirstOrDefault(g => g.Id == GrupoId);
                }
                catch (System.Exception)
                {
                    return null;
                }
            }
        }
    
        public void ViewGroupsAndUsers()
        {
            Console.Clear();
            
            Console.WriteLine("\n---> VER GRUPOS CON USUARIOS:");
            Console.WriteLine("=============================");
            Console.WriteLine();

            List<Group> grupos;

            using (_context = new AppDataContext())
            {
                grupos = _context.Grupos
                    .Include(g => g.UsuarioGrupo)
                    .ThenInclude(ug => ug.Usuario)
                    .ToList();
            }

            foreach (Group g in grupos)
            {
                System.Console.WriteLine($"\t-> {g.Nombre}:");

                foreach (UserGroup u in g.UsuarioGrupo)
                {
                    System.Console.WriteLine($"\t    ->{u.Usuario.Nombre}");
                }

                Console.WriteLine();
            }
            
            Console.Write("\n---> Presione ENTER para continuar... ");
            Console.ReadLine();
        }
    
        public void ViewUsersInGroup()
        {
            Console.Clear();

            Console.WriteLine("\n---> VER USUARIOS EN EL GRUPO:");
            Console.WriteLine("==============================");
            Console.WriteLine();

            var group = this.SelectGroup();

            System.Console.WriteLine($"\t---> GRUPO: {group.Nombre}");
            System.Console.WriteLine("\n\t---> USUARIOS: ");

            using (_context = new AppDataContext())
            {
                var users = _context.Usuarios
                    .Where(u => u.UsuarioGrupo.Select(ug => ug.Grupo).Contains(group))                
                    .ToList();

                foreach (var user in users)
                {
                    System.Console.WriteLine($"\t\t-> {user.Nombre}");
                }
            }

            Console.Write("\n---> Presione ENTER para continuar... ");
            Console.ReadLine();
        }
    }
}