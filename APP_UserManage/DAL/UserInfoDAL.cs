using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using Helper;
using Model;
using System.Data;

namespace DAL
{
    public class UserInfoDAL
    {
        public int ThirdPartyLogin(string openId, string unionId, string token, string authorizedTypeId, string appId, LogInfoModel logInfo)
        {
            return 0;
        }

        #region 根据openid+unionid判断用户是否存在
        /// <summary>
        /// 根据openid+unionid判断用户是否存在
        /// </summary>
        /// <param name="openId"></param>
        /// <param name="unionId"></param>
        /// <returns></returns>
        public bool CheckUserExist(string openId, string unionId)
        {
            bool res = false;
            try
            {
                string sql = "SELECT id FROM user WHERE openId=@OpenId AND unionId=@UnionId;";
                MySqlParameter[] param =
                {
                    new MySqlParameter("@OpenId",openId),
                    new MySqlParameter("@UnionId",unionId)
                };

                res = MySqlHelper.ExecuteScalar(ConfigHelper.ConnStr, sql, param).ToInt() > 0;

            }
            catch (Exception ex)
            {
                LogHelper.Error(ex);
            }
            return res;
        }
        #endregion

        #region 根据userid 获取用户信息
        public UserModel GetByUserId(string userId)
        {
            try
            {
                string sql = @"SELECT * FROM `user` WHERE userId=@UserId;";
                DataRow dr = MySqlHelper.ExecuteDataRow(ConfigHelper.ConnStr, sql, new MySqlParameter("@UserId", userId));
                return EntityToModel(dr);
            }
            catch (Exception ex)
            {
                LogHelper.Error(ex);
            }
            return null;
        }
        #endregion

        #region 根据openid和unionid获取用户
        public UserModel GetByOpenIdAndUnionId(string openId, string unionId)
        {
            try
            {
                string sql = @"SELECT * FROM `user` WHERE `openId`=@OpenId AND `unionId`=@UnionId ;";
                MySqlParameter[] param =
                {
                    new MySqlParameter("@OpenId",openId),
                    new MySqlParameter("@UnionId",unionId),
                };
                return EntityToModel(MySqlHelper.ExecuteDataRow(ConfigHelper.ConnStr, sql, param));
            }
            catch (Exception ex)
            {
                LogHelper.Error(ex);
            }
            return null;
        } 
        #endregion

        #region 根据id获取用户
        public UserModel GetById(int id)
        {
            try
            {
                string sql = @"SELECT * FROM `user` WHERE id=@Id;";
                return EntityToModel(MySqlHelper.ExecuteDataRow(ConfigHelper.ConnStr, sql, new MySqlParameter("@Id", id)));
            }
            catch (Exception ex)
            {
                LogHelper.Error(ex);
            }
            return null;
        }
        #endregion

        #region 根据手机号获取用户
        public UserModel GetByMobile(string mobileNum)
        {
            try
            {
                string sql = "SELECT * FROM `user` WHERE mobileNum = @MobileNum;";
                return EntityToModel(MySqlHelper.ExecuteDataRow(ConfigHelper.ConnStr, sql, new MySqlParameter("@MobileNum", mobileNum)));
            }
            catch (Exception ex)
            {
                LogHelper.Error(ex);
            }
            return null;
        } 
        #endregion

        #region 根据token获取用户信息
        public UserModel GetByToken(string token)
        {
            try
            {
                string sql = @"SELECT * FROM `user` WHERE userToken=@Token;";
                return EntityToModel(MySqlHelper.ExecuteDataRow(ConfigHelper.ConnStr, sql, new MySqlParameter("@Token", token)));
            }
            catch (Exception ex)
            {
                LogHelper.Error(ex);
            }
            return null;
        }

