using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.Enum;
using Model;
namespace DAL
{
    public class CodeMsgDAL 
    {
        #region 前台
        public static string GetByCode(int code)
        {
            CodeMsgModel retModel = Singleton_Ret.GetInstance().Where(o => o.Code == code).FirstOrDefault();
            return retModel == null ? "" : retModel.Msg;
        }
        #endregion
    }

    public sealed class Singleton_Ret
    {
        // 懒汉式单例模式
        // 存在内存中效率更高
        // 每次添加或修改返回码需要改代码，所以可以用此种方式加载返回码
        private static List<CodeMsgModel> instance = new List<CodeMsgModel>() {

            new CodeMsgModel(){ Code = (int)CodeEnum.系统异常, Msg = "系统异常" },
            // 成功
            new CodeMsgModel(){ Code = (int)CodeEnum.成功, Msg = "{0}" },
            // 失败
            new CodeMsgModel(){ Code = (int)CodeEnum.失败, Msg = "系统异常" },

            // 用户不可见
            //new RetModel(){ Code = (int)CodeEnum.AppId错误, Msg = "系统异常" },
            //new RetModel(){ Code = (int)CodeEnum.AuthorizedTypeId错误, Msg = "系统异常" },
            //new RetModel(){ Code = (int)CodeEnum.IP不能为空, Msg = "系统异常" },
            //new RetModel(){ Code = (int)CodeEnum.IP格式错误, Msg = "系统异常" },
            //new RetModel(){ Code = (int)CodeEnum.设备系统类型错误, Msg = "系统异常" },
            //new RetModel(){ Code = (int)CodeEnum.设备号格式错误, Msg = "系统异常" },
            //new RetModel(){ Code = (int)CodeEnum.OpenId错误, Msg = "系统异常" },
            //new RetModel(){ Code = (int)CodeEnum.UnionId错误, Msg = "系统异常" },

            new CodeMsgModel(){ Code = (int)CodeEnum.AppId错误, Msg = "AppId不存在" },
            new CodeMsgModel(){ Code = (int)CodeEnum.AuthorizedTypeId错误, Msg = "AuthorizedTypeId错误" },
            new CodeMsgModel(){ Code = (int)CodeEnum.IP不能为空, Msg = "IP不能为空" },
            new CodeMsgModel(){ Code = (int)CodeEnum.IP格式错误, Msg = "IP格式错误" },
            new CodeMsgModel(){ Code = (int)CodeEnum.设备系统类型错误, Msg = "设备系统类型错误" },
            new CodeMsgModel(){ Code = (int)CodeEnum.设备号格式错误, Msg = "设备号格式错误" },
            new CodeMsgModel(){ Code = (int)CodeEnum.OpenId不能为空, Msg = "OpenId不能为空" },
            new CodeMsgModel(){ Code = (int)CodeEnum.UnionId不能为空, Msg = "UnionId不能为空" },
            new CodeMsgModel(){ Code = (int)CodeEnum.服务器拒绝此IP请求, Msg = "服务器拒绝此IP请求" },
            new CodeMsgModel(){ Code = (int)CodeEnum.UnionUserId不能为空, Msg = "UnionUserId不能为空" },
            new CodeMsgModel(){ Code = (int)CodeEnum.UnionUserId格式错误, Msg = "UnionUserId格式错误" },

            // 用户可见错误
            // 参数错误
            new CodeMsgModel(){ Code = (int)CodeEnum.手机号不能为空, Msg = "请输入手机号" },
            new CodeMsgModel(){ Code = (int)CodeEnum.手机号格式错误, Msg = "手机号输入不规范" }, //请输入正确的11位手机号
            new CodeMsgModel(){ Code = (int)CodeEnum.密码不能为空, Msg = "请输入密码" },
            new CodeMsgModel(){ Code = (int)CodeEnum.密码格式错误, Msg = "帐号或密码错误，请重新输入" },

            new CodeMsgModel(){ Code = (int)CodeEnum.新手机号不能为空, Msg = "请输入手机号" },
            new CodeMsgModel(){ Code = (int)CodeEnum.新手机号格式错误, Msg = "请输入正确的11位手机号" },

            new CodeMsgModel(){ Code = (int)CodeEnum.旧密码不能为空, Msg = "请输入旧密码" },
            new CodeMsgModel(){ Code = (int)CodeEnum.旧密码格式错误, Msg = "旧密码错误" },
            new CodeMsgModel(){ Code = (int)CodeEnum.新密码不能为空, Msg = "请输入新密码" },
            new CodeMsgModel(){ Code = (int)CodeEnum.新密码格式错误, Msg = "密码不符合规则，请重新输入" },

            new CodeMsgModel(){ Code = (int)CodeEnum.Token不能为空, Msg = "请重新登录" },
            new CodeMsgModel(){ Code = (int)CodeEnum.Token格式错误, Msg = "请重新登录" },
            new CodeMsgModel(){ Code = (int)CodeEnum.短信验证码不能为空, Msg = "请输入短信中的验证码" },
            new CodeMsgModel(){ Code = (int)CodeEnum.短信验证码格式错误, Msg = "短信验证码输入错误，请重新输入" },

            // 系统异常
            new CodeMsgModel(){ Code = (int)CodeEnum.Token无效, Msg = "请重新登录" },
            new CodeMsgModel(){ Code = (int)CodeEnum.短信验证码无效, Msg = "验证码输入错误" }, //短信验证码输入错误，请重新输入
            new CodeMsgModel(){ Code = (int)CodeEnum.手机号已注册, Msg = "手机号已在{0}平台注册过，可以直接登录" },
            new CodeMsgModel(){ Code = (int)CodeEnum.该手机号尚未注册, Msg = "该手机号尚未注册，请先注册" },
            new CodeMsgModel(){ Code = (int)CodeEnum.密码错误, Msg = "手机号或密码错误，请重新输入" },
            new CodeMsgModel(){ Code = (int)CodeEnum.帐号已被冻结, Msg = "帐号已被冻结" },
            new CodeMsgModel(){ Code = (int)CodeEnum.三方帐号已绑定过手机号, Msg = "三方帐号已绑定过手机号" },
            new CodeMsgModel(){ Code = (int)CodeEnum.App版本过旧, Msg = "您当前的版本过低，无法正常使用第三方登录，请进行版本升级" },
            new CodeMsgModel(){ Code = (int)CodeEnum.Token已过期, Msg = "登陆已过期，请重新登录" },
            new CodeMsgModel(){ Code = (int)CodeEnum.此版本不支持绑定手机号, Msg = "由于业务需要，近期暂时停止第三方账号绑定手机号，由此给大家带来不便，深感歉意。" },
            new CodeMsgModel(){ Code = (int)CodeEnum.更新九合Id失败, Msg = "更新九合Id失败" },
            new CodeMsgModel(){ Code = (int)CodeEnum.短信验证码3次输入错误失效, Msg = "多次输入验证码" }, //短信验证码3次输入错误失效
            new CodeMsgModel(){ Code = (int)CodeEnum.AppId冲突, Msg = "AppId冲突" },
            new CodeMsgModel(){ Code = (int)CodeEnum.UnionUserId无效, Msg = "UnionUserId无效" },
        };

        private Singleton_Ret() { }

        public static List<CodeMsgModel> GetInstance()
        {
            return instance;
        }
    }
}
