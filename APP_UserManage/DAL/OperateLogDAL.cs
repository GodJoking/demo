using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Helper;
using MySql.Data.MySqlClient;
namespace DAL
{
    public class OperateLogDAL
    {
        #region 插入操作日志
        public int Insert(string UserId, int OperateTypeId, string OperateIP, int SystemTypeId, string EquipmentNum)
        {
            int id = 0;
            try
            {
                string sql = @"INSERT INTO `operatelog`(userId,operateTypeId,operateTime,operateIP,systemTypeId,equipmentNum)VALUES(@UserId,@OperateTypeId,NOW(),@OperateIP,@SystemTypeId,@EquipmentNum);@SELECT IDENTITY;";
                MySqlParameter[] param =
                {
                    new MySqlParameter("@UserId",UserId),
                    new MySqlParameter("@OperateTypeId",OperateTypeId),
                    new MySqlParameter("@OperateIP",OperateIP),
                    new MySqlParameter("@SystemTypeId",SystemTypeId),
                    new MySqlParameter ("@EquipmentNum",EquipmentNum),
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
