using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace ASP.NET_FinalTermExam.Models
{
    public class Employee    
    {

        /// <summary>
        /// 員工編號
        /// </summary>

      //  [DisplayName("員工編號")]
       
        public string EmployeeId { get; set; }



        /// <summary>
        /// 員工姓名
        /// </summary>
      //  [DisplayName("員工姓名")]
        public string EmployeeName { get; set; }



        /// <summary>
        /// 職稱
        /// </summary>
        /// 
      //  [DisplayName("職稱")]
        public string Job { get; set; }



        

        /// <summary>
        /// 任職日期
        /// </summary>
        /// 
       // [DisplayName("任職日期")]
        public DateTime? Employeedate { get; set; }


        






    }
}