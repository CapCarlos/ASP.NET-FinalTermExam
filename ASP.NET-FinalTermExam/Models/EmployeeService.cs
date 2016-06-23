using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;


namespace ASP.NET_FinalTermExam.Models
{
    public class EmployeeService
    {


        /// <summary>
        /// 取得DB連線字串
        /// </summary>
        /// <returns></returns>
        private string GetDBConnectionString()
        {
            return
                System.Configuration.ConfigurationManager.ConnectionStrings["DBConn"].ConnectionString.ToString();
        }


        /// <summary>
        /// 依照員工ID取得員工資料
        /// </summary>
        /// <param name="id">員工ID</param>
        /// <returns></returns>
        public Models.Employee GetEmployeeById(string employeeid)
        {
            DataTable dt = new DataTable();
            string sql = @"SELECT * FROM (SELECT * FROM dbo.CodeTable WHERE CodeType = 'TITLE') AS J
INNER JOIN (SELECT EmployeeID , lastname+ firstname As EmpName , Title FROM HR.Employees ) AS H ON  J.CodeId  = H.Title 
   
                    WHERE H.EmployeeID = @EmployeeId";
            using (SqlConnection conn = new SqlConnection(this.GetDBConnectionString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.Add(new SqlParameter("@EmployeeId", employeeid));

                SqlDataAdapter sqlAdapter = new SqlDataAdapter(cmd);
                sqlAdapter.Fill(dt);
                conn.Close();
            }
            return this.MapEmployeeDataToList(dt).FirstOrDefault();
        }




        /// <summary>
        /// 依照條件取得訂單資料
        /// </summary>
        /// <returns></returns>
        public List<Models.Employee> GetEmployeeByCondtioin(Models.EmployeeSearchArg arg)
        {

            DataTable dt = new DataTable();
            string sql = @"SELECT * FROM (SELECT * FROM dbo.CodeTable WHERE CodeType = 'TITLE') AS J
INNER JOIN (SELECT EmployeeID , lastname+ firstname As EmpName , Title FROM HR.Employees ) AS H ON  J.CodeId  = H.Title
					Where (H.EmployeeId Like '%'+@EmployeeId+'%' Or @EmployeeId='') AND 
						  (H.HireDate=@Employeedate Or @Employeedate='') AND (H.EmpName=@EmployeeName Or @EmployeeName='')";


            using (SqlConnection conn = new SqlConnection(this.GetDBConnectionString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.Add(new SqlParameter("@EmployeeId", arg.EmployeeId == null ? string.Empty : arg.EmployeeId));             
                cmd.Parameters.Add(new SqlParameter("@EmployeeName", arg.EmployeeName == null ? string.Empty : arg.EmployeeName));
                cmd.Parameters.Add(new SqlParameter("@Employeedate", arg.Employeedate == null ? string.Empty : arg.Employeedate));
              
                SqlDataAdapter sqlAdapter = new SqlDataAdapter(cmd);
                sqlAdapter.Fill(dt);
                conn.Close();
            }


            return this.MapEmployeeDataToList(dt);
        }





        /// <summary>
        /// 新增員工
    
        /// </summary>
        public int InsertOrder(Models.Employee employee)
        {
            string sql = @"INSERT INTO Sales.Orders(
	                        CustomerID,EmployeeID,orderdate,requireddate,shippeddate,shipperid,freight,
	                        shipname,shipaddress,shipcity,shipregion,shippostalcode,shipcountry
                        )VALUES(
							@CustomerID,@EmployeeID,@Orderdate,@RequireDdate,@ShippedDate,@ShipperID,@Freight,
							@ShipName,@ShipAddress,@ShipCity,@ShipRegion,@ShipPostalCode,@ShipCountry
						)
						SELECT SCOPE_IDENTITY()
						";
            int EmployeeId;
            using (SqlConnection conn = new SqlConnection(this.GetDBConnectionString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.Add(new SqlParameter("@EmployeeId", employee.EmployeeId));
                cmd.Parameters.Add(new SqlParameter("@EmployeeName", employee.EmployeeName));
                 cmd.Parameters.Add(new SqlParameter("@Job", employee.Job));
                cmd.Parameters.Add(new SqlParameter("@Employeedate", employee.Employeedate));
               

                EmployeeId = Convert.ToInt32(cmd.ExecuteScalar());
                conn.Close();
            }
            return EmployeeId;
        }











        private List<Models.Employee> MapEmployeeDataToList(DataTable Employeedate)
        {
            List<Models.Employee> result = new List<Employee>();


            foreach (DataRow row in Employeedate.Rows)
            {
                result.Add(new Employee()
                {

                    EmployeeId = row["EmployeeId"].ToString(),

                    EmployeeName = row["EmployeeName"].ToString(),

                    Employeedate = row["Employeedate"] == DBNull.Value ? (DateTime?)null : (DateTime)row["Employeedate"],

                    Job = row["Job"].ToString()
                });
            }
            return result;
        }











    }
}