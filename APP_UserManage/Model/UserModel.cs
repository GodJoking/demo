using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class UserModel
    {
        public int Id { get; set; }

        public string UserId { get; set; }

        public string NickName { get; set; }

        public string RealName { get; set; }

        public string IDCard { get; set; }

        public string UserAvatar { get; set; }

        public string Email { get; set; }

        public string MobileNum { get; set; }

        public string Password { get; set; }

        public string Salt { get; set; }

        public string UserToken { get; set; }

        public DateTime TokenUpdateTime { get; set; }

        public int AppId { get; set; }

        public int AuthorizedTypeId { get; set; }

        public string OpenId { get; set; }

        public string UnionId { get; set; }

        public string Extra { get; set; }

        public DateTime RegisterTime { get; set; }

        public DateTime CreateTime { get; set; }

        public DateTime UpdateTime { get; set; }

        public string CreateIP { get; set; }

        public int SystemTypeId { get; set; }

        public string EquipmentNum { get; set; }

        public int IsDel { get; set; }
    }
}
