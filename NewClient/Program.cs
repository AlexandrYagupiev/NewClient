using System.Net;
using System.Net.Sockets;


namespace NewClient
{
    class Program
    {
        static void Main(string[] args)
        {
            for (var i = 0; i < 5; i++)
            {
                Console.WriteLine("\n Соединение # "+i.ToString()+"\n");
                Connect("127.0.0.1", "Hello World! #" + i.ToString());
            }
            Console.WriteLine("\n Нажмите Enter...");
            Console.Read();
        }

        static void Connect(string server, string message)
        {
            try
            {
                var port = 9595;
                TcpClient client = new TcpClient(server, port);

                var data = System.Text.Encoding.ASCII.GetBytes(message);

                NetworkStream stream = client.GetStream();

                stream.Write(data, 0, data.Length);
                Console.WriteLine("Отправленно: {0}", message);

                data = new byte[256];

                var responseData = string.Empty;

                //читаем ответы сервера
                var bytes = stream.Read(data, 0, data.Length);
                responseData = System.Text.Encoding.ASCII.GetString(data, 0, bytes);
                Console.WriteLine("Получено: {0}", responseData);

                stream.Close();
                client.Close();
            }
            catch (ArgumentNullException e)
            {
                Console.WriteLine("ArgumentNullException: {0}", e);
            }
            catch (SocketException e)
            {
                Console.WriteLine("SocketException: {0}", e);
            }
        }
    }
}
