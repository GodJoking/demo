using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Model;
using BLL;

namespace APP_UserManage.Controllers
{
    /// <summary>
    /// 用户管理中心
    /// </summary>
    public class UserInfoController : BaseController
    {
        private UserInfoBLL userBLL = new UserInfoBLL();

        #region 第三方微信登录
        /// <summary>
        /// 第三方微信登录
        /// </summary>
        /// <param name="systemTypeId">系统类型id</param>
        /// <param name="equipmentNum">设备号</param>
        /// <param name="openId">用户微信openId</param>
        /// <param name="unionId">unionid</param>
        /// <param name="authorizedTypeId">权限类型id</param>
        /// <returns></returns>
        [HttpPost]
        public BaseModel<UserModel> ThirdPartyLogin(int systemTypeId, string equipmentNum, string openId, string unionId, int authorizedTypeId)
        {
            return userBLL.ThirdPartyLogin(systemTypeId, equipmentNum, openId, unionId, authorizedTypeId);
        } 
        #endregion


        public BaseModel<UserModel> BindMobile()
        {
            return null;
        }


    }
}
