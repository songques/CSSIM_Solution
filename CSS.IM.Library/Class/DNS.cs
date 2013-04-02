using System;
using System.Collections.Generic;
using System.Text;
using System.DirectoryServices;

namespace CSS.IM.Library 
{
    /// <summary>
    /// DNS��
    /// </summary>
    public sealed class DNS 
    {
        /// <summary>
        /// DNS��
        /// </summary>
        private DNS()
        {
        }
        /// <summary>
        /// �������û�������
        /// </summary>
        /// <param name="UserName">���û���</param>
        /// <param name="OldPassword">������</param>
        /// <param name="NewPassword">������</param>
        /// <param name="DomainName">DNS����</param>
        /// <returns>�ɹ������棬���ɹ����ؼ�</returns>
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
        /// �ж��û��Ƿ����û�
        /// </summary>
        /// <param name="UserName">�û���</param>
        /// <param name="Password">�û�����</param>
        /// <param name="DomainName">windows����</param>
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
