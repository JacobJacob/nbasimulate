using System;
using System.Collections.Generic;
using System.Text;
using System.Data;  
using System.Data.SqlClient;  

namespace LoginParameter
{
	class TestConnection
	{

         static void Main(string[] args)  
         {
             string strDataBase = DBLogin.GetConnWithTime(0, 60);  
             SqlConnection conn = new SqlConnection(strDataBase);  
             string sqlStatement = "select * from Table_1";  
             SqlCommand sqlcmd = new SqlCommand(sqlStatement, conn);            //设置参数  
             conn.Open();  
             SqlDataReader sdr = sqlcmd.ExecuteReader(); //执行SQL语句  
             int cols = sdr.FieldCount;   //获取结果行中的列数  
             object[] values = new object[cols];  
             while (sdr.Read())  
             {  
                 sdr.GetValues(values);       //values保存一行数据  
                 for (int i = 0; i < values.Length; i++)  
                 {  
                     Console.Write(values[i].ToString() + " ");  
                 }  
                 Console.WriteLine();  
             }  
             sdr.Close();  
             conn.Close();  
         }  

	}
}
