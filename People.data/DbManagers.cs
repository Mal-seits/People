using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace People.data
{
    public class Person
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }    
        public int Age { get; set; }
    }
    public class DbManager
    {
        private string _constr;
        public DbManager(string constr)
        {
            _constr = constr;
        }
        public List<Person> GetAllPeople()
        {
            using var connection = new SqlConnection(_constr);
            using var command = connection.CreateCommand();
            command.CommandText = @"SELECT * FROM People";
            connection.Open();
            List<Person> list = new List<Person>();
            var reader = command.ExecuteReader();
            while (reader.Read())
            {
                Person p = new Person();
                p.Id = (int)reader["id"];
                p.FirstName = (string)reader["firstName"];
                p.LastName = (string)reader["lastName"];
                p.Age = (int)reader["age"];
                list.Add(p);
            }
            return list;
        }
        public void AddPerson(Person person)
        {
            using var connection = new SqlConnection(_constr);
            using var command = connection.CreateCommand();
            command.CommandText = @"INSERT INTO People VALUES(@firstName, @lastName, @age)";
            command.Parameters.AddWithValue("@firstName", person.FirstName);
            command.Parameters.AddWithValue("@lastName", person.LastName);
            command.Parameters.AddWithValue("@age", person.Age);
            connection.Open();
            command.ExecuteNonQuery();
        }
        public void EditPerson(Person person)
        {
            using var connection = new SqlConnection(_constr);
            using var command = connection.CreateCommand();
            command.CommandText = @"UPDATE People SET FirstName = @firstName, LastName = @lastName, Age = @age WHERE Id = @id";
            command.Parameters.AddWithValue("@firstName", person.FirstName);
            command.Parameters.AddWithValue("@lastName", person.LastName);
            command.Parameters.AddWithValue("@age", person.Age);
            command.Parameters.AddWithValue("@id", person.Id);
            connection.Open();
            command.ExecuteNonQuery();
        }
        public void DeletePerson(int id)
        {
            using var connection = new SqlConnection(_constr);
            using var command = connection.CreateCommand();
            command.CommandText = @"DELETE FROM People WHERE Id = @id";
            command.Parameters.AddWithValue("@id", id);
            connection.Open();
            command.ExecuteNonQuery();
        }
    }
}
