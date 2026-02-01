using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using ContactManagerSQL.Models;
using ContactManagerSQL.Utils;

namespace ContactManagerSQL.Data;

internal class ContactRepository
{
    private readonly string _connectionString;
    public ContactRepository(string connectionString) => _connectionString = connectionString;

    // CREATE
    public int Add(Contact c)
    {
        const string sql = @"
INSERT INTO Contacts (FirstName, LastName, Phone, Email)
VALUES (@fn,@ln,@ph,@em);
SELECT CAST(SCOPE_IDENTITY() AS int);";

        try
        {
            using SqlConnection conn = new SqlConnection(_connectionString);
            conn.Open();
            using SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@fn", c.FirstName);
            cmd.Parameters.AddWithValue("@ln", c.LastName);
            cmd.Parameters.AddWithValue("@ph", (object?)c.Phone ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@em", (object?)c.Email ?? DBNull.Value);
            int newId = (int)cmd.ExecuteScalar();
            Logger.Info($"Add Contact OK (Id={newId}, {c.FirstName} {c.LastName})");
            return newId;
        }
        catch (Exception ex)
        {
            Logger.Error($"Add Contact FAILED: {ex.Message}");
            throw;
        }
    }

    // READ
    public List<Contact> GetAll()
    {
        const string sql = "SELECT Id, FirstName, LastName, Phone, Email FROM Contacts ORDER BY Id;";
        var list = new List<Contact>();
        try
        {
            using SqlConnection conn = new SqlConnection(_connectionString);
            conn.Open();
            using SqlCommand cmd = new SqlCommand(sql, conn);
            using SqlDataReader r = cmd.ExecuteReader();
            while (r.Read())
            {
                list.Add(new Contact
                {
                    Id = r.GetInt32(0),
                    FirstName = r.GetString(1),
                    LastName = r.GetString(2),
                    Phone = r.IsDBNull(3) ? null : r.GetString(3),
                    Email = r.IsDBNull(4) ? null : r.GetString(4)
                });
            }
            Logger.Info($"GetAll OK (count={list.Count})");
            return list;
        }
        catch (Exception ex)
        {
            Logger.Error($"GetAll FAILED: {ex.Message}");
            throw;
        }
    }

    // SEARCH
    public List<Contact> SearchByLastName(string lastNamePart)
    {
        const string sql = "SELECT Id, FirstName, LastName, Phone, Email FROM Contacts WHERE LastName LIKE @ln ORDER BY LastName;";
        var list = new List<Contact>();
        try
        {
            using SqlConnection conn = new SqlConnection(_connectionString);
            conn.Open();
            using SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@ln", "%" + lastNamePart + "%");
            using SqlDataReader r = cmd.ExecuteReader();
            while (r.Read())
            {
                list.Add(new Contact
                {
                    Id = r.GetInt32(0),
                    FirstName = r.GetString(1),
                    LastName = r.GetString(2),
                    Phone = r.IsDBNull(3) ? null : r.GetString(3),
                    Email = r.IsDBNull(4) ? null : r.GetString(4)
                });
            }
            Logger.Info($"SearchByLastName OK (query='{lastNamePart}', count={list.Count})");
            return list;
        }
        catch (Exception ex)
        {
            Logger.Error($"SearchByLastName FAILED: {ex.Message}");
            throw;
        }
    }

    // UPDATE
    public bool Update(Contact c)
    {
        const string sql = "UPDATE Contacts SET FirstName=@fn, LastName=@ln, Phone=@ph, Email=@em WHERE Id=@id;";
        try
        {
            using SqlConnection conn = new SqlConnection(_connectionString);
            conn.Open();
            using SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@fn", c.FirstName);
            cmd.Parameters.AddWithValue("@ln", c.LastName);
            cmd.Parameters.AddWithValue("@ph", (object?)c.Phone ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@em", (object?)c.Email ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@id", c.Id);
            int rows = cmd.ExecuteNonQuery();
            Logger.Info($"Update Contact {(rows > 0 ? "OK" : "NOT FOUND")} (Id={c.Id})");
            return rows > 0;
        }
        catch (Exception ex)
        {
            Logger.Error($"Update Contact FAILED: {ex.Message}");
            throw;
        }
    }

    // DELETE
    public bool Delete(int id)
    {
        const string sql = "DELETE FROM Contacts WHERE Id=@id;";
        try
        {
            using SqlConnection conn = new SqlConnection(_connectionString);
            conn.Open();
            using SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@id", id);
            int rows = cmd.ExecuteNonQuery();
            Logger.Info($"Delete Contact {(rows > 0 ? "OK" : "NOT FOUND")} (Id={id})");
            return rows > 0;
        }
        catch (Exception ex)
        {
            Logger.Error($"Delete Contact FAILED: {ex.Message}");
            throw;
        }
    }

    // BULK INSERT
    public int BulkInsert(List<Contact> contacts)
    {
        if (contacts == null || contacts.Count == 0) return 0;

        const string sql = "INSERT INTO Contacts (FirstName, LastName, Phone, Email) VALUES (@fn,@ln,@ph,@em);";

        using SqlConnection conn = new SqlConnection(_connectionString);
        conn.Open();
        using SqlTransaction tx = conn.BeginTransaction();

        try
        {
            int count = 0;
            foreach (var c in contacts)
            {
                using SqlCommand cmd = new SqlCommand(sql, conn, tx);
                cmd.Parameters.AddWithValue("@fn", c.FirstName);
                cmd.Parameters.AddWithValue("@ln", c.LastName);
                cmd.Parameters.AddWithValue("@ph", (object?)c.Phone ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@em", (object?)c.Email ?? DBNull.Value);
                count += cmd.ExecuteNonQuery();
            }
            tx.Commit();
            Logger.Info($"BulkInsert OK (count={count})");
            return count;
        }
        catch (Exception ex)
        {
            tx.Rollback();
            Logger.Error($"BulkInsert FAILED - ROLLBACK: {ex.Message}");
            throw;
        }
    }
}
