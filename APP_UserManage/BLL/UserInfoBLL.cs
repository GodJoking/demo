using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using DAL;
using Model.Enum;
using Helper;

namespace BLL
{
    public class UserInfoBLL
    {
        private UserInfoDAL userDAL = new UserInfoDAL();

        public BaseModel<UserModel> ThirdPartyLogin(int systemTypeId, string equipmentNum, string openId, string unionId, int authorizedTypeId)
        {
            BaseModel<UserModel> resmodel = new BaseModel<UserModel>();

            if(openId != null && unionId != null)
            {
                try
                {
                    UserModel user = userDAL.GetByOpenIdAndUnionId(openId, unionId);
                    if (user != null)
                    {
                        resmodel.Code = (int)CodeEnum.成功;
                        resmodel.Msg = "成功";
                        resmodel.Data = userDAL.GetByOpenIdAndUnionId(openId, unionId);
                        resmodel.Desc = "成功";
                    }
                    else
                    {
                        user.SystemTypeId = systemTypeId;
                        user.EquipmentNum = equipmentNum;
                        user.OpenId = openId;
                        user.UnionId = unionId;
                        user.AuthorizedTypeId = authorizedTypeId;

                        resmodel.Code = (int)CodeEnum.成功;
                        resmodel.Msg = "成功";
                        resmodel.Data = userDAL.GetById(userDAL.Insert(user));
                        resmodel.Desc = "成功";

                    }
                }
                catch(Exception ex)
                {
                    resmodel.Code = (int)CodeEnum.系统异常;
                    resmodel.Msg = "系统异常";
                    resmodel.Data = null;
                    resmodel.Desc = "系统异常";
                    LogHelper.Info("李绪东", ex.ToString());
                }
                
            }
            else
            {
                resmodel.Code = (int)CodeEnum.OpenId或UnionId不能为空;
                resmodel.Msg = "OpenId或UnionId不能为空";
                resmodel.Data = null;
                resmodel.Desc = "OpenId或UnionId不能为空";

            }
            return resmodel;
        } 

    }
}
