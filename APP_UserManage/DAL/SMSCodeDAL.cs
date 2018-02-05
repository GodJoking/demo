using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Helper;
using MySql.Data.MySqlClient;
using Model;
using System.Data;

namespace DAL
{
    public class SMSCodeDAL
    {
        public bool Insert(string mobileNum, DateTime createTime, out string codeValue)
        {
            codeValue = RandomHelper.GetRandom();

            string sql = "INSERT INTO `smscode` (`MobileNum`, `CodeValue`, `CreateTime`, `UsedCount`) VALUES (@AppId, @MobileNum, @CodeValue, @CreateTime, 0);";
            MySqlParameter[] parameters = {
                
                new MySqlParameter("@MobileNum", mobileNum),
                new MySqlParameter("@CodeValue", codeValue),
                new MySqlParameter("@CreateTime", createTime)
            };
            return MySqlHelper.ExecuteNonQuery(ConfigHelper.ConnStr, sql, parameters) > 0;
        }

        public SMSCodeModel GetByMobileNum(string mobileNum, DateTime dt)
        {
            string sql = "SELECT * FROM `smscode` WHERE `MobileNum` = @MobileNum AND `CreateTime` > @CreateTime ORDER BY `CreateTime` DESC LIMIT 0,1;";
            MySqlParameter[] parameters = {
                new MySqlParameter("@MobileNum", mobileNum),
               
                new MySqlParameter("@CreateTime", dt)
            };
            DataRow dr = MySqlHelper.ExecuteDataRow(ConfigHelper.ConnStr, sql, parameters);
            return EntityToModel(dr);
        }

        private SMSCodeModel EntityToModel(DataRow dr)
        {
            if (dr != null)
            {
                SMSCodeModel smsCodeModel = new SMSCodeModel();
                smsCodeModel.Id = dr["Id"].ToInt();
                
                smsCodeModel.MobileNum = dr["MobileNum"].ToString();
                smsCodeModel.CodeValue = dr["CodeValue"].ToString();
                smsCodeModel.CreateTime = dr["CreateTime"].To<DateTime>();
                smsCodeModel.UsedCount = dr["UsedCount"].ToInt();
                return smsCodeModel;
            }
            return null;
        }

        public bool ValidateFail(int id)
        {
            string sql = "Update `smscode` SET `UsedCount` = `UsedCount` + 1 WHERE `Id` = @Id";
            return MySqlHelper.ExecuteNonQuery(ConfigHelper.ConnStr, sql, new MySqlParameter("@Id", id)) > 0;
        }
    }
}
