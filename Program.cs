using System;
using EfRelations.Views;

namespace EfRelations
{
    class Program
    {
        static void Main(string[] args)
        {
            CreateMenu();
        }

        public static void CreateMenu()
        {
            string opcion = "";
            UsersView usuarios;
            GruposView grupos;

            while (opcion != "q")
            {
                Console.Clear();

                Console.WriteLine("Bienvenido. Este programa es un ejemplo de relaciones Multi-Multi mediante EF Core v5.0");
                Console.WriteLine("=======================================================================================");
                Console.WriteLine("\n---> MENU PRINCIPAL:");
                Console.WriteLine("====================");
                Console.WriteLine();

                Console.WriteLine("\t---> Usuarios:");
                Console.WriteLine("\t\t11) ---> Nuevo Usuario");
                Console.WriteLine("\t\t12) ---> Ver Usuarios");
                Console.WriteLine("\t\t13) ---> Eliminar Usuario");
                Console.WriteLine("\t\t14) ---> Asignar Usuario a Grupo");
                Console.WriteLine("\t\t15) ---> Ver Usuarios y Grupos");
                Console.WriteLine("\t\t16) ---> Ver Grupos de un determinado usaurio");
                Console.WriteLine("\t\t17) ---> Eliminar Usuario De Un Grupo");
                Console.WriteLine();

                Console.WriteLine("\t---> Grupos:");
                Console.WriteLine("\t\t21) ---> Nuevo Grupo");
                Console.WriteLine("\t\t22) ---> Ver Grupos");
                Console.WriteLine("\t\t23) ---> Eliminar Grupo");
                Console.WriteLine("\t\t24) ---> Ver Grupos Con Usuarios");
                Console.WriteLine("\t\t25) ---> Ver Usuarios de un determinado grupo");
                Console.WriteLine();

                Console.WriteLine("\t---> Administración:");
                Console.WriteLine("\t\tq) ---> Salir");
                Console.WriteLine();

                Console.Write("\nELIGA UNA OPCION: ");
                opcion = Console.ReadLine();

                switch (opcion)
                {
                    case "11":
                        usuarios = new UsersView();
                        usuarios.AddUser();
                        break;
                    case "12":
                        usuarios = new UsersView();
                        usuarios.ViewUsers();
                        break;
                    case "13":
                        usuarios = new UsersView();
                        usuarios.RemoveUser();
                        break;
                    case "14":
                        usuarios = new UsersView();
                        usuarios.AssignUserToGroup();
                        break;
                    case "15":
                        usuarios = new UsersView();
                        usuarios.ViewUsersAndGroups();
                        break;
                    case "16":
                        usuarios = new UsersView();
                        usuarios.ViewGroupsForUser();
                        break;
                    case "17":
                        usuarios = new UsersView();
                        usuarios.RemoveUserFromGroup();
                        break;
                    case "21":
                        grupos = new GruposView();
                        grupos.AddGroup();
                        break;
                    case "22":
                        grupos = new GruposView();
                        grupos.ViewGroups();
                        break;
                    case "23":
                        grupos = new GruposView();
                        grupos.RemoveGroup();
                        break;
                    case "24":
                        grupos = new GruposView();
                        grupos.ViewGroupsAndUsers();
                        break;
                    case "25":
                        grupos = new GruposView();
                        grupos.ViewUsersInGroup();
                        break;
                    case "q":
                        break;
                }
            }
        }
    }
}
