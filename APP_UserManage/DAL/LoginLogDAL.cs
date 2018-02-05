using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Helper;
using MySql.Data.MySqlClient;
namespace DAL
{
    public class LoginLogDAL
    {
        #region 插入登录日志
        public int Insert(string UserId, string LoginIP, int SystemTypeId, string EquipmentNum)
        {
            int id = 0;
            try
            {
                string sql = @"INSERT INTO `loginlog`(userId,loginIP,loginTime,systemTypeId,equipmentNum) VALUES(@UserId,NOW(),@LoginIP,@SystemTypeId,@EquipmentNum);@@SELECT IDENTITY;";
                MySqlParameter[] param =
                {
                    new MySqlParameter("@UserId",UserId),
                    new MySqlParameter("@LoginIP",LoginIP),
                    new MySqlParameter("@SystemTypeId",SystemTypeId),
                    new MySqlParameter("@EquipmentNum",EquipmentNum),
                };
                id = MySqlHelper.ExecuteScalar(ConfigHelper.ConnStr, sql, param).ToInt();
            }
            catch (Exception ex)
            {
                LogHelper.Error(ex);
            }
            return id;
        } 
        #endregion
    }
}
