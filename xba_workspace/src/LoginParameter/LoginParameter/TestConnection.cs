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
             SqlCommand sqlcmd = new SqlCommand(sqlStatement, conn);            //���ò���  
             conn.Open();  
             SqlDataReader sdr = sqlcmd.ExecuteReader(); //ִ��SQL���  
             int cols = sdr.FieldCount;   //��ȡ������е�����  
             object[] values = new object[cols];  
             while (sdr.Read())  
             {  
                 sdr.GetValues(values);       //values����һ������  
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
