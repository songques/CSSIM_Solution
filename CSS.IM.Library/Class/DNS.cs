using System;
using System.Collections.Generic;
using System.Text;
using System.DirectoryServices;

namespace CSS.IM.Library 
{
    /// <summary>
    /// DNS类
    /// </summary>
    public sealed class DNS 
    {
        /// <summary>
        /// DNS类
        /// </summary>
        private DNS()
        {
        }
        /// <summary>
        /// 更改域用户的密码
        /// </summary>
        /// <param name="UserName">域用户名</param>
        /// <param name="OldPassword">旧密码</param>
        /// <param name="NewPassword">新密码</param>
        /// <param name="DomainName">DNS域名</param>
        /// <returns>成功返回真，不成功返回假</returns>
        public static bool ChangePassword(string UserName, string OldPassword, string NewPassword, string DomainName)
        {
            try
            {
                string UserPrincipalName = UserName + "@" + DomainName;
                DirectoryEntry deRootDSE = new DirectoryEntry("LDAP://RootDSE", UserPrincipalName, OldPassword, AuthenticationTypes.Secure);
                DirectoryEntry deDomain = new DirectoryEntry("LDAP://" + deRootDSE.Properties["defaultNamingContext"].Value.ToString(), UserPrincipalName, OldPassword, AuthenticationTypes.Secure);
                DirectorySearcher dsSearcher = new DirectorySearcher();
                dsSearcher.SearchRoot = deDomain;
                dsSearcher.SearchScope = SearchScope.Subtree;
                dsSearcher.Filter = "(userPrincipalName=" + UserPrincipalName + ")";
                SearchResult srResult = dsSearcher.FindOne();
                if (srResult != null)
                {
                    DirectoryEntry deUser = new DirectoryEntry(srResult.GetDirectoryEntry().Path, UserPrincipalName, OldPassword, AuthenticationTypes.Secure);
                    deUser.Invoke("ChangePassword", new object[] { OldPassword, NewPassword });
                    deUser.CommitChanges();
                    return true;
                }
                else
                    return false;
            }
            catch //(Exception ex)
            {
                return false;// ex.Message;
            }
        }

        /// <summary>
        /// 判断用户是否域用户
        /// </summary>
        /// <param name="UserName">用户名</param>
        /// <param name="Password">用户密码</param>
        /// <param name="DomainName">windows域名</param>
        /// <returns></returns>
        public static bool IsDomainUser(string UserName, string Password, string DomainName)
        {
            try
            {
                string UserPrincipalName = UserName + "@" + DomainName;
                DirectoryEntry deRootDSE = new DirectoryEntry("LDAP://RootDSE", UserPrincipalName,  Password, AuthenticationTypes.Secure);
                DirectoryEntry deDomain = new DirectoryEntry("LDAP://" + deRootDSE.Properties["defaultNamingContext"].Value.ToString(), UserPrincipalName,  Password, AuthenticationTypes.Secure);
                DirectorySearcher dsSearcher = new DirectorySearcher();
                dsSearcher.SearchRoot = deDomain;
                dsSearcher.SearchScope = SearchScope.Subtree;
                dsSearcher.Filter = "(userPrincipalName=" + UserPrincipalName + ")";
                SearchResult srResult = dsSearcher.FindOne();
                if (srResult != null)
                     return true;
                else
                    return true;
            }
            catch //(Exception ex)
            {
                return false;// ex.Message;
            }
         }
          
    }


}