        #endregion
        #region 插入用户
        public int Insert(UserModel user)
        {
            int id = 0;
            try
            {
                string sql = @"INSERT INTO `user` (`userId`,`nickName`,`userAvatar`,`email`,`mobileNum`,`password`,`salt`,`userToken`,`tokenUpdateTime`,`tokenIsWork`,`authorizedTypeId`,`openId`,`unionId`,`extra`,`registerTime`,`createTime`,`updateTime`,`createIP`,`systemTypeId`,`equipmentNum`,`isDel`)
VALUES(@UserId,@NickName,@UserAvatar,@Email,@MobileNum,@Password,@Salt,@UserToken,@TokenUpdateTime,@TokenIsWork,@AuthorizedTypeId,@OpenId,@UnionId,@Extra,NOW(),NOW(),NOW(),@CreateIP,@SystemTypeId,@EquipmentNum,@IsDel);@@SELECT IDENTITY;";

                MySqlParameter[] param =
                {
                    new MySqlParameter("@UserId",user.UserId),
                    new MySqlParameter("@NickName",user.NickName),
                    new MySqlParameter("@UserAvatar",user.UserAvatar),
                    new MySqlParameter("@Email",user.Email),
                    new MySqlParameter("@MobileNum",user.MobileNum),
                    new MySqlParameter("@Password",EncryptHelper.MD5Encrypt(user.Password + ConfigHelper.Salt)),
                    new MySqlParameter("@Salt",ConfigHelper.Salt),
                    new MySqlParameter("@UserToken",user.UserToken),
                    new MySqlParameter("@TokenUpdateTime",user.TokenUpdateTime),
                    new MySqlParameter("@TokenIsWork",user.TokenIsWork),
                    new MySqlParameter("@AuthorizedTypeId",user.AuthorizedTypeId),
                    new MySqlParameter("@OpenId",user.OpenId),
                    new MySqlParameter("@UnionId",user.UnionId),
                    new MySqlParameter("@Extra",user.Extra),
                    new MySqlParameter("@CreateIP",user.CreateIP),
                    new MySqlParameter("@SystemTypeId",user.SystemTypeId),
                    new MySqlParameter("@EquipmentNum",user.EquipmentNum),
                    new MySqlParameter("@IsDel",user.IsDel)
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

        #region 更新用户
        public bool Update(UserModel user)
        {
            bool res = false;
            try
            {
                string sql = @"UPDATE `user` SET `realName`=@RealName,`idCard`=@IdCard,`userAvatar`=@UserAvatar,`email`=@Email,`mobileNum`=@MobileNum,`password`=@Password,`salt`=@Salt,`userToken`=@UserToken,`tokenCreateTime`=NOW(),`tokenIsWork`=@TokenIsWork,`extra`=@Extra,`updateTime`=NOW();";
                MySqlParameter[] param =
                {
                    new MySqlParameter("@RealName",user.RealName),
                    new MySqlParameter("@IdCard",user.IDCard),
                    new MySqlParameter("@UserAvatar",user.UserAvatar),
                    new MySqlParameter("@Email",user.Email),
                    new MySqlParameter("@MobileNum",user.MobileNum),
                    new MySqlParameter("@Password",user.Password),
                    new MySqlParameter("@Salt",user.Salt),
                    new MySqlParameter("@UserToken",GUIDHelper.GenerateGUID()),
                    new MySqlParameter("@TokenIsWork",user.TokenIsWork),
                    new MySqlParameter("@Extra",user.Extra),
                };
                res = MySqlHelper.ExecuteNonQuery(ConfigHelper.ConnStr, sql, param) > 0;
            }
            catch (Exception ex)
            {
                LogHelper.Error(ex);
            }
            return res;
        }
        #endregion

        #region 删除用户
        public bool Delete(string UserId)
        {
            bool res = false;
            try
            {
                string sql = "UPDATE `user` SET isDEL=1 WHERE userId=@UserId;";
                res = MySqlHelper.ExecuteNonQuery(ConfigHelper.ConnStr, sql, new MySqlParameter("@User", UserId)) > 0;
            }
            catch (Exception ex)
            {
                LogHelper.Error(ex);
            }
            return res;
        } 
        #endregion

        #region 实体类转换
        private UserModel EntityToModel(DataRow dr)
        {
            if (dr != null)
            {
                UserModel model = new UserModel();
                if (dr.Table.Columns.Contains("id"))
                    model.Id = dr["id"].ToInt();
                if (dr.Table.Columns.Contains("userId"))
                    model.UserId = dr["userId"].To<string>();
                if (dr.Table.Columns.Contains("nickName"))
                    model.NickName = dr["nickName"].To<string>();
                if (dr.Table.Columns.Contains("realName"))
                    model.RealName = dr["realName"].To<string>();
                if (dr.Table.Columns.Contains("idCard"))
                    model.IDCard = dr["idCard"].To<string>();
                if (dr.Table.Columns.Contains("userAvatar"))
                    model.UserAvatar = dr["userAvatar"].To<string>();
                if (dr.Table.Columns.Contains("email"))
                    model.Email = dr["email"].To<string>();
                if (dr.Table.Columns.Contains("mobileNum"))
                    model.MobileNum = dr["mobileNum"].To<string>();
                if (dr.Table.Columns.Contains("password"))
                    model.Password = dr["password"].To<string>();
                if (dr.Table.Columns.Contains("salt"))
                    model.Salt = dr["salt"].To<string>();
                if (dr.Table.Columns.Contains("userToken"))
                    model.UserToken = dr["userToken"].To<string>();
                if (dr.Table.Columns.Contains("tokenCreateTime"))
                    model.TokenUpdateTime = dr["tokenCreateTime"].To<DateTime>();
                if (dr.Table.Columns.Contains("tokenIsWork"))
                    model.TokenIsWork = dr["tokenIsWork"].ToInt();
                //if (dr.Table.Columns.Contains("appId"))
                //    model.AppId = dr["appId"].ToInt();
                if (dr.Table.Columns.Contains("authorizedTypeId"))
                    model.AuthorizedTypeId = dr["authorizedTypeId"].ToInt();
                if (dr.Table.Columns.Contains("openId"))
                    model.OpenId = dr["openId"].To<string>();
                if (dr.Table.Columns.Contains("unionId"))
                    model.UnionId = dr["unionId"].To<string>();
                if (dr.Table.Columns.Contains("extra"))
                    model.Extra = dr["extra"].To<string>();
                if (dr.Table.Columns.Contains("registerTime"))
                    model.RegisterTime = dr["registerTime"].To<DateTime>();
                if (dr.Table.Columns.Contains("createTime"))
                    model.CreateTime = dr["createTime"].To<DateTime>();
                if (dr.Table.Columns.Contains("updateTime"))
                    model.UpdateTime = dr["UpdateTime"].To<DateTime>();
                if (dr.Table.Columns.Contains("createIP"))
                    model.CreateIP = dr["createIP"].To<string>();
                if (dr.Table.Columns.Contains("systemTypeId"))
                    model.SystemTypeId = dr["systemTypeId"].ToInt();
                if (dr.Table.Columns.Contains("equipmentNum"))
                    model.EquipmentNum = dr["equipmentNum"].To<string>();
                if (dr.Table.Columns.Contains("isDel"))
                    model.IsDel = dr["isDel"].ToInt();
                return model;
            }
            return null;
        } 
        #endregion
    }
}
