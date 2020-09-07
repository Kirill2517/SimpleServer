using MySql.Data.MySqlClient;
using Server.Extensions;
using Server.Models;
using Server.Models.Book;
using Server.Models.Bookr;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static Server.Data.Constants;
namespace Server.Data
{
    public class DataBase
    {
        private readonly MySqlConnection Connection;

        public DataBase()
        {
            Connection = new MySqlConnection(Constants.ConnectionString);
            Connection.Open();
        }

        internal Role GetRoleOfUser(int userId)
        {
            var cmd = string.Format(Requests.GetRoleOfUser, StringsDataBase.students, userId);

            return new Role(this.DataReaderMapToList<RoleFromRequest>(this.GetCommand(cmd))[0]);
        }

        internal bool AddBook(Book book)
        {
            var cmd = GenerateRequestInsert(book, StringsDataBase.books);
            try
            {
                this.ExecuteCommand(cmd);
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        internal List<StudentView> GetAllStudents()
        {
            var command = string.Format(Constants.Requests.GetRequest, StringsDataBase.students);

            return this.DataReaderMapToList<StudentView>(GetCommand(command));
        }

        internal List<Book> GetAllBooks()
        {
            var command = string.Format(Constants.Requests.GetRequest, StringsDataBase.books);

            return this.DataReaderMapToList<Book>(GetCommand(command));
        }

        internal bool BookISBNIxists(string isbn)
        {
            var checkISBN = string.Format(Requests.CheckFieldString, "isbn", StringsDataBase.books, isbn);
            return IsExists(checkISBN);
        }

        internal bool removeBook(string isbn)
        {
            var cmd = string.Format(Requests.DeleteIndexString, StringsDataBase.books, "isbn", isbn);
            try
            {
                this.ExecuteCommand(cmd);
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        internal bool CheckUserData(StudentRegistration student)
        {
            var checkLogin = string.Format(Requests.CheckFieldString, "login", StringsDataBase.students, student.login);
            if (IsExists(checkLogin))
                return true;
            var checkName = string.Format(Requests.CheckFieldString, "studentName", StringsDataBase.students, student.studentName);
            if (IsExists(checkName))
                return true;
            return false;
        }

        internal void Registration(StudentRegistration student)
        {
            var st = student.Clone() as StudentRegistration;
            st.password = st.password.PasswordToHash();
            var cmd = GenerateRequestInsert(st, StringsDataBase.students);
            this.ExecuteCommand(cmd);
        }

        internal List<Bookr> GetAllBookrs()
        {
            var command = string.Format(Constants.Requests.GetRequest, StringsDataBase.bookrs);

            return this.DataReaderMapToList<Bookr>(GetCommand(command));
        }

        /// <summary>
        /// Вернуть
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        private DbDataReader GetCommand(string command)
        {
            using var cmd = new MySqlCommand(command, Connection);

            return cmd.ExecuteReader();
        }
        /// <summary>
        /// Без возврата
        /// </summary>
        /// <param name="command"></param>
        private void ExecuteCommand(string command)
        {
            using var cmd = new MySqlCommand(command, Connection);

            cmd.ExecuteNonQuery();
        }

        private bool IsExists(string command)
        {
            using var cmd = new MySqlCommand(command, Connection);
            var reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                var isExists = Convert.ToBoolean(reader.GetValue(0));
                reader.Close();
                return isExists;
            }
            return false;
        }

        private List<T> DataReaderMapToList<T>(IDataReader dr)
        {
            List<T> list = new List<T>();
            while (dr.Read())
            {
                T obj = Activator.CreateInstance<T>();
                foreach (PropertyInfo prop in obj.GetType().GetProperties())
                {
                    if (!object.Equals(dr[prop.Name], DBNull.Value))
                    {
                        prop.SetValue(obj, dr[prop.Name], null);
                    }
                }
                list.Add(obj);
            }
            dr.Close();
            return list;
        }

        internal StudentView GetStudentOrDefault(string login, string password)
        {
            var cmd = string.Format(Requests.GetUser, StringsDataBase.students, login, password.PasswordToHash());

            var reader = this.GetCommand(cmd);
            var students = this.DataReaderMapToList<StudentView>(reader);
            if (students.Count == 1)
                return students[0];
            return null;
        }

        private string GenerateRequestInsert(object obj, string table)
        {
            var cmd = new StringBuilder($"insert novelladatabase.{table}(");
            List<object> values = new List<object>();
            foreach (PropertyInfo prop in obj.GetType().GetProperties())
            {
                object value = prop.GetValue(obj);
                if (value != null)
                {
                    cmd.Append(prop.Name + ",");
                    values.Add(value);
                }
            }
            cmd.Length--;
            cmd.Append(")");
            cmd.Append("values (");
            foreach (var item in values)
            {
                if (item.GetType() == typeof(string))
                    cmd.Append($"\"{item}\",");
                else
                    cmd.Append(item + ",");
            }
            cmd.Length--;
            cmd.Append(");");
            return cmd.ToString();
        }
    }
}
