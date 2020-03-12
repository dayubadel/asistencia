using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Net;

namespace H_AsistenciaPosgrado.Models.Metodos
{
    public class Correo
    {
        public string EnviarCorreo(string _correoDestino, string _asunto, string _mensaje)
        {
            var client = new SmtpClient("smtp.gmail.com", 587)
            {
                Credentials = new NetworkCredential("helenbailon95@gmail.com", "Bailon1995"),
                EnableSsl = true
            };
            client.Send("helenbailon95@gmail.com", _correoDestino, _asunto, _mensaje);
            return "0";
        }
    }
}