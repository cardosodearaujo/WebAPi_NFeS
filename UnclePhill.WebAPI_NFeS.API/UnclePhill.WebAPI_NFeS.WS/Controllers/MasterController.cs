﻿using System;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web.Mvc;
using UnclePhill.WebAPI_NFeS.Models;

namespace UnclePhill.WebAPI_NFeS.API.Controllers
{
    public class MasterController : Controller
    {
        //Opções Default para as controllers:
        protected ConnectionManager Conn = new ConnectionManager("unclephill.database.windows.net","BD_NFeS","1433","Administrador","M1n3Rv@7");
        protected StringBuilder SQL = new StringBuilder();
        protected Sessions Session = new Sessions();

        public ActionResult Index()
        {
            return View();
        }

        //Funções de sessão:
        protected bool CheckSession()
        {
            try
            {
                UpdateSession();

                string Session = Request.Headers.Get("SessionHash");
                if (string.IsNullOrEmpty(Session))
                {
                    return false;
                }

                SQL = new StringBuilder();
                SQL.AppendLine(" Select ");
                SQL.AppendLine("    SessionId, ");
                SQL.AppendLine("    UserId, ");
                SQL.AppendLine("    SessionHash, ");
                SQL.AppendLine("    DateStart, ");
                SQL.AppendLine("    DateEnd, ");
                SQL.AppendLine("    Active, ");
                SQL.AppendLine("    DateInsert, ");
                SQL.AppendLine("    DateUpdate ");
                SQL.AppendLine(" From Session ");
                SQL.AppendLine(" Where Active = 1 ");
                SQL.AppendLine(" And DateDiff(MI, DateStart, GetDate()) <= 5 ");
                SQL.AppendLine(" And Session.SessionHash Like '" + NoInjection(Session) + "'");

                DataTable data = Conn.GetDataTable(SQL.ToString(), "Session");
                if (data.Rows.Count > 0)
                {
                    DataRow row = data.AsEnumerable().First();
                    SQL = new StringBuilder();
                    SQL.AppendLine(" Update Session Set ");
                    SQL.AppendLine("    DateStart = GetDate(), ");
                    SQL.AppendLine("    DateEnd = DateAdd(MI,5,GetDate()) ");
                    SQL.AppendLine(" Where SessionId = " + row.Field<long>("SessionId"));

                    Conn.Execute(SQL.ToString());

                    return true;
                }
                throw new Exception("Não foram encontradas sessões para esse usuário!");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        protected void UpdateSession()
        {
            try
            {
                SQL = new StringBuilder();
                SQL.AppendLine(" Update Session Set ");
                SQL.AppendLine("    Active = 0 ");
                SQL.AppendLine(" Where Active = 1 ");
                SQL.AppendLine(" And DateDiff(MI,DateStart,GetDate()) > 5 ");

                Conn.Update(SQL.ToString());

            }
            catch (Exception ex)
            {

            }
        }

        protected Users GetUserSession()
        {
            try
            {
                string SessionHash = Request.Headers.Get("SessionHash");

                if (string.IsNullOrEmpty(SessionHash))
                {
                    throw new Exception("Variavel de Sessão não informada!");
                }

                DataTable data;

                SQL = new StringBuilder();
                SQL.AppendLine(" Select ");
                SQL.AppendLine("    UserId ");
                SQL.AppendLine(" From Session ");
                SQL.AppendLine(" Where Active = 1 ");
                SQL.AppendLine(" And SessionHash Like '" + Session + "'");

                data = Conn.GetDataTable(SQL.ToString(),"Session");

                if (data != null && data.Rows.Count > 0)
                {
                    SQL = new StringBuilder();
                    SQL.AppendLine(" Select * From Users ");
                    SQL.AppendLine(" Where Active = 1 ");
                    SQL.AppendLine(" And UserId = " + data.AsEnumerable().First().Field<long>("UserId"));

                    data = Conn.GetDataTable(SQL.ToString(), "Users");
                    if (data != null && data.Rows.Count > 0)
                    {
                        DataRow row = data.AsEnumerable().First();
                        
                        Users users = new Users();
                        users.UserId = row.Field<long>("UserId");
                        users.Name = row.Field<string>("Name");
                        users.LastName = row.Field<string>("LastName");
                        users.CPF = row.Field<string>("CPF");
                        users.Email = row.Field<string>("Email");
                        users.Password = row.Field<string>("Password");
                        users.SessionHash = string.Empty;
                        users.Active = row.Field<bool>("Active");
                        users.DateInsert = row.Field<DateTime>("DateInsert").ToString("dd-MM-yyyy");
                        users.DateUpdate = row.Field<DateTime>("DateUpdate").ToString("dd-MM-yyyy");

                        return users;                        
                    }
                    throw new Exception("Usuário não encontrado!");
                }
                throw new Exception("Sessão não encontrada!");
            }
            catch (Exception ex)
            {
                throw ex;
            }            
        }

        protected string GenerateHash(string Password)
        {
            try
            {
                UnicodeEncoding unicode = new UnicodeEncoding();
                byte[] passwordByte = unicode.GetBytes(Password + DateTime.Now.ToString());
                SHA1Managed SHA1 = new SHA1Managed();
                byte[] hashByte = SHA1.ComputeHash(passwordByte);
                string hash = string.Empty;

                foreach (byte b in hashByte)
                {
                    hash += b.ToString();
                }
                return hash;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        //Funções diversas:
        protected string NoInjection(string Value)
        {
            return Value.Replace("'", "''");
        }

        protected string FormatNumber(decimal Value)
        {
            return Value.ToString().Replace(".","").Replace(",",".");
        }

        protected JsonResult Response(object Param)
        {
            return Json(Param, JsonRequestBehavior.AllowGet);
        }
    }
}