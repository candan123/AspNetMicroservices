using Dapper;
using Discount.API.Entities;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Discount.API.Repository
{
    public class DiscountRepository:IDiscountRepository
    {
        private readonly IConfiguration _configuration;

        public DiscountRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<Coupan> GetDiscount(string productName)
        {
            using var connection = new NpgsqlConnection
                                  (_configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
            var coupan = await connection.QueryFirstOrDefaultAsync<Coupan>
                ("SELECT * FROM Coupon WHERE ProductName= @ProductName", new { ProductName = productName });
            if (coupan == null)
                return new Coupan { Productname = "No Discount", Amount = 0, Description = "No Discription" };
            return coupan;
        }

        public async Task<bool> DeleteDiscount(string productName)
        {
            using var connection = new NpgsqlConnection
                                  (_configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
            var affected = await connection.ExecuteAsync("DELETE FROM Coupon WHERE productname= @ProductName", 
                new { ProductName = productName });
            if (affected == 0)
                return false;
            return true;
        }

        public async Task<bool> CreateDiscount(Coupan coupan)
        {
            using var connection = new NpgsqlConnection
                                  (_configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
            var affected = await connection.ExecuteAsync
                ("INSERT INTO Coupon(productName, description, amount) VALUES(@productName, @description, @amount)",
                new { productName = coupan.Productname, description = coupan.Description, amount=coupan.Amount });
            if (affected == 0)
                return false;
            return true;
        }

        public async Task<bool> UpdateDiscount(Coupan coupan)
        {
            using var connection = new NpgsqlConnection
                                  (_configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
            var affected = await connection.ExecuteAsync
                ("UPDATE Coupon SET productname=@productName, description=@description, amount=@amount WHERE id= @id",
                new { productName = coupan.Productname, description = coupan.Description, amount=coupan.Amount , id=coupan.Id});
            if (affected == 0)
                return false;
            return true;
        }
    }
}
