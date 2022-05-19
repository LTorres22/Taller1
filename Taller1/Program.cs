using ClienteSocketUtil;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Taller1
{
    public class Program
    {
        static void Main(string[] args)
        {
            int puerto = Convert.ToInt32(ConfigurationManager.AppSettings["puerto"]);
            ServerSocket servidor = new ServerSocket(puerto);

            if (servidor.Iniciar())
            {
                Console.WriteLine("Servidor Iniciado");
                while (true)
                {
                    Console.WriteLine("Esperando Cliente");
                    Socket socketCliente = servidor.ObtenerCliente();
                    ClienteCom cliente = new ClienteCom(socketCliente);

                    bool Estado = true;
                    do
                    {
                        cliente.Escribir("Hola Nuevo cliente, Cual es tu mensaje:");
                        string mensaje = cliente.Leer();
                        Console.WriteLine("El cliente dice: {0}", mensaje);
                        string respuesta = Console.ReadLine();
                        cliente.Escribir(respuesta);

                        if (respuesta == "chao")
                        {
                            cliente.Desconectar();
                            Estado = false;
                            Console.WriteLine("Hasta Luego");
                        }

                    } while (Estado);
                }
            }
            else
            {
                Console.WriteLine("Error, el puerto {0} esta en uso ", puerto);
            }
        }
    }
}