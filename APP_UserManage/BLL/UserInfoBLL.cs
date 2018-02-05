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
        private SMSCodeDAL smsCodeDAL = new SMSCodeDAL();
        private LoginLogDAL loginDAL = new LoginLogDAL();

        #region 第三方登录
        public BaseModel ThirdPartyLogin(int systemTypeId, string equipmentNum, string openId, string unionId, int authorizedTypeId)
        {
            BaseModel resmodel = new BaseModel();

            if (openId != null && unionId != null)
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
                catch (Exception ex)
                {
                    resmodel.Code = (int)CodeEnum.系统异常;
                    resmodel.Msg = "系统异常";
                    resmodel.Data = null;
                    resmodel.Desc = "系统异常";
                    LogHelper.Error(ex);
                }

            }
            else
            {
                resmodel.Code = (int)CodeEnum.OpenId不能为空;
                resmodel.Msg = "OpenId不能为空";
                resmodel.Data = null;
                resmodel.Desc = "OpenId不能为空";

            }
            return resmodel;
        }
        #endregion



        #region 获取短信验证码
        public BaseModel GetSMSCode(string mobileNum, string requestIP)
        {
            try
            {
                #region 非数据库端验证mobileNum
                BaseModel cv_MobileNum = ClientValidateMobileNum(mobileNum);
                if (cv_MobileNum.Code != 0)
                {
                    return cv_MobileNum;
                }
                #endregion
                
                DateTime dt = DateTime.Now;

                #region 添加smscode
                string codeValue;
                smsCodeDAL.Insert(mobileNum, dt, out codeValue);

                return new BaseModel() { Code = (int)CodeEnum.成功, Msg = string.Format(CodeMsgDAL.GetByCode((int)CodeEnum.成功), "成功"), Data = codeValue};
                #endregion
            }
            catch (Exception ex)
            {
                LogHelper.Error(ex);
                return new BaseModel() { Code = (int)CodeEnum.系统异常, Msg = CodeMsgDAL.GetByCode((int)CodeEnum.系统异常) };
            }
        }
        #endregion

        #region 绑定新手机号
        public BaseModel BindUser(string token, string mobileNum, string smsCode, string password)
        {
            try
            {
                #region 非数据库端验证token
                BaseModel cv_Token = ClientValidateToken(token);
                if (cv_Token.Code != 0)
                {
                    return cv_Token;
                }
                #endregion

                #region 非数据库端验证mobileNum
                BaseModel cv_MobileNum = ClientValidateMobileNum(mobileNum);
                if (cv_MobileNum.Code != 0)
                {
                    return cv_MobileNum;
                }
                #endregion

                #region 非数据库端验证smsCode
                BaseModel cv_SMSCode = ClientValidateSMSCode(smsCode);
                if (cv_SMSCode.Code != 0)
                {
                    return cv_SMSCode;
                }
                #endregion

                #region 非数据库端验证password
                BaseModel cv_Password = ClientValidatePassword(password);
                if (cv_Password.Code != 0)
                {
                    return cv_Password;
                }
                #endregion

                UserModel TokenModel = userDAL.GetByToken(token);
                #region 数据库端验证token
                BaseModel sv_Token = ServerValidateToken(TokenModel);
                if (sv_Token.Code != 0)
                {
                    return sv_Token;
                }
                #endregion

                DateTime dt = DateTime.Now;
                DateTime dt2 = dt.AddMinutes(0 - ConfigHelper.SMSTime);

                #region 数据库端验证smsCode
                BaseModel sv_SMSCode = ServerValidateSMSCode(smsCode, mobileNum, dt2);
                if (sv_SMSCode.Code != 0)
                {
                    return sv_SMSCode;
                }
                #endregion

                UserModel userModel = userDAL.GetByMobile(mobileNum);
                #region 数据库端验证mobileNum
                BaseModel sv_MobileNum = ServerValidateMobileNum(userModel);
                if (sv_MobileNum.Code != 0)
                {
                    return sv_MobileNum;
                }
                #endregion

                #region 更新用户手机号和密码

                TokenModel.MobileNum = mobileNum;
                TokenModel.Password = EncryptHelper.MD5Encrypt(password + ConfigHelper.Salt);
                if (userDAL.Update(TokenModel))
                {
                    return new BaseModel() { Code = (int)CodeEnum.成功, Msg = string.Format(CodeMsgDAL.GetByCode((int)CodeEnum.成功)) };
                }
                else
                {
                    return new BaseModel() { Code = (int)CodeEnum.失败, Msg = string.Format(CodeMsgDAL.GetByCode((int)CodeEnum.失败)) };
                }
                #endregion


            }
            catch (Exception ex)
            {
                LogHelper.Error(ex);
                return new BaseModel() { Code = (int)CodeEnum.系统异常, Msg = CodeMsgDAL.GetByCode((int)CodeEnum.系统异常) };
            }
        }
        #endregion

        #region 绑定新手机号
        public BaseModel ResetMobile(string token, string password, string mobileNum, string smsCode)
        {
            try
            {
                #region 非数据库端验证token
                BaseModel cv_Token = ClientValidateToken(token);
                if (cv_Token.Code != 0)
                {
                    return cv_Token;
                }
                #endregion

                #region 非数据库端验证mobileNum
                BaseModel cv_MobileNum = ClientValidateMobileNum(mobileNum);
                if (cv_MobileNum.Code != 0)
                {
                    return cv_MobileNum;
                }
                #endregion

                #region 非数据库端验证smsCode
                BaseModel cv_SMSCode = ClientValidateSMSCode(smsCode);
                if (cv_SMSCode.Code != 0)
                {
                    return cv_SMSCode;
                }
                #endregion

                #region 非数据库端验证password
                BaseModel cv_Password = ClientValidatePassword(password);
                if (cv_Password.Code != 0)
                {
                    return cv_Password;
                }
                #endregion

                UserModel TokenModel = userDAL.GetByToken(token);
                #region 数据库端验证token
                BaseModel sv_Token = ServerValidateToken(TokenModel);
                if (sv_Token.Code != 0)
                {
                    return sv_Token;
                }
                #endregion

                DateTime dt = DateTime.Now;
                DateTime dt2 = dt.AddMinutes(0 - ConfigHelper.SMSTime);

                #region 数据库端验证smsCode
                BaseModel sv_SMSCode = ServerValidateSMSCode(smsCode, mobileNum, dt2);
                if (sv_SMSCode.Code != 0)
                {
                    return sv_SMSCode;
                }
                #endregion

                UserModel userModel = userDAL.GetByMobile(mobileNum);
                #region 数据库端验证mobileNum
                BaseModel sv_MobileNum = ServerValidateMobileNum(userModel);
                if (sv_MobileNum.Code != 0)
                {
                    return sv_MobileNum;
                }
                #endregion

                #region 更新用户手机号
                
                if(TokenModel.Password != EncryptHelper.MD5Encrypt(password + ConfigHelper.Salt))
                {
                    return new BaseModel() { Code = (int)CodeEnum.密码错误, Msg = string.Format(CodeMsgDAL.GetByCode((int)CodeEnum.密码错误)) };
                }
                TokenModel.MobileNum = mobileNum;
                
                if (userDAL.Update(TokenModel))
                {
                    return new BaseModel() { Code = (int)CodeEnum.成功, Msg = string.Format(CodeMsgDAL.GetByCode((int)CodeEnum.成功)) };
                }
                else
                {
                    return new BaseModel() { Code = (int)CodeEnum.失败, Msg = string.Format(CodeMsgDAL.GetByCode((int)CodeEnum.失败)) };
                }
                #endregion


            }
            catch (Exception ex)
            {
                LogHelper.Error(ex);
                return new BaseModel() { Code = (int)CodeEnum.系统异常, Msg = CodeMsgDAL.GetByCode((int)CodeEnum.系统异常) };
            }
        }
        #endregion

        #region 验证短信验证码
        public BaseModel ValidateSMSCode(string mobileNum, string smsCode, string requestIP)
        {
            try
            {
                #region 非数据库端验证mobileNum
                BaseModel cv_MobileNum = ClientValidateMobileNum(mobileNum);
                if (cv_MobileNum.Code != 0)
                {
                    return cv_MobileNum;
                }
                #endregion
                #region 非数据库端验证smsCode
                BaseModel cv_SMSCode = ClientValidateSMSCode(smsCode);
                if (cv_SMSCode.Code != 0)
                {
                    return cv_SMSCode;
                }
                #endregion

                #region 数据库端验证appId
                //BaseModel sv_AppId = ServerValidateAppId(appId, requestIP);
                //if (sv_AppId.Code != 0)
                //{
                //    return sv_AppId;
                //}
                #endregion

                DateTime dt = DateTime.Now;
                DateTime dt2 = dt.AddMinutes(0 - ConfigHelper.SMSTime);

                #region 数据库端验证smsCode
                SMSCodeModel smsCodeModel = smsCodeDAL.GetByMobileNum(mobileNum, dt2);
                if (smsCodeModel == null)
                {
                    return new BaseModel() { Code = (int)CodeEnum.短信验证码无效, Msg = CodeMsgDAL.GetByCode((int)CodeEnum.短信验证码无效) };
                }
                if (smsCodeModel.UsedCount >= 3)
                {
                    return new BaseModel() { Code = (int)CodeEnum.短信验证码3次输入错误失效, Msg = CodeMsgDAL.GetByCode((int)CodeEnum.短信验证码3次输入错误失效) };
                }

                if (smsCodeModel.CodeValue == smsCode)
                {
                    return new BaseModel() { Code = (int)CodeEnum.成功, Msg = string.Format(CodeMsgDAL.GetByCode((int)CodeEnum.成功), "成功") };
                }
                else
                {
                    smsCodeDAL.ValidateFail(smsCodeModel.Id);
                    return new BaseModel() { Code = (int)CodeEnum.短信验证码无效, Msg = CodeMsgDAL.GetByCode((int)CodeEnum.短信验证码无效) };
                }
                #endregion
            }
            catch (Exception ex)
            {
                LogHelper.Error(ex);
                return new BaseModel() { Code = (int)CodeEnum.系统异常, Msg = CodeMsgDAL.GetByCode((int)CodeEnum.系统异常) };
            }
        } 
        #endregion

        #region 登出
        public BaseModel LogOut(string token)
        {
            try
            {
                #region 非数据库端验证token
                BaseModel cv_Token = ClientValidateToken(token);
                if (cv_Token.Code != 0)
                {
                    return cv_Token;
                }
                #endregion

                UserModel TokenModel = userDAL.GetByToken(token);
                #region 数据库端验证token
                BaseModel sv_Token = ServerValidateToken(TokenModel);
                if (sv_Token.Code != 0)
                {
                    return sv_Token;
                }
                #endregion

                userDAL.Update(TokenModel);
                return new BaseModel() { Code = (int)CodeEnum.成功, Msg = string.Format(CodeMsgDAL.GetByCode((int)CodeEnum.成功), "成功") };
            }
            catch (Exception ex)
            {
                LogHelper.Error(ex);
                return new BaseModel() { Code = (int)CodeEnum.系统异常, Msg = CodeMsgDAL.GetByCode((int)CodeEnum.系统异常) };
            }
        }
        #endregion

        #region 完善用户信息
        public BaseModel CompleteUserInfo(string token, OutUserModel model)
        {
            #region 非数据库端验证token
            BaseModel cv_Token = ClientValidateToken(token);
            if (cv_Token.Code != 0)
            {
                return cv_Token;
            }
            #endregion

            UserModel TokenModel = userDAL.GetByToken(token);
            #region 数据库端验证token
            BaseModel sv_Token = ServerValidateToken(TokenModel);
            if (sv_Token.Code != 0)
            {
                return sv_Token;
            }
            #endregion
            if (token == model.Token)
            {
                userDAL.UpdateUserInfo(model);
            }
            else
            {
                return new BaseModel() { Code = (int)CodeEnum.Token无效, Msg = "请求的Token和要修改的用户Token不一致" };
            }
            return new BaseModel() { Code = (int)CodeEnum.成功, Msg = CodeMsgDAL.GetByCode((int)CodeEnum.成功) };
        } 
        #endregion

        #region Token登录
        public BaseModel TokenLogin(string token)
        {
            #region 非数据库端验证token
            BaseModel cv_Token = ClientValidateToken(token);
            if (cv_Token.Code != 0)
            {
                return cv_Token;
            }
            #endregion

            UserModel TokenModel = userDAL.GetByToken(token);
            #region 数据库端验证token
            BaseModel sv_Token = ServerValidateToken(TokenModel);
            if (sv_Token.Code != 0)
            {
                return sv_Token;
            }
            #endregion
            return new BaseModel() { Code = (int)CodeEnum.成功, Msg = CodeMsgDAL.GetByCode((int)CodeEnum.成功), Data = TokenModel };
        } 
        #endregion







        private BaseModel ClientValidateMobileNum(string mobileNum)
        {
            #region 非数据库端验证mobileNum
            if (ValidateHelper.IsNullOrEmpty(mobileNum))
            {
                return new BaseModel() { Code = (int)CodeEnum.手机号不能为空, Msg = CodeMsgDAL.GetByCode((int)CodeEnum.手机号不能为空) };
            }
            if (!ValidateHelper.MobileNum(mobileNum))
            {
                return new BaseModel() { Code = (int)CodeEnum.手机号格式错误, Msg = CodeMsgDAL.GetByCode((int)CodeEnum.手机号格式错误) };
            }
            #endregion

            return new BaseModel() { Code = (int)CodeEnum.成功, Msg = string.Format(CodeMsgDAL.GetByCode((int)CodeEnum.成功), "成功") };
        }

        private BaseModel ServerValidateMobileNum(UserModel userModel)
        {
            #region 数据库端验证mobileNum
            if (userModel != null)
            {
                return new BaseModel() { Code = (int)CodeEnum.手机号已注册, Msg = string.Format(CodeMsgDAL.GetByCode((int)CodeEnum.手机号已注册), "") };
            }
            #endregion

            return new BaseModel() { Code = (int)CodeEnum.成功, Msg = string.Format(CodeMsgDAL.GetByCode((int)CodeEnum.成功), "成功") };
        }

        //private BaseModel ServerValidateAppId(int appId, string requestIP)
        //{
        //    #region 数据库端验证appId
        //    AppModel appModel = appModelList.Where(o => o.Id == appId).FirstOrDefault();
        //    if (appModel == null)
        //    {
        //        return new BaseModel() { Code = (int)CodeEnum.AppId错误, Msg = CodeMsgDAL.GetByCode((int)CodeEnum.AppId错误) };
        //    }
        //    string[] appModel_IPSet = appModel.IPSet.Split(',');
        //    if (!appModel_IPSet.Contains(requestIP ?? "127.0.0.1"))
        //    {
        //        return new BaseModel() { Code = (int)CodeEnum.AppId错误, Msg = CodeMsgDAL.GetByCode((int)CodeEnum.服务器拒绝此IP请求) };
        //    }
        //    #endregion

        //    return new BaseModel() { Code = (int)CodeEnum.成功, Msg = string.Format(CodeMsgDAL.GetByCode((int)CodeEnum.成功), "成功") };
        //}

        private BaseModel ClientValidateSMSCode(string smsCode)
        {
            #region 非数据库端验证smsCode
            if (ValidateHelper.IsNullOrEmpty(smsCode))
            {
                return new BaseModel() { Code = (int)CodeEnum.短信验证码不能为空, Msg = CodeMsgDAL.GetByCode((int)CodeEnum.短信验证码不能为空) };
            }
            if (!ValidateHelper.SMSCode(smsCode))
            {
                return new BaseModel() { Code = (int)CodeEnum.短信验证码格式错误, Msg = CodeMsgDAL.GetByCode((int)CodeEnum.短信验证码格式错误) };
            }
            #endregion

            return new BaseModel() { Code = (int)CodeEnum.成功, Msg = string.Format(CodeMsgDAL.GetByCode((int)CodeEnum.成功), "成功") };
        }

        private BaseModel ServerValidateSMSCode(string smsCode, string mobileNum, DateTime dt2)
        {
            #region 数据库端验证smsCode
            SMSCodeModel smsCodeModel = smsCodeDAL.GetByMobileNum(mobileNum, dt2);
            if (smsCodeModel == null)
            {
                return new BaseModel() { Code = (int)CodeEnum.短信验证码无效, Msg = CodeMsgDAL.GetByCode((int)CodeEnum.短信验证码无效) };
            }
            if (smsCodeModel.UsedCount >= 3)
            {
                return new BaseModel() { Code = (int)CodeEnum.短信验证码3次输入错误失效, Msg = CodeMsgDAL.GetByCode((int)CodeEnum.短信验证码3次输入错误失效) };
            }
            if (smsCodeModel.CodeValue != smsCode)
            {
                smsCodeDAL.ValidateFail(smsCodeModel.Id);
                return new BaseModel() { Code = (int)CodeEnum.短信验证码无效, Msg = CodeMsgDAL.GetByCode((int)CodeEnum.短信验证码无效) };
            }
            #endregion

            return new BaseModel() { Code = (int)CodeEnum.成功, Msg = string.Format(CodeMsgDAL.GetByCode((int)CodeEnum.成功), "成功") };
        }

        private BaseModel ClientValidateUnionUserId(string unionUserId)
        {
            #region 非数据库端验证unionUserId
            if (ValidateHelper.IsNullOrEmpty(unionUserId))
            {
                return new BaseModel() { Code = (int)CodeEnum.UnionUserId不能为空, Msg = CodeMsgDAL.GetByCode((int)CodeEnum.UnionUserId不能为空) };
            }
            if (!ValidateHelper.Guid(unionUserId))
            {
                return new BaseModel() { Code = (int)CodeEnum.UnionUserId格式错误, Msg = CodeMsgDAL.GetByCode((int)CodeEnum.UnionUserId格式错误) };
            }
            #endregion

            return new BaseModel() { Code = (int)CodeEnum.成功, Msg = string.Format(CodeMsgDAL.GetByCode((int)CodeEnum.成功), "成功") };
        }

        private BaseModel ClientValidateToken(string token)
        {
            #region 非数据库端验证token
            if (ValidateHelper.IsNullOrEmpty(token))
            {
                return new BaseModel() { Code = (int)CodeEnum.Token不能为空, Msg = CodeMsgDAL.GetByCode((int)CodeEnum.Token不能为空) };
            }
            if (!ValidateHelper.Guid(token))
            {
                return new BaseModel() { Code = (int)CodeEnum.Token格式错误, Msg = CodeMsgDAL.GetByCode((int)CodeEnum.Token格式错误) };
            }
            #endregion

            return new BaseModel() { Code = (int)CodeEnum.成功, Msg = string.Format(CodeMsgDAL.GetByCode((int)CodeEnum.成功), "成功") };
        }

        private BaseModel ServerValidateToken(UserModel tokenModel)
        {
            #region 数据库端验token
            if (tokenModel == null || tokenModel.TokenIsWork == 0)
            {
                return new BaseModel() { Code = (int)CodeEnum.Token无效, Msg = CodeMsgDAL.GetByCode((int)CodeEnum.Token无效) };
            }
            if ((DateTime.Now - tokenModel.TokenUpdateTime).Days > 29)
            {
                return new BaseModel() { Code = (int)CodeEnum.Token已过期, Msg = CodeMsgDAL.GetByCode((int)CodeEnum.Token已过期) };
            }
            #endregion

            return new BaseModel() { Code = (int)CodeEnum.成功, Msg = string.Format(CodeMsgDAL.GetByCode((int)CodeEnum.成功), "成功") };
        }

        private BaseModel ClientValidatePassword(string password)
        {
            #region 非数据库端验证password
            if (ValidateHelper.IsNullOrEmpty(password))
            {
                return new BaseModel() { Code = (int)CodeEnum.密码不能为空, Msg = CodeMsgDAL.GetByCode((int)CodeEnum.密码不能为空) };
            }
            if (string.IsNullOrWhiteSpace(password) || password.Length != 32)
            {
                return new BaseModel() { Code = (int)CodeEnum.密码格式错误, Msg = CodeMsgDAL.GetByCode((int)CodeEnum.密码格式错误) };
            }
            #endregion

            return new BaseModel() { Code = (int)CodeEnum.成功, Msg = string.Format(CodeMsgDAL.GetByCode((int)CodeEnum.成功), "成功") };
        }

        private BaseModel ClientValidateIP(string ip)
        {
            #region 非数据库端验证ip
            if (ValidateHelper.IsNullOrEmpty(ip))
            {
                return new BaseModel() { Code = (int)CodeEnum.IP不能为空, Msg = CodeMsgDAL.GetByCode((int)CodeEnum.IP不能为空) };
            }
            if (!ValidateHelper.IP(ip))
            {
                return new BaseModel() { Code = (int)CodeEnum.IP格式错误, Msg = CodeMsgDAL.GetByCode((int)CodeEnum.IP格式错误) };
            }
            #endregion

            return new BaseModel() { Code = (int)CodeEnum.成功, Msg = string.Format(CodeMsgDAL.GetByCode((int)CodeEnum.成功), "成功") };
        }

        private BaseModel ClientValidateSystemTypeId(int systemTypeId)
        {
            #region 非数据库端验证systemTypeId
            if (!ValidateHelper.SystemTypeId(systemTypeId))
            {
                return new BaseModel() { Code = (int)CodeEnum.设备系统类型错误, Msg = CodeMsgDAL.GetByCode((int)CodeEnum.设备系统类型错误) };
            }
            #endregion

            return new BaseModel() { Code = (int)CodeEnum.成功, Msg = string.Format(CodeMsgDAL.GetByCode((int)CodeEnum.成功), "成功") };
        }

        private BaseModel ClientValidateEquipmentNum(ref string equipmentNum)
        {
            #region 非数据库端验证equipmentNum
            equipmentNum = equipmentNum == null ? "" : equipmentNum.Trim();
            if (!ValidateHelper.EquipmentNum(equipmentNum))
            {
                return new BaseModel() { Code = (int)CodeEnum.设备号格式错误, Msg = CodeMsgDAL.GetByCode((int)CodeEnum.设备号格式错误) };
            }
            #endregion

            return new BaseModel() { Code = (int)CodeEnum.成功, Msg = string.Format(CodeMsgDAL.GetByCode((int)CodeEnum.成功), "成功") };
        }

        private BaseModel ClientValidateOpenId(string openId)
        {
            #region 非数据库端验证openId
            if (ValidateHelper.IsNullOrEmpty(openId))
            {
                return new BaseModel() { Code = (int)CodeEnum.OpenId不能为空, Msg = CodeMsgDAL.GetByCode((int)CodeEnum.OpenId不能为空) };
            }
            #endregion

            return new BaseModel() { Code = (int)CodeEnum.成功, Msg = string.Format(CodeMsgDAL.GetByCode((int)CodeEnum.成功), "成功") };
        }

        private BaseModel ClientValidateUnionId(string unionId)
        {
            #region 非数据库端验证unionId
            if (ValidateHelper.IsNullOrEmpty(unionId))
            {
                return new BaseModel() { Code = (int)CodeEnum.UnionId不能为空, Msg = CodeMsgDAL.GetByCode((int)CodeEnum.UnionId不能为空) };
            }
            #endregion

            return new BaseModel() { Code = (int)CodeEnum.成功, Msg = string.Format(CodeMsgDAL.GetByCode((int)CodeEnum.成功), "成功") };
        }

        private BaseModel ClientValidateAuthorizedTypeId(int authorizedTypeId)
        {
            #region 非数据库端验证authorizedTypeId
            if (!ValidateHelper.AuthorizedTypeId(authorizedTypeId))
            {
                return new BaseModel() { Code = (int)CodeEnum.AuthorizedTypeId错误, Msg = CodeMsgDAL.GetByCode((int)CodeEnum.AuthorizedTypeId错误) };
            }
            #endregion

            return new BaseModel() { Code = (int)CodeEnum.成功, Msg = string.Format(CodeMsgDAL.GetByCode((int)CodeEnum.成功), "成功") };
        }
    }
}
