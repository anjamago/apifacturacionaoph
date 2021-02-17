using BackEnd.OpheliaTest.Entities.Interface.Repository;
using BackEnd.OpheliaTest.Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
namespace BackEnd.OpheliaTest.Repositories.DataBase
{
    public class InvoiceRepository:IInvoiceRepository
    {
        private readonly Context.OPHELIATESTContext Context;

        public InvoiceRepository(Context.OPHELIATESTContext context)
        {
            Context = context;
        }

        public Task<List<Client>> getFilterClient()
        {
            List<Client> list = new List<Client>();

            SqlConnection connection = new SqlConnection(Context.Database.GetDbConnection().ConnectionString);
            SqlCommand command = new SqlCommand("Select DISTINCT c.* from INVOICES i INNER JOIN CLIENTS c on i.CLIENT_ID = c.ID where DATEDIFF(YEAR, c.BIRTHDAY, GETDATE()) < 35")
            {
                Connection = connection
            };
            connection.Open();
            SqlDataReader dataReader = command.ExecuteReader();
            if (dataReader.HasRows)
            {
                while (dataReader.Read())
                {
                    list.Add(new Client { 
                        Id= Convert.ToInt32(dataReader["ID"].ToString()),
                        Name = dataReader["NAME"].ToString(),
                        LastName= dataReader["LAST_NAME"].ToString(),
                        Birthday= Convert.ToDateTime(dataReader["BIRTHDAY"].ToString()),
                        Email= dataReader["EMAIL"].ToString(),
                        Address= dataReader["ADDRESS"].ToString(),
                        IdentificationNumber = Convert.ToInt32(dataReader["Id"].ToString())
                    });
                }

            }
            connection.Close();

            return Task.Run(() => list);
        }

        public Task<List<ProductoRequest>> GetTotalSold()
        {
            List<ProductoRequest> list = new List<ProductoRequest>();

            SqlConnection connection = new SqlConnection(Context.Database.GetDbConnection().ConnectionString);
            SqlCommand command = new SqlCommand("SELECT p.PRODUCT, SUM(id.PRINCE_INVOICE) as totalInvoice from INVOICES as inv INNER JOIN INVOICE_DETAIL id on inv.ID = id.ID_INVOICE inner join PRODUCTS p on p.ID = id.ID_PRODUCT where YEAR(inv.INVOICE_DATE) = 2000 GROUP BY p.PRODUCT; ")
            {
                Connection = connection
            };
            connection.Open();
            SqlDataReader dataReader = command.ExecuteReader();

            if (dataReader.HasRows)
            {
                while (dataReader.Read())
                {
                    list.Add(new ProductoRequest
                    {
                        Product = dataReader["PRODUCT"].ToString(),
                        Prince = Convert.ToDouble(dataReader["totalInvoice"].ToString())
                    });
                }

            }
            connection.Close();

            return Task.Run(() => list);
        }
    }
}
