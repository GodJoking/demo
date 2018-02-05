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
    public class UserInfoController : BaseController
    {
        private UserInfoBLL userBLL = new UserInfoBLL();
        private SMSCodeBLL smsCodeBLL = new SMSCodeBLL();

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
        public BaseModel ThirdPartyLogin(int systemTypeId, string equipmentNum, string openId, string unionId, int authorizedTypeId)
        {
            return userBLL.ThirdPartyLogin(systemTypeId, equipmentNum, openId, unionId, authorizedTypeId);
        }
        #endregion

        #region 绑定新手机号
        /// <summary>
        /// 绑定新手机号
        /// </summary>
        /// <param name="token"></param>
        /// <param name="mobileNum"></param>
        /// <param name="password"></param>
        /// <param name="checkCode"></param>
        /// <returns></returns>
        [HttpPost]
        public BaseModel BindUser(string token, string mobileNum, string password, string checkCode)
        {
            return userBLL.BindUser(token, mobileNum, password, checkCode);
        } 
        #endregion

        public BaseModel CompleteInfo(string token, UserModel user)
        {
            return null;
        }

        #region 换绑定手机号
        public BaseModel ResetMobile(string token, string password, string mobileNum, string checkCode)
        {
            return null;
        } 
        #endregion

        #region 登出
        /// <summary>
        /// 登出
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public BaseModel LogOut(string token)
        {
            return userBLL.LogOut(token);
        } 
        #endregion

        #region 发送验证码
        /// <summary>
        /// 发送验证码
        /// </summary>
        /// <param name="mobileNum">手机号</param>
        /// <returns></returns>
        [HttpPost]
        public RESTfulModel SendCode(string mobileNum)
        {
            return smsCodeBLL.GetSMSCode(mobileNum, RequestIP);
        }
        #endregion

        #region Token登录
        /// <summary>
        /// Token登录
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        [HttpPost]
        public BaseModel TokenLogin(string token)
        {
            return null;
        } 
        #endregion

        public BaseModel ValidateToken(string token)
        {
            return null;
        }
    }
}
