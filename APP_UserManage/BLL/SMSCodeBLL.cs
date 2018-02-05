using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using Model;
using Model.Enum;
using Helper;

namespace BLL
{
    public class SMSCodeBLL
    {
        SMSCodeDAL smsCodeDAL = new SMSCodeDAL();
        
        public RESTfulModel GetSMSCode(string mobileNum, string requestIP)
        {
            try
            {
                #region 非数据库端验证mobileNum
                RESTfulModel cv_MobileNum = ClientValidateMobileNum(mobileNum);
                if (cv_MobileNum.Code != 0)
                {
                    return cv_MobileNum;
                }
                #endregion

                #region 数据库端验证appId
                //RESTfulModel sv_AppId = ServerValidateAppId(appId, requestIP);
                //if (sv_AppId.Code != 0)
                //{
                //    return sv_AppId;
                //}
                #endregion

                DateTime dt = DateTime.Now;

                #region 添加smscode
                string codeValue;
                smsCodeDAL.Insert( mobileNum, dt, out codeValue);

                return new RESTfulModel() { Code = (int)CodeEnum.成功, Msg = string.Format(CodeMsgDAL.GetByCode((int)CodeEnum.成功), "成功"), SMSCode = codeValue };
                #endregion
            }
            catch (Exception ex)
            {
                LogHelper.Error(ex);
                return new RESTfulModel() { Code = (int)CodeEnum.系统异常, Msg = CodeMsgDAL.GetByCode((int)CodeEnum.系统异常) };
            }
        }

        #region 验证手机号
        private RESTfulModel ClientValidateMobileNum(string mobileNum)
        {
            #region 非数据库端验证mobileNum
            if (ValidateHelper.IsNullOrEmpty(mobileNum))
            {
                return new RESTfulModel() { Code = (int)CodeEnum.手机号不能为空, Msg = CodeMsgDAL.GetByCode((int)CodeEnum.手机号不能为空) };
            }
            if (!ValidateHelper.MobileNum(mobileNum))
            {
                return new RESTfulModel() { Code = (int)CodeEnum.手机号格式错误, Msg = CodeMsgDAL.GetByCode((int)CodeEnum.手机号格式错误) };
            }
            #endregion

            return new RESTfulModel() { Code = (int)CodeEnum.成功, Msg = string.Format(CodeMsgDAL.GetByCode((int)CodeEnum.成功), "成功") };
        }
        #endregion

        

       

        private RESTfulModel ClientValidateSMSCode(string smsCode)
        {
            #region 非数据库端验证smsCode
            if (ValidateHelper.IsNullOrEmpty(smsCode))
            {
                return new RESTfulModel() { Code = (int)CodeEnum.短信验证码不能为空, Msg = CodeMsgDAL.GetByCode((int)CodeEnum.短信验证码不能为空) };
            }
            if (!ValidateHelper.SMSCode(smsCode))
            {
                return new RESTfulModel() { Code = (int)CodeEnum.短信验证码格式错误, Msg = CodeMsgDAL.GetByCode((int)CodeEnum.短信验证码格式错误) };
            }
            #endregion

            return new RESTfulModel() { Code = (int)CodeEnum.成功, Msg = string.Format(CodeMsgDAL.GetByCode((int)CodeEnum.成功), "成功") };
        }

        private RESTfulModel ClientValidateToken(string token)
        {
            #region 非数据库端验证token
            if (ValidateHelper.IsNullOrEmpty(token))
            {
                return new RESTfulModel() { Code = (int)CodeEnum.Token不能为空, Msg = CodeMsgDAL.GetByCode((int)CodeEnum.Token不能为空) };
            }
            if (!ValidateHelper.Guid(token))
            {
                return new RESTfulModel() { Code = (int)CodeEnum.Token格式错误, Msg = CodeMsgDAL.GetByCode((int)CodeEnum.Token格式错误) };
            }
            #endregion

            return new RESTfulModel() { Code = (int)CodeEnum.成功, Msg = string.Format(CodeMsgDAL.GetByCode((int)CodeEnum.成功), "成功") };
        }

        #region 多app验证app
        //private RESTfulModel ServerValidateAppId(int appId, string requestIP)
        //{
        //    #region 数据库端验证appId
        //    AppModel appModel = appModelList.Where(o => o.Id == appId).FirstOrDefault();
        //    if (appModel == null)
        //    {
        //        return new RESTfulModel() { Code = (int)CodeEnum.AppId错误, Msg = CodeMsgDAL.GetByCode((int)CodeEnum.AppId错误) };
        //    }
        //    string[] appModel_IPSet = appModel.IPSet.Split(',');
        //    if (!appModel_IPSet.Contains(requestIP ?? "127.0.0.1"))
        //    {
        //        return new RESTfulModel() { Code = (int)CodeEnum.AppId错误, Msg = CodeMsgDAL.GetByCode((int)CodeEnum.服务器拒绝此IP请求) };
        //    }
        //    #endregion

        //    return new RESTfulModel() { Code = (int)CodeEnum.成功, Msg = string.Format(CodeMsgDAL.GetByCode((int)CodeEnum.成功), "成功") };
        //} 
        #endregion
    }
}
