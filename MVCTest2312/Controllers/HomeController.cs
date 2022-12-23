using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MVCTest2312.Models;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace MVCTest2312.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Index() => View();

        [HttpPost]
        public string Index(string username, string password)
        {
            var id = 0;
            NpgsqlConnection conn = new NpgsqlConnection("Host=localhost:5432;Username=postgres;Password=mvc2212;Database=postgres");
            var commandText = "SELECT id FROM users WHERE login = @username AND password = @password";
            var command = new NpgsqlCommand(commandText, conn);

            command.Parameters.AddWithValue("@username", username);
            command.Parameters.AddWithValue("@password", password);

            conn.Open();

            NpgsqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                id = int.Parse(reader["id"].ToString());
            }

            conn.Close();

            if (id != 0)
            {
                return "Вы успешно вошли на сайт!";
            }
            else
            {
                return "Неправильный логин или пароль!";
            }
        }
    }
}
